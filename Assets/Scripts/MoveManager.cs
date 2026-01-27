using UnityEngine;

public static class MoveManager
{
    public static void Move(Rigidbody rb, Vector3 targetVelocity, float moveAcc = 5.0f, float smoothingFactor = 2.0f)
    {
        if(targetVelocity.magnitude>1e-6)
        {
            rb.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, targetVelocity, Vector3.up), 0);
        }

        Vector3 v = MoveToward(rb.velocity, targetVelocity, moveAcc * 100, smoothingFactor * 100);
        v.y = rb.velocity.y;
        rb.velocity = v;
    }
    private static Vector3 MoveToward(Vector3 from, Vector3 to, float moveAcc, float smoothingFactor)
    {
        from.y = 0;
        to.y = 0;

        float distance = Vector3.Distance(from, to);
        if (distance < moveAcc * Time.fixedDeltaTime) // 如果目标速度和现在的速度之间的“距离”小于一个单位加速度
        {
            return to;  // 返回目标速度
        }
        else
        {
            float arg = moveAcc * Mathf.Exp(-from.magnitude / smoothingFactor);
            return from + (to - from).normalized * Time.fixedDeltaTime * arg;   // 返回现速度加上一个指向目标速度的加速度
        }
    }
}