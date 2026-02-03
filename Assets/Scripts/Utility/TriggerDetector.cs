using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using GameObjectTag = System.String;

/// <summary>
/// 触发事件类型
/// </summary>
public enum TriggerEventType
{
    OnTriggerEnter,
    OnTriggerExit,
    OnTriggerStay,
}

public class TriggerDetector : MonoBehaviour
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

    private void OnTriggerEnter(Collider collision)
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

    private void OnTriggerExit(Collider collision)
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

    private void OnTriggerStay(Collider collision)
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

    /// <summary>
    /// 不要在Awake中使用
    /// </summary>
    /// <param name="triggerEventType"></param>
    /// <param name="targetTag"></param>
    /// <param name="action"></param>
    public void AddTriggerListener(TriggerEventType triggerEventType, GameObject target, UnityAction<GameObject> action)
    {
        switch (triggerEventType)
        {
            case TriggerEventType.OnTriggerEnter:
                if (!enterDic.ContainsKey(target))
                {
                    enterDic.Add(target, action);
                }
                else
                {
                    enterDic[target] += action;
                }
                break;
            case TriggerEventType.OnTriggerExit:
                if (!exitDic.ContainsKey(target))
                {
                    exitDic.Add(target, action);
                }
                else
                {
                    exitDic[target] += action;
                }
                break;
            case TriggerEventType.OnTriggerStay:
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

    /// <summary>
    /// 不要在Awake中使用
    /// </summary>
    /// <param name="triggerEventType"></param>
    /// <param name="targetTag"></param>
    /// <param name="action"></param>
    public void AddTriggerListener(TriggerEventType triggerEventType, GameObjectTag targetTag, UnityAction<GameObject> action)
    {
        switch (triggerEventType)
        {
            case TriggerEventType.OnTriggerEnter:
                if (!enterTagDic.ContainsKey(targetTag))
                {
                    enterTagDic.Add(targetTag, action);
                }
                else
                {
                    enterTagDic[targetTag] += action;
                }
                break;
            case TriggerEventType.OnTriggerExit:
                if (!exitTagDic.ContainsKey(targetTag))
                {
                    exitTagDic.Add(targetTag, action);
                }
                else
                {
                    exitTagDic[targetTag] += action;
                }
                break;
            case TriggerEventType.OnTriggerStay:
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

    public void Clear()
    {
        enterDic.Clear();
        exitDic.Clear();
        stayDic.Clear();

        enterTagDic.Clear();
        exitTagDic.Clear();
        stayTagDic.Clear();
    }
}