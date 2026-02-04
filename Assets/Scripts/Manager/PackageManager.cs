using System.Collections.Generic;
using UnityEngine;

public class PackageManager : MonoBehaviourSingleton<PackageManager>
{
    private List<CombatItemStaticData> combatItemStaticData;
    private List<EquipmentStaticData> equipmentStaticData;
    private List<FaBaoStaticData> faBaoStaticData;
    private List<MiscellaneousStaticData> miscellaneouStaticData;

    public List<CombatItem> combatItem { get; private set; }
    public List<Equipment> equipment { get; private set; }
    public List<FaBao> faBao { get; private set; }
    public List<Miscellaneous> miscellaneous { get; private set; }

    public ItemType chosenItemType;
    public int chosenItemIndex;

    private PackagePanelController packagePanelController;

    protected override void Awake()
    {
        base.Awake();

        LoadItemStaticData();

        ResetPackage();

        // 添加测试数据
        AddTestData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (UIManager.Instance.IsPanelOpened(PanelName.PackagePanel))
            {
                UIManager.Instance.ClosePanel(PanelName.PackagePanel);
                packagePanelController = null;
            }
            else
            {
                packagePanelController = UIManager.Instance.OpenPanel(PanelName.PackagePanel) as PackagePanelController;
            }
        }
    }

    public void ResetPackage()
    {
        combatItem = new List<CombatItem>();
        equipment = new List<Equipment>();
        faBao = new List<FaBao>();
        miscellaneous = new List<Miscellaneous>();
    }

    public CombatItemStaticData GetCombatItemStaticData(string id)
    {
        foreach (var item in combatItemStaticData)
        {
            if (item.id == id) return item;
        }

        return null;
    }
    public EquipmentStaticData GetEquipmentStaticData(string id)
    {
        foreach (var item in equipmentStaticData)
        {
            if (item.id == id) return item;
        }

        return null;
    }
    public FaBaoStaticData GetFaBaoStaticData(string id)
    {
        foreach (var item in faBaoStaticData)
        {
            if (item.id == id) return item;
        }

        return null;
    }
    public MiscellaneousStaticData GetMiscellaneousStaticData(string id)
    {
        foreach (var item in miscellaneouStaticData)
        {
            if (item.id == id) return item;
        }

        return null;
    }

    public void AddCombatItem(CombatItem item)
    {
        combatItem.Add(item);
    }
    public void AddEquipment(Equipment item)
    {
        equipment.Add(item);
    }
    public void AddFabao(FaBao item)
    {
        faBao.Add(item);
    }
    public void AddMiscellaneou(Miscellaneous item)
    {
        miscellaneous.Add(item);
    }

    public void UpdateChosenInfo(int index, ItemType itemType)
    {
        chosenItemIndex = index;
        chosenItemType = itemType;

        UpdateDescription();
    }

    private void UpdateDescription()
    {
        Item item = null;
        switch (chosenItemType)
        {
            case ItemType.CombatItem:
                if (chosenItemIndex < combatItem.Count)
                {
                    item = combatItem[chosenItemIndex];
                }
                break;

            case ItemType.Equipment:
                if (chosenItemIndex < equipment.Count)
                {
                    item = equipment[chosenItemIndex];
                }
                break;

            case ItemType.FaBao:
                if (chosenItemIndex < faBao.Count)
                {
                    item = faBao[chosenItemIndex];
                }
                break;

            case ItemType.Miscellaneous:
                if (chosenItemIndex < miscellaneous.Count)
                {
                    item = miscellaneous[chosenItemIndex];
                }
                break;
        }
        if (item != null)
        {
            packagePanelController.UpdateDescription(item.Name, item.Detail);
        }
    }

    // 从文件读取静态数据
    private void LoadItemStaticData()
    {
        combatItemStaticData = new List<CombatItemStaticData>();
        equipmentStaticData = new List<EquipmentStaticData>();
        faBaoStaticData = new List<FaBaoStaticData>();
        miscellaneouStaticData = new List<MiscellaneousStaticData>();

        UnityTools.WriteDataToList(combatItemStaticData, DataLoader.LoadItemStaticData(ItemType.CombatItem));
        UnityTools.WriteDataToList(equipmentStaticData, DataLoader.LoadItemStaticData(ItemType.Equipment));
        UnityTools.WriteDataToList(faBaoStaticData, DataLoader.LoadItemStaticData(ItemType.FaBao));
        UnityTools.WriteDataToList(miscellaneouStaticData, DataLoader.LoadItemStaticData(ItemType.Miscellaneous));
    }

    private void AddTestData()
    {
        AddMiscellaneou(new Miscellaneous(GetMiscellaneousStaticData("1"), 1));
        AddMiscellaneou(new Miscellaneous(GetMiscellaneousStaticData("1"), 1));
        AddMiscellaneou(new Miscellaneous(GetMiscellaneousStaticData("2"), 1));
        AddMiscellaneou(new Miscellaneous(GetMiscellaneousStaticData("2"), 1));
        AddMiscellaneou(new Miscellaneous(GetMiscellaneousStaticData("2"), 1));
    }
}