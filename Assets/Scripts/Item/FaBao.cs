using System.Collections.Generic;
using UnityEngine;

public class FaBaoStaticData : ItemStaticData
{

}

[CreateAssetMenu(fileName = "FaBao", menuName = "ScriptableObject/ItemStaticData/FaBao")]
public class FaBaoSO : ScriptableObject
{
    public TextAsset textAsset;

    // 储存所有敌人的列表
    [SerializeField]
    public List<FaBaoStaticData> data = new List<FaBaoStaticData>();

    private void OnValidate()
    {
        UnityTools.WriteDataToList(data, textAsset);
    }
}

public class FaBao : Item
{
    public new FaBaoStaticData StaticData { get => base.StaticData as FaBaoStaticData; set => base.StaticData = value; }

    public FaBao(FaBaoStaticData staticData, int num)
        : base(staticData, num)
    {
    }

}