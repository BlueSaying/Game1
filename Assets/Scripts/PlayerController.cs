using System.Collections.Generic;
using UnityEngine;

// 专注于非战斗状态下的Player逻辑
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float rotSpeed = 1.0f;

    private Vector3 fallingVelocity;

    private CharacterController characterController;

    // 能否继续前进
    private bool canMove;
    private bool canMoveForward;
    private HashSet<GameObject> triggeringObj;

    void Awake()
    {
        // 总是快速下落的
        fallingVelocity = new Vector3(0, -10, 0);

        characterController = GetComponent<CharacterController>();

        canMove = true;
        canMoveForward = true;
        triggeringObj = new HashSet<GameObject>();
    }

    void Start()
    {
        // 检测前方是否为悬崖
        transform.Find("FrontDetector").GetComponent<TriggerDetector>().AddTriggerListener(TriggerEventType.OnTriggerEnter, "Ground", (obj) =>
        {
            canMoveForward = true;
            triggeringObj.Add(obj);
        });
        transform.Find("FrontDetector").GetComponent<TriggerDetector>().AddTriggerListener(TriggerEventType.OnTriggerExit, "Ground", (obj) =>
        {
            if (triggeringObj.Contains(obj))
            {
                triggeringObj.Remove(obj);
                if (triggeringObj.Count == 0)
                {
                    canMoveForward = false;
                }
            }
        });
    }

    void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    public void LockMove()
    {
        canMove = false;
    }

    public void UnlockMove()
    {
        canMove = true;
    }

    private void Move()
    {
        // 键盘移动
        float x = 0, z = 0;
        if (Input.GetKey(KeyCode.D)) x += 1;
        if (Input.GetKey(KeyCode.A)) x -= 1;
        if (Input.GetKey(KeyCode.W)) z += 1;
        if (Input.GetKey(KeyCode.S)) z -= 1;
        Vector3 motion = new Vector3(x, 0, z);

        // 鼠标移动
        if (Camera.main != null && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int groundLayerMask = 1 << LayerMask.NameToLayer("Ground");

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundLayerMask))
            {
                motion = hitInfo.point - transform.position;
                motion.y = 0;
            }
            else
            {
                Debug.Log("鼠标没点击到地面");
            }
        }

        // 判断能否向前移动
        if (canMoveForward || Vector3.Dot(motion, transform.forward) <= 0)
        {
            characterController.Move((motion.normalized * moveSpeed + fallingVelocity) * Time.deltaTime);
        }

        if (motion.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(motion * rotSpeed);
        }
    }

    #region 事件集
    #endregion
}