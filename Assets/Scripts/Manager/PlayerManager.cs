using UnityEngine;

public class PlayerManager : MonoBehaviourSingleton<PlayerManager>
{
    public PlayerController PlayerController { get; private set; }

    public PlayerUnit PlayerUnit { get; private set; }
    public PlayerModel PlayerModel => PlayerUnit.Model;

    private void Start()
    {
        EventCenter.Instance.RegisterEvent(EventType.OnSceneSwitchComplete, FindPlayer);

    }

    public void SetPlayerTransform(Transform transform)
    {
        PlayerController.transform.position = transform.position;
        PlayerController.transform.rotation = transform.rotation;
        PlayerController.transform.localScale = transform.localScale;
    }

    public void LockMove()
    {
        PlayerController.LockMove();
    }

    public void UnlockMove()
    {
        PlayerController.UnlockMove();
    }

    #region 事件集
    public void FindPlayer()
    {
        PlayerController = GameObject.Find("Player")?.GetComponent<PlayerController>();
        PlayerUnit = GameObject.Find("Player")?.GetComponent<PlayerUnit>();
    }
    #endregion
}