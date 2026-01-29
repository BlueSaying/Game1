using UnityEngine;

public enum PanelName
{
    DialogPanel,
    MainMenu,
}

/// <summary>
/// UI MVC View基类
/// </summary>
public abstract class UIView : MonoBehaviour
{
    protected virtual void Awake() { }

    protected virtual void Start() { }

    protected virtual void Update() { }

    /// <summary>
    /// 打开UI界面
    /// </summary>
    /// <param name="panelName">UI界面的名称</param>
    public virtual void OpenPanel()
    {
        DOOpenPanel();
    }

    /// <summary>
    /// 关闭UI界面
    /// </summary>
    public virtual void ClosePanel()
    {
        DOClosePanel();
    }

    protected virtual void DOOpenPanel() { }

    protected virtual void DOClosePanel() { }

    protected void DestroyPanel()
    {
        Destroy(gameObject);
    }
}