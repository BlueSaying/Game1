using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    private Rigidbody rb;

    private Vector3 moveDir = Vector3.zero;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = 0, z = 0;
        if (Input.GetKey(KeyCode.D)) x += 1;
        if (Input.GetKey(KeyCode.A)) x -= 1;
        if (Input.GetKey(KeyCode.W)) z += 1;
        if (Input.GetKey(KeyCode.S)) z -= 1;
        moveDir = new Vector3(x, 0, z).normalized;
    }

    void FixedUpdate()
    {
        MoveManager.Move(rb, moveDir * moveSpeed,2,1);
    }
}
