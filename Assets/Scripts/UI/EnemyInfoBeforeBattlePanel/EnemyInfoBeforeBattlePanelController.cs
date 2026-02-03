using UnityEngine;

public class EnemyInfoBeforeBattlePanelController : UIController
{
    private Battle battle;

    public EnemyInfoBeforeBattlePanelView view;

    protected override void Awake()
    {
        base.Awake();

        view.AddListenerToStartBattleButton(OnStartBattleButtonClicked);
        view.AddListenerToQuitBattleButton(OnQuitBattleButtonClicked);
    }

    public void Init(Battle battle)
    {
        this.battle = battle;
    }

    public void UpdateText(string enemyName, string enemyDetail)
    {
        view.UpdateText(enemyName, enemyDetail);
    }

    #region 事件集
    public void OnStartBattleButtonClicked()
    {
        UIManager.Instance.ClosePanel(this.name);
        BattleManager.Instance.StartBattle(battle);
    }

    public void OnQuitBattleButtonClicked()
    {
        CameraManager.Instance.SwitchToPlayerFollowCamera();
        PlayerManager.Instance.UnlockMove();

        UIManager.Instance.ClosePanel(this.name);
    }
    #endregion
}