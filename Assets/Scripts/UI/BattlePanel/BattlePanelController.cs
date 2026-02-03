using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattlePanelController : UIController
{
    public BattlePanelView view;

    protected override void Awake()
    {
        base.Awake();

        view.AddListenerToPhysicalAttackButton(OnPhysicalAttackButtonClicked);
        view.AddListenerToManaAttackButton(OnManaAttackButtonClicked);
        view.AddListenerToskillButton(OnskillButtonClicked);
        view.AddListenerToDefenceButton(OnDefenceButtonClicked);
        view.AddListenerToGiveUpButton(OnGiveUpButtonClicked);

        // 只展示一级菜单，隐藏其他菜单
        view.ShowPrimaryMenu();
        view.HidePhysicalAttackMenu();
        view.HideManaAttackMenu();

        // Test
        //view.UpdateHPInfo(25, 50);
        //view.UpdateMPInfo(100, 100);
        UpdatePlayerInfo(PlayerManager.Instance.PlayerModel);
    }

    public void UpdateBattleQueue(List<Unit> battleQueue)
    {
        view.UpdateBattleQueue(battleQueue);
        if (battleQueue.FirstOrDefault() is PlayerUnit)
        {
            view.ShowPrimaryMenu();
        }
        else
        {
            view.HidePrimaryMenu();
        }
    }

    public void UpdatePlayerInfo(PlayerModel playerModel)
    {
        view.UpdateHPInfo(playerModel.curHP, playerModel.maxHP);
        view.UpdateMPInfo(playerModel.curMP, playerModel.maxMP);
    }

    #region 事件集
    public void OnPhysicalAttackButtonClicked()
    {
        view.ShowPhysicalAttackMenu();
        view.HidePrimaryMenu();
        view.UpdatePhysicalAttackMenu(PlayerManager.Instance.PlayerModel.physcialSkills);
    }
    public void OnManaAttackButtonClicked()
    {
        view.ShowManaAttackMenu();
        view.HidePrimaryMenu();
        view.UpdateManaAttackMenu(PlayerManager.Instance.PlayerModel.manaSkills);
    }
    public void OnskillButtonClicked()
    {

    }
    public void OnDefenceButtonClicked()
    {

    }
    public void OnGiveUpButtonClicked()
    {
        CameraManager.Instance.SwitchToPlayerFollowCamera();
        PlayerManager.Instance.UnlockMove();

        UIManager.Instance.ClosePanel(this.name);
    }
    #endregion
}