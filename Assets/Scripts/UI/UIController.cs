using UnityEngine;

/// <summary>
/// UI MVC Controller基类
/// </summary>
public abstract class UIController : MonoBehaviour
{
    protected bool isRemove = false;
    protected new string name;

    protected virtual void Awake() { }

    protected virtual void Start() { }

    protected virtual void Update() { }

    /// <summary>
    /// 打开UI界面
    /// </summary>
    /// <param name="panelName">UI界面的名称</param>
    public virtual void OpenPanel(string panelName)
    {
        name = panelName;
    }

    /// <summary>
    /// 关闭UI界面
    /// </summary>
    public virtual void ClosePanel()
    {
        isRemove = true;
        Destroy(gameObject);
    }

    protected void DestroyPanel()
    {
        isRemove = true;
    }
}