using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PackagePanelView : UIView
{
    public RectTransform playerInfo;

    public RectTransform inventoryBar;
    public RectTransform content;

    public RectTransform description;

    #region PlayerInfo
    public void UpdatePlayerInfoHP(int newHP, int maxHP)
    {
        var slider = playerInfo.Find("HPBar").GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = maxHP;
        StartCoroutine(TransBar(slider, newHP));
        playerInfo.Find("HPBar").Find("HPText").GetComponent<TMP_Text>().text = newHP.ToString() + " / " + maxHP.ToString();
    }

    public void UpdatePlayerInfoMP(int newMP, int maxMP)
    {
        var slider = playerInfo.Find("MPBar").GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = maxMP;
        StartCoroutine(TransBar(slider, newMP));
        playerInfo.Find("MPBar").Find("MPText").GetComponent<TMP_Text>().text = newMP.ToString() + " / " + maxMP.ToString();
    }

    private IEnumerator TransBar(Slider slider, int newValue)
    {
        float timer = 0.0f;
        float duration = 0.2f;
        float curValue = slider.value;

        while (timer < duration)
        {
            slider.value = Mathf.Lerp(curValue, newValue, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        slider.value = newValue;
        yield return null;
    }

    public void UpdatePlayerInfoMana(int mana)
    {
        playerInfo.Find("VerticalLayoutGroup").Find("Mana").GetComponent<TMP_Text>().text = "法力   " + mana;
    }
    public void UpdatePlayerInfoStrength(int strength)
    {
        playerInfo.Find("VerticalLayoutGroup").Find("Strength").GetComponent<TMP_Text>().text = "力量   " + strength;
    }
    public void UpdatePlayerInfoDefence(int defence)
    {
        playerInfo.Find("VerticalLayoutGroup").Find("Defence").GetComponent<TMP_Text>().text = "防御   " + defence;
    }
    public void UpdatePlayerInfoSpeed(int speed)
    {
        playerInfo.Find("VerticalLayoutGroup").Find("Speed").GetComponent<TMP_Text>().text = "速度   " + speed;
    }
    #endregion

    #region Inventory
    public void UpdateItemScroll<T>(List<T> items) where T : Item
    {
        // 先清理滚动容器中原本的物体
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }

        //从本地数据中读取背包数据，然后加载到UI上
        for (int i = 0; i < items.Count; i++)
        {
            GameObject itemPrefab = ResourcesLoader.Instance.LoadPanel("ItemCell");
            GameObject itemCell = GameObject.Instantiate(itemPrefab, content);
            itemCell.GetComponent<ItemCell>().Init(i, items[i]);
        }
    }

    public void AddListenerToEquipmentButton(UnityAction action)
    {
        inventoryBar.Find("Equipment").GetComponent<Button>().onClick.AddListener(action);
    }
    public void AddListenerToFaBaoButton(UnityAction action)
    {
        inventoryBar.Find("FaBao").GetComponent<Button>().onClick.AddListener(action);
    }
    public void AddListenerToCombatItemButton(UnityAction action)
    {
        inventoryBar.Find("CombatItem").GetComponent<Button>().onClick.AddListener(action);
    }
    public void AddListenerToMiscellaneousButton(UnityAction action)
    {
        inventoryBar.Find("Miscellaneous").GetComponent<Button>().onClick.AddListener(action);
    }
    #endregion


    public void UpdateItemName(string name)
    {
        description.Find("ItemName").GetComponent<TMP_Text>().text = name;
    }
    public void UpdateItemDetail(string detail)
    {
        description.Find("ItemDetail").GetComponent<TMP_Text>().text = detail;
    }
}