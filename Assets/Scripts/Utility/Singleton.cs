using System;
using System.Reflection;

public abstract class Singleton<T> where T : Singleton<T>
{
    protected Singleton() { }

    protected static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // 以下方法用于实例化instance变量
                // 不使用new的原因是子类T的构造函数为private，必须使用反射特性绕开此限制

                // 先获取所有非public构造方法
                ConstructorInfo[] ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

                // 然后从ctors中获取无参的构造方法
                ConstructorInfo ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                if (ctor == null)
                {
                    throw new Exception("Non-public ctor() not  found in " + typeof(T).ToString());
                }
                instance = ctor.Invoke(null) as T;
            }
            return instance;
        }
    }
}