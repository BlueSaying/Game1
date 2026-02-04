using System.Collections.Generic;
using UnityEngine;

public class EquipmentStaticData : ItemStaticData
{
    public int initHP;
    public int initMP;
    public int initMana;
    public int initStrength;
    public int initDefence;
    public int initSpeed;
}

[CreateAssetMenu(fileName = "Equipment", menuName = "ScriptableObject/ItemStaticData/Equipment")]
public class EquipmentSO : ScriptableObject
{
    public TextAsset textAsset;

    // 储存所有敌人的列表
    [SerializeField]
    public List<EquipmentStaticData> data = new List<EquipmentStaticData>();

    private void OnValidate()
    {
        UnityTools.WriteDataToList(data, textAsset);
    }
}

public class Equipment : Item
{
    public new EquipmentStaticData StaticData { get => base.StaticData as EquipmentStaticData; set => base.StaticData = value; }

    public int InitHP => StaticData.initHP;
    public int InitMP => StaticData.initMP;
    public int InitMana => StaticData.initMana;
    public int InitStrength => StaticData.initStrength;
    public int InitDefence => StaticData.initDefence;
    public int InitSpeed => StaticData.initSpeed;

    public Equipment(EquipmentStaticData staticData, int num)
        : base(staticData, num)
    {
    }
}