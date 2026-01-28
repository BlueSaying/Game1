using System.Collections.Generic;
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
    public bool IsPanelOpened(string panelname)
    {
        return openedPanels.ContainsKey(panelname);
    }

    // 获取界面
    public UIController GetPanel(string panelname)
    {
        if (!IsPanelOpened(panelname)) return null;

        return openedPanels[panelname];
    }

    /// <summary>
    /// 打开UI界面
    /// </summary>
    /// <returns>打开的界面的Base_Panel</returns>
    public UIController OpenPanel(PanelName panelName)
    {
        string name = panelName.ToString();

        //检查是否已经打开
        if (IsPanelOpened(name))
        {
            Debug.Log("界面已打开" + name);
            return null;
        }

        GameObject panelPrefab = ResourcesLoader.Instance.LoadPanel(name);
        UIController controller = GameObject.Instantiate(panelPrefab, UIRoot, false).GetComponent<UIController>();
        if (controller == null)
        {
            Debug.LogWarning(controller.ToString() + "未添加UIController的子类");
        }

        controller.OpenPanel(name);
        Debug.Log(name);
        openedPanels.Add(name, controller);

        return controller;
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
}