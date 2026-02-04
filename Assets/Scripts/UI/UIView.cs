using UnityEngine;

public enum PanelName
{
    /// <summary>
    /// 对话面板
    /// </summary>
    DialogPanel,

    /// <summary>
    /// 主菜单
    /// </summary>
    MainMenu,

    /// <summary>
    /// 开始战斗前，战斗详情界面
    /// </summary>
    EnemyInfoBeforeBattlePanel,

    /// <summary>
    /// 战斗界面
    /// </summary>
    BattlePanel,

    /// <summary>
    /// 背包界面
    /// </summary>
    PackagePanel,
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