using System.Collections.Generic;
using UnityEngine.Events;

public enum EventType
{
    /// <summary>
    /// 场景开始切换
    /// </summary>
    OnSceneSwitchStart,

    /// <summary>
    /// 场景完成切换
    /// </summary>
    OnSceneSwitchComplete,

    // 以上为永久事件
    PermanentDividingLine,
    // 以下为非永久事件

    /// <summary>
    /// 战斗开始
    /// </summary>
    OnBattleStart,

    /// <summary>
    /// 战斗结束
    /// </summary>
    OnBattleFinish,

    /// <summary>
    /// 敌人死亡
    /// </summary>
    OnEnemyDie,

    /// <summary>
    /// 当玩家数据改变
    /// </summary>
    OnPlayerInfoChanged,

    /// <summary>
    /// 玩家死亡
    /// </summary>
    OnPlayerDie,

    /// <summary>
    /// 更新战斗界面
    /// </summary>
    UpdateBattlePanel,
}

public class EventCenter : Singleton<EventCenter>
{
    public class BaseEventInfo { }

    // 无参事件
    public class EventInfo : BaseEventInfo
    {
        public UnityAction action;

        public EventInfo(UnityAction action)
        {
            this.action = action;
        }
    }

    // 1个参数事件
    public class EventInfo<T> : BaseEventInfo
    {
        public UnityAction<T> action;

        public EventInfo(UnityAction<T> action)
        {
            this.action = action;
        }
    }

    // 2个参数事件
    public class EventInfo<T, U> : BaseEventInfo
    {
        public UnityAction<T, U> action;

        public EventInfo(UnityAction<T, U> action)
        {
            this.action = action;
        }
    }

    public Dictionary<EventType, List<BaseEventInfo>> eventDic;

    private EventCenter()
    {
        eventDic = new Dictionary<EventType, List<BaseEventInfo>>();
    }

    private bool isPermanentEvent(EventType eventType)
    {
        return eventType < EventType.PermanentDividingLine;
    }

    public void RegisterEvent(EventType type, UnityAction action)
    {
        if (!eventDic.TryGetValue(type, out var infoList))
        {
            infoList = new List<BaseEventInfo>();
            eventDic.Add(type, infoList);
        }

        foreach (var info in infoList)
        {
            if (info is EventInfo)
            {
                (info as EventInfo).action += action;
                return;
            }
        }

        infoList.Add(new EventInfo(action));
    }

    public void RegisterEvent<T>(EventType type, UnityAction<T> action)
    {
        if (!eventDic.TryGetValue(type, out var infoList))
        {
            infoList = new List<BaseEventInfo>();
            eventDic.Add(type, infoList);
        }

        foreach (var info in infoList)
        {
            if (info is EventInfo<T>)
            {
                (info as EventInfo<T>).action += action;
                return;
            }
        }

        infoList.Add(new EventInfo<T>(action));
    }

    public void RegisterEvent<T, U>(EventType type, UnityAction<T, U> action)
    {
        if (!eventDic.TryGetValue(type, out var infoList))
        {
            infoList = new List<BaseEventInfo>();
            eventDic.Add(type, infoList);
        }

        foreach (var info in infoList)
        {
            if (info is EventInfo<T, U>)
            {
                (info as EventInfo<T, U>).action += action;
                return;
            }
        }

        infoList.Add(new EventInfo<T, U>(action));
    }

    public void NotifyEvent(EventType type)
    {
        if (!eventDic.ContainsKey(type)) return;

        foreach (BaseEventInfo info in eventDic[type])
        {
            if (info is EventInfo)
            {
                (info as EventInfo).action.Invoke();
            }
        }
    }

    public void NotifyEvent<T>(EventType type, T param)
    {
        if (!eventDic.ContainsKey(type)) return;

        foreach (BaseEventInfo info in eventDic[type])
        {
            if (info is EventInfo<T>)
            {
                (info as EventInfo<T>).action.Invoke(param);
            }
        }
    }

    public void NotifyEvent<T, U>(EventType type, T param1, U param2)
    {
        if (!eventDic.ContainsKey(type)) return;

        foreach (BaseEventInfo info in eventDic[type])
        {
            if (info is EventInfo<T, U>)
            {
                (info as EventInfo<T, U>).action.Invoke(param1, param2);
            }
        }
    }

    public void RemoveEvent(EventType type, UnityAction action)
    {
        if (!eventDic.TryGetValue(type, out var infoList)) return;

        foreach (var info in eventDic[type])
        {
            if (info is EventInfo eventInfo)
            {
                eventInfo.action -= action;
            }
        }
    }

    public void RemoveEvent<T>(EventType type, UnityAction<T> action)
    {
        if (!eventDic.TryGetValue(type, out var infoList)) return;

        foreach (var info in eventDic[type])
        {
            if (info is EventInfo<T> eventInfo)
            {
                eventInfo.action -= action;
            }
        }
    }

    public void RemoveEvent<T, U>(EventType type, UnityAction<T, U> action)
    {
        if (!eventDic.TryGetValue(type, out var infoList)) return;

        foreach (var info in eventDic[type])
        {
            if (info is EventInfo<T, U> eventInfo)
            {
                eventInfo.action -= action;
            }
        }
    }

    public void ClearNonPermanentEvents()
    {
        foreach (EventType type in eventDic.Keys)
        {
            if (isPermanentEvent(type)) continue;

            eventDic[type].Clear();
        }
    }
}