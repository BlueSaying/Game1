using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattlePanelView : UIView
{
    // 战斗队列
    public RectTransform battleQueue;

    // 一级菜单
    public RectTransform primaryMenu;
    public Button physicalAttackButton;
    public Button manaAttackButton;
    public Button skillButton;
    public Button defenceButton;
    public Button giveUpButton;

    // 物理攻击菜单
    public RectTransform physicalAttackMenu;

    // 魔法攻击菜单
    public RectTransform manaAttackMenu;

    // 玩家信息
    public RectTransform healthBar;
    public RectTransform manaBar;

    protected override void DOClosePanel()
    {
        base.DOClosePanel();

        DestroyPanel();
    }

    #region PlayerInfoPanel
    public void UpdateHPInfo(int newHP, int maxHP)
    {
        var slider = healthBar.GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = maxHP;
        StartCoroutine(TransBar(slider, newHP));

        healthBar.Find("HPText").GetComponent<TMP_Text>().text = newHP.ToString() + " / " + maxHP.ToString();
    }

    public void UpdateMPInfo(int newMP, int maxMP)
    {
        var slider = manaBar.GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = maxMP;
        StartCoroutine(TransBar(slider, newMP));

        manaBar.Find("MPText").GetComponent<TMP_Text>().text = newMP.ToString() + " / " + maxMP.ToString();
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
    #endregion

    public void UpdatePhysicalAttackMenu(List<PlayerSkill> playerSkills)
    {
        int i = 0;
        for (; i < playerSkills.Count && i < physicalAttackMenu.childCount; i++)
        {
            Transform button = physicalAttackMenu.GetChild(i);
            PlayerSkill playerSkill = playerSkills[i];

            button.gameObject.SetActive(true);
            button.Find("Text").GetComponent<TMP_Text>().text = playerSkill.name;

            // 先清空事件再添加
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                HidePhysicalAttackMenu();
                BattleManager.Instance.CurBattle.SelectTargetAfterSkillSelected(playerSkill);
            });
        }
        for (; i < physicalAttackMenu.childCount; i++)
        {
            physicalAttackMenu.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void UpdateManaAttackMenu(List<PlayerSkill> playerSkills)
    {
        int i = 0;
        for (; i < playerSkills.Count && i < manaAttackMenu.childCount; i++)
        {
            var button = manaAttackMenu.GetChild(i);
            PlayerSkill playerSkill = playerSkills[i];

            button.gameObject.SetActive(true);
            button.Find("Text").GetComponent<TMP_Text>().text = playerSkills[i].name;
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                HidePhysicalAttackMenu();
                BattleManager.Instance.CurBattle.SelectTargetAfterSkillSelected(playerSkill);
            });
        }
        for (; i < manaAttackMenu.childCount; i++)
        {
            manaAttackMenu.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void UpdateBattleQueue(List<Unit> units)
    {
        for (int i = 0; i < units.Count; i++)
        {
            battleQueue.GetChild(i).GetComponent<Image>().sprite = ResourcesLoader.Instance.LoadSprite(SpriteType.UnitIcon, units[i].Name);
        }
    }

    #region 展示与隐藏界面
    public void ShowPrimaryMenu()
    {
        primaryMenu.gameObject.SetActive(true);
    }

    public void HidePrimaryMenu()
    {
        primaryMenu.gameObject.SetActive(false);
    }

    public void ShowPhysicalAttackMenu()
    {
        physicalAttackMenu.gameObject.SetActive(true);
    }

    public void HidePhysicalAttackMenu()
    {
        physicalAttackMenu.gameObject.SetActive(false);
    }

    public void ShowManaAttackMenu()
    {
        manaAttackMenu.gameObject.SetActive(true);
    }

    public void HideManaAttackMenu()
    {
        manaAttackMenu.gameObject.SetActive(false);
    }
    #endregion

    #region 为按钮绑定事件
    public void AddListenerToPhysicalAttackButton(UnityAction action)
    {
        physicalAttackButton.onClick.AddListener(action);
    }
    public void AddListenerToManaAttackButton(UnityAction action)
    {
        manaAttackButton.onClick.AddListener(action);
    }
    public void AddListenerToSkillButton(UnityAction action)
    {
        skillButton.onClick.AddListener(action);
    }
    public void AddListenerToDefenceButton(UnityAction action)
    {
        defenceButton.onClick.AddListener(action);
    }
    public void AddListenerToGiveUpButton(UnityAction action)
    {
        giveUpButton.onClick.AddListener(action);
    }
    #endregion

    
}