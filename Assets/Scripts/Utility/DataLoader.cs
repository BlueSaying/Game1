using UnityEngine;

public static class DataLoader
{
    // 数据
    private static readonly string DataPath = "Datas/";

    private static readonly string DialoguePath = "Dialogues/";
    private static readonly string ItemStaticDataPath = "ItemStaticData/";

    public static TextAsset LoadItemStaticData(ItemType itemType)
    {
        return Resources.Load<TextAsset>(DataPath + ItemStaticDataPath + itemType);
    }

    public static TextAsset LoadDialogue(DialogueName dialogueName)
    {
        return Resources.Load<TextAsset>(DataPath + DialoguePath + dialogueName);
    }
}
