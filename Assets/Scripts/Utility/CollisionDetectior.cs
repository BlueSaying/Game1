using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using GameObjectTag = System.String;

/// <summary>
/// 碰撞事件类型
/// </summary>
public enum CollisionEventType
{
    OnCollisionEnter,
    OnCollisionExit,
    OnCollisionStay,
}

public class CollisionDetector : MonoBehaviour
{
    private Dictionary<GameObject, UnityAction<GameObject>> enterDic;
    private Dictionary<GameObject, UnityAction<GameObject>> exitDic;
    private Dictionary<GameObject, UnityAction<GameObject>> stayDic;

    private Dictionary<GameObjectTag, UnityAction<GameObject>> enterTagDic;
    private Dictionary<GameObjectTag, UnityAction<GameObject>> exitTagDic;
    private Dictionary<GameObjectTag, UnityAction<GameObject>> stayTagDic;

    private void Awake()
    {
        enterDic = new Dictionary<GameObject, UnityAction<GameObject>>();
        exitDic = new Dictionary<GameObject, UnityAction<GameObject>>();
        stayDic = new Dictionary<GameObject, UnityAction<GameObject>>();

        enterTagDic = new Dictionary<GameObjectTag, UnityAction<GameObject>>();
        exitTagDic = new Dictionary<GameObjectTag, UnityAction<GameObject>>();
        stayTagDic = new Dictionary<GameObjectTag, UnityAction<GameObject>>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enterDic.ContainsKey(collision.gameObject))
        {
            enterDic[collision.gameObject].Invoke(collision.gameObject);
        }
        if (enterTagDic.ContainsKey(collision.gameObject.tag))
        {
            enterTagDic[collision.gameObject.tag].Invoke(collision.gameObject);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (exitDic.ContainsKey(collision.gameObject))
        {
            exitDic[collision.gameObject].Invoke(collision.gameObject);
        }
        if (exitTagDic.ContainsKey(collision.gameObject.tag))
        {
            exitTagDic[collision.gameObject.tag].Invoke(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (stayDic.ContainsKey(collision.gameObject))
        {
            stayDic[collision.gameObject].Invoke(collision.gameObject);
        }
        if (stayTagDic.ContainsKey(collision.gameObject.tag))
        {
            stayTagDic[collision.gameObject.tag].Invoke(collision.gameObject);
        }
    }

    public void AddCollisionListener(CollisionEventType collisionEventType, GameObject target, UnityAction<GameObject> action)
    {
        switch (collisionEventType)
        {
            case CollisionEventType.OnCollisionEnter:
                if (!enterDic.ContainsKey(target))
                {
                    enterDic.Add(target, action);
                }
                else
                {
                    enterDic[target] += action;
                }
                break;
            case CollisionEventType.OnCollisionExit:
                if (!exitDic.ContainsKey(target))
                {
                    exitDic.Add(target, action);
                }
                else
                {
                    exitDic[target] += action;
                }
                break;
            case CollisionEventType.OnCollisionStay:
                if (!stayDic.ContainsKey(target))
                {
                    stayDic.Add(target, action);
                }
                else
                {
                    stayDic[target] += action;
                }
                break;
        }
    }

    public void AddCollisionListener(CollisionEventType collisionEventType, GameObjectTag targetTag, UnityAction<GameObject> action)
    {
        switch (collisionEventType)
        {
            case CollisionEventType.OnCollisionEnter:
                if (!enterTagDic.ContainsKey(targetTag))
                {
                    enterTagDic.Add(targetTag, action);
                }
                else
                {
                    enterTagDic[targetTag] += action;
                }
                break;
            case CollisionEventType.OnCollisionExit:
                if (!exitTagDic.ContainsKey(targetTag))
                {
                    exitTagDic.Add(targetTag, action);
                }
                else
                {
                    exitTagDic[targetTag] += action;
                }
                break;
            case CollisionEventType.OnCollisionStay:
                if (!stayTagDic.ContainsKey(targetTag))
                {
                    stayTagDic.Add(targetTag, action);
                }
                else
                {
                    stayTagDic[targetTag] += action;
                }
                break;
        }
    }
}