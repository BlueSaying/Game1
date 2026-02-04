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
        view.AddListenerToSkillButton(OnSkillButtonClicked);
        view.AddListenerToDefenceButton(OnDefenceButtonClicked);
        view.AddListenerToGiveUpButton(OnGiveUpButtonClicked);

        // 只展示一级菜单，隐藏其他菜单
        view.ShowPrimaryMenu();
        view.HidePhysicalAttackMenu();
        view.HideManaAttackMenu();

        UpdatePlayerInfo();

        EventCenter.Instance.RegisterEvent(EventType.OnPlayerInfoChanged, UpdatePlayerInfo);
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
        EventCenter.Instance.RemoveEvent(EventType.OnPlayerInfoChanged, UpdatePlayerInfo);
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

    public void UpdatePlayerInfo()
    {
        PlayerModel model = PlayerManager.Instance.PlayerModel;

        view.UpdateHPInfo(model.curHP, model.maxHP);
        view.UpdateMPInfo(model.curMP, model.maxMP);
    }

    #region 事件集
    private void OnPhysicalAttackButtonClicked()
    {
        view.ShowPhysicalAttackMenu();
        view.HidePrimaryMenu();
        view.UpdatePhysicalAttackMenu(PlayerManager.Instance.PlayerModel.physcialSkills);
    }
    private void OnManaAttackButtonClicked()
    {
        view.ShowManaAttackMenu();
        view.HidePrimaryMenu();
        view.UpdateManaAttackMenu(PlayerManager.Instance.PlayerModel.manaSkills);
    }
    private void OnSkillButtonClicked()
    {

    }
    private void OnDefenceButtonClicked()
    {

    }
    private void OnGiveUpButtonClicked()
    {
        CameraManager.Instance.SwitchToPlayerFollowCamera();
        PlayerManager.Instance.UnlockMove();

        UIManager.Instance.ClosePanel(this.name);
    }
    #endregion
}