using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public static class UnityTools
{
    public static int GetRandomInt(int min, int max)
    {
        return UnityEngine.Random.Range(min, max + 1);
    }

    public static float GetRandomFloat(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    /// <summary>
    /// call function after seconds
    /// </summary>
    /// <param name="caller"></param>
    /// <param name="waitTime"></param>
    /// <param name="action"></param>
    public static void WaitThenCallFun(object caller, float waitTime, UnityAction action)
    {
        CoroutinePool.Instance.StartCoroutine(caller, WaitSecondsCallFun(waitTime, action));
    }

    private static IEnumerator WaitSecondsCallFun(float waitTime, UnityAction action)
    {
        yield return new WaitForSeconds(waitTime);
        action.Invoke();
    }

    /// <summary>
    /// 在父对象的所有子层级中查找指定名称的物体，并获取其上的指定类型组件
    /// </summary>
    /// <typeparam name="T">要获取的组件类型</typeparam>
    /// <param name="parent">要搜索的父对象（会递归搜索所有层级的子物体）</param>
    /// <param name="name">要查找的子物体名称</param>
    /// <param name="includeInactive">是否搜索未激活的游戏物体（默认为搜索）</param>>
    /// <returns>
    /// 找到时：返回匹配的第一个子物体上的T类型组件；
    /// 未找到时：返回null
    /// </returns>
    /// <remarks>
    /// 注意：
    /// 1. 会搜索父对象自身及其所有层级的子对象
    /// 2. 需要同时满足名称匹配和组件存在两个条件
    /// 3. 只返回第一个找到的匹配项
    /// 4. 对性能敏感的场景慎用（遍历所有子对象+多次GetComponent调用）
    /// </remarks>
    public static T GetComponentFromChildren<T>(GameObject parent, string name, bool includeInactive = true)
    {
        if (parent == null) return default;

        foreach (Transform t in parent.GetComponentsInChildren<Transform>(includeInactive))
        {
            if (t.name == name && t.GetComponent<T>() != null)
            {
                return t.GetComponent<T>();
            }
        }
        return default;
    }

    /// <summary>
    /// 在父对象的所有子层级中查找指定名称的Transform
    /// </summary>
    /// <param name="parent">要搜索的父对象（会递归搜索所有子对象）</param>
    /// <param name="name">要查找的子物体名称（区分大小写）</param>
    /// <param name="includeInactive">是否搜索未激活的游戏物体（默认为搜索）</param>>
    /// <returns>
    /// 找到时：返回匹配的第一个子物体的Transform组件；
    /// 未找到时：返回null（default）
    /// </returns>
    public static Transform GetTransformFromChildren(GameObject parent, string name, bool includeInactive = true)
    {
        if (parent == null) return null;

        foreach (Transform t in parent.GetComponentsInChildren<Transform>(includeInactive))
        {
            if (t.name == name)
            {
                return t;
            }
        }

        return default;
    }

    /// <summary>
    /// 检查一个类型是否继承或实现了特定的泛型类型（包括泛型接口）
    /// </summary>
    /// <param name="type"></param>
    /// <param name="genericType"></param>
    /// <returns></returns>
    public static bool isGenericType(Type type, Type genericType)
    {
        if (type == null || genericType == null) return false;

        if (type.GetInterfaces().Any(isGeneric)) return true;

        while (type != null && type != typeof(object))
        {
            if (isGeneric(type)) return true;
            type = type.BaseType;
        }

        return false;

        bool isGeneric(Type type)
        {
            if (!type.IsGenericType) return false;
            if (type.GetGenericTypeDefinition() == genericType) return true;
            return false;
        }
    }

    /// <summary>
    /// 将字符串转换为指定类型的值
    /// </summary>
    /// <param name="s"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static object ConvertType(string s, Type type)
    {
        if (s == "None" || s == "") return null;

        if (s == "TRUE")
        {
            return true;
        }
        if (s == "FALSE")
        {
            return false;
        }

        // 检查type是否是枚举类型
        if (typeof(Enum).IsAssignableFrom(type))
        {

            if (Enum.TryParse(type, s, out object result))
            {
                return result;
            }
            else
            {
                throw new Exception("无法将" + s + "转换为类型" + type);
            }
        }

        object obj = Convert.ChangeType(s, type);
        if (obj == null)
        {
            throw new Exception("无法将" + s + "转换为类型" + type);
        }
        else
        {
            return obj;
        }
    }

    private class ListInfo
    {
        public FieldInfo fieldInfo;
        public MethodInfo addMethod;
    }

    /// <summary>
    /// 将文本写入list中
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="textAsset"></param>
    /// <exception cref="Exception"></exception>
    public static void WriteDataToList<T>(List<T> list, TextAsset textAsset) where T : new()
    {
        if (textAsset == null) return;

        list.Clear();

        Type type = typeof(T);
        string text = textAsset.text.Replace("\r", "");
        string[] rows = text.Split("\n");

        // 以逗号分割字段
        string[] fieldNames = rows[0].Split(",");

        // *preFieldName*用于创建链表
        string preFieldName = null;
        List<ListInfo> listInfos = new List<ListInfo>();

        // 遍历所有fieldNames里的所有字段
        foreach (string fieldName in fieldNames)
        {
            // NOTE:GetField方法可以获取type的成员变量（通过比较fieldName与成员变量名称进行字符串比较）
            FieldInfo info = type.GetField(fieldName);

            if (info == null)
            {
                throw new Exception("类型：" + type.ToString() + "中不存在成员变量：" + fieldName);
            }

            // 判断info.FieldType是否是IList类型
            if (typeof(System.Collections.IList).IsAssignableFrom(info.FieldType))
            {
                if (fieldName != preFieldName)
                {
                    preFieldName = fieldName;
                    ListInfo listInfo = new ListInfo();
                    listInfo.fieldInfo = info;
                    listInfo.addMethod = info.FieldType.GetMethod("Add");   // 获取List.Add方法
                    listInfos.Add(listInfo);
                }
            }
        }

        for (int i = 1; i < rows.Length; i++)
        {
            if (rows[i].Length == 0) continue;

            string[] columes = rows[i].Split(",");
            T obj = (T)Activator.CreateInstance(type);

            foreach (ListInfo info in listInfos)
            {
                // 该语句实现对*obj*的*info.fieldInfo*字段的值的修改
                // MakeGenericType用于将List<T>转换为List<info.fieldInfo.FieldType.GenericTypeArguments>
                // GenericTypeArguments用于获取已构造的泛型类型的具体参数类型
                info.fieldInfo.SetValue(obj, Activator.CreateInstance(typeof(List<>).MakeGenericType(info.fieldInfo.FieldType.GenericTypeArguments)));
            }

            bool isValidData = false;
            for (int j = 0; j < columes.Length; j++)
            {
                FieldInfo fieldInfo = type.GetField(fieldNames[j]);

                if (columes.Length == 0 || fieldInfo == null) continue;

                if (typeof(System.Collections.IList).IsAssignableFrom(fieldInfo.FieldType))
                {
                    foreach (ListInfo info in listInfos)
                    {
                        if (fieldInfo == info.fieldInfo)
                        {
                            object val = ConvertType(columes[j], fieldInfo.FieldType.GenericTypeArguments[0]);
                            if (val == null) continue;
                            // Invoke用于执行Add方法
                            info.addMethod.Invoke(fieldInfo.GetValue(obj), new object[] { val });
                            isValidData = true;
                        }
                    }
                }
                else
                {
                    object val = ConvertType(columes[j], fieldInfo.FieldType);
                    if (val == null) continue;
                    fieldInfo.SetValue(obj, val);
                    isValidData = true;
                }
            }
            if (isValidData)
            {
                list.Add(obj);
            }
        }
    }
}