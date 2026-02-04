using System.Collections.Generic;
using UnityEngine;

public class MiscellaneousStaticData : ItemStaticData
{
}

[CreateAssetMenu(fileName = "Miscellaneous", menuName = "ScriptableObject/ItemStaticData/Miscellaneous")]
public class MiscellaneousSO : ScriptableObject
{
    public TextAsset textAsset;

    // 储存所有敌人的列表
    [SerializeField]
    public List<MiscellaneousStaticData> data = new List<MiscellaneousStaticData>();

    private void OnValidate()
    {
        UnityTools.WriteDataToList(data, textAsset);
    }
}

public class Miscellaneous : Item
{
    public new MiscellaneousStaticData StaticData { get => base.StaticData as MiscellaneousStaticData; set => base.StaticData = value; }

    public Miscellaneous(MiscellaneousStaticData staticData, int num)
        : base(staticData, num)
    {
    }
}