using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    void Update()
    {
        float x = 0, y = 0;
        if (Input.GetKey(KeyCode.D)) x += 1;
        if (Input.GetKey(KeyCode.A)) x -= 1;
        if (Input.GetKey(KeyCode.W)) y += 1;
        if (Input.GetKey(KeyCode.S)) y -= 1;

        Vector3 xBase = transform.right.normalized;
        Vector3 yBase = (transform.up + transform.forward).normalized;
        Vector3 moveDir = x * xBase + y * yBase;

        transform.Translate(moveDir * moveSpeed * Time.deltaTime,Space.World);
    }
}
