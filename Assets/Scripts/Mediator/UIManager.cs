using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    //根节点
    private Transform _uiRoot;
    public Transform UIRoot
    {
        get
        {
            if (_uiRoot == null) _uiRoot = GameObject.Find("Canvas").transform;
            if (_uiRoot == null) throw new System.Exception("未找到Canvas!");
            return _uiRoot;
        }
    }

    // 储存已经打开界面的缓存字典
    public Dictionary<string, UIController> openedPanels;

    private UIManager()
    {
        EventCenter.Instance.RegisterEvent(EventType.OnSceneSwitchStart, () =>
        {
            openedPanels.Clear();
        });

        openedPanels = new Dictionary<string, UIController>();
    }

    // 检测UI界面是否被打开
    public bool IsPanelOpened(string panelName)
    {
        return openedPanels.ContainsKey(panelName);
    }
    public bool IsPanelOpened(PanelName panelName)
    {
        return IsPanelOpened(panelName.ToString());
    }

    // 获取界面
    public UIController GetPanel(string panelName)
    {
        if (!IsPanelOpened(panelName)) return null;

        return openedPanels[panelName];
    }
    public UIController GetPanel(PanelName panelName)
    {
        return GetPanel(panelName.ToString());
    }

    /// <summary>
    /// 打开UI界面
    /// </summary>
    /// <returns>打开的界面的Base_Panel</returns>
    public UIController OpenPanel(string panelName)
    {
        //检查是否已经打开
        if (IsPanelOpened(panelName))
        {
            Debug.Log("界面已打开" + panelName);
            return null;
        }

        GameObject panelPrefab = ResourcesLoader.Instance.LoadPanel(panelName);
        UIController controller = GameObject.Instantiate(panelPrefab, UIRoot, false).GetComponent<UIController>();
        if (controller == null)
        {
            Debug.LogWarning(controller.ToString() + "未添加UIController的子类");
        }

        controller.OpenPanel(panelName);

        openedPanels.Add(panelName, controller);

        return controller;
    }
    public UIController OpenPanel(PanelName panelName)
    {
        return OpenPanel(panelName.ToString());
    }

    /// <summary>
    /// 关闭UI界面
    /// </summary>
    /// <param name="panelName">要关闭的UI界面的名称</param>
    /// <returns>是否成功打开UI界面</returns>
    public bool ClosePanel(string panelName)
    {
        UIController controller = null;
        if (!openedPanels.TryGetValue(panelName, out controller)) // 检测要关闭的界面有没有被打开
        {
            Debug.LogError("界面未打开" + panelName);
            return false;
        }

        openedPanels.Remove(panelName);
        controller.ClosePanel();
        return true;
    }
    public bool ClosePanel(PanelName panelName)
    {
        return ClosePanel(panelName.ToString());
    }

    /// <summary>
    /// 将UI面板上移一层
    /// </summary>
    public void MovePanelUp(UIController controller)
    {
        int siblingCount = controller.transform.parent.childCount;
        int siblingIndex = controller.transform.GetSiblingIndex();
        if (siblingIndex < siblingCount - 1) controller.transform.SetSiblingIndex(siblingIndex + 1);
    }

    /// <summary>
    /// 将UI面板下移一层
    /// </summary>
    public void MovePanelDown(UIController controller)
    {
        int siblingIndex = controller.transform.GetSiblingIndex();
        if (siblingIndex > 0) controller.transform.SetSiblingIndex(siblingIndex - 1);
    }

    /// <summary>
    /// 将UI面板移动到最前面
    /// </summary>
    public void MovePanelToFront(UIController controller)
    {
        controller.transform.SetAsLastSibling();
    }

    /// <summary>
    /// 将UI面板移动到最后面
    /// </summary>
    public void MovePanelToBack(UIController controller)
    {
        controller.transform.SetAsFirstSibling();
    }
}