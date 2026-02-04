using System.Collections.Generic;
using UnityEngine;

public class CombatItemStaticData : ItemStaticData
{
    // 道具伤害
    public int damage;
}

[CreateAssetMenu(fileName = "CombatItem", menuName = "ScriptableObject/ItemStaticData/CombatItem")]
public class CombatItemSO : ScriptableObject
{
    public TextAsset textAsset;

    // 储存所有敌人的列表
    [SerializeField]
    public List<CombatItemStaticData> data = new List<CombatItemStaticData>();

    private void OnValidate()
    {
        UnityTools.WriteDataToList(data, textAsset);
    }
}

public class CombatItem : Item
{
    public new CombatItemStaticData StaticData { get => base.StaticData as CombatItemStaticData; set => base.StaticData = value; }

    public int Damage => StaticData.damage;

    public CombatItem(CombatItemStaticData staticData, int num)
        : base(staticData, num)
    { }
}