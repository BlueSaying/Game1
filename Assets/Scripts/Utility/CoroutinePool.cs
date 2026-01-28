using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutinePool : MonoBehaviour
{
    public static CoroutinePool Instance
    {
        get
        {
            GameObject pool = GameObject.Find("CoroutinePool");
            if (pool == null)
            {
                pool = new GameObject("CoroutinePool", typeof(CoroutinePool));
            }

            return pool.GetComponent<CoroutinePool>();
        }
    }

    private Dictionary<object, List<Coroutine>> dic;

    private void Awake()
    {
        dic = new Dictionary<object, List<Coroutine>>();
    }

    /// <summary>
    /// 开启协程
    /// </summary>
    public void StartCoroutine(object caller, IEnumerator coroutine)
    {
        if (dic.ContainsKey(caller))
        {
            dic[caller].Add(StartCoroutine(coroutine));
        }
        else
        {
            dic.Add(caller, new List<Coroutine>() { StartCoroutine(coroutine) });
        }
    }

    public void StopAllCoroutine(object caller)
    {
        if (dic.ContainsKey(caller))
        {
            foreach (Coroutine coroutine in dic[caller])
            {
                StopCoroutine(coroutine);
            }
            dic[caller].Clear();
        }
    }
}