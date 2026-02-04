public enum ItemType
{
    CombatItem,
    Equipment,
    FaBao,
    Miscellaneous,
}

public abstract class ItemStaticData
{
    public string id;
    public string name;
    public string detail;
    public int buyPrice;
    public int sellPrice;
}

public abstract class Item
{
    public ItemStaticData StaticData { get; set; }

    public string ID => StaticData.id;
    public string Name => StaticData.name;
    public string Detail => StaticData.detail;
    public int BuyPrice => StaticData.buyPrice;
    public int SellPrice => StaticData.sellPrice;

    public int num;
    public string uid;

    public Item(ItemStaticData staticData, int num)
    {
        this.StaticData = staticData;
        this.num = num;
    }
}
