using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 所有战斗的名称
/// </summary>
public enum BattleName
{
    Battle1
}

public class BattleManager : MonoBehaviourSingleton<BattleManager>
{
    public const float InitDistance = 10000.0f;
    public const int BattleQueueCount = 8;

    private bool isBattling;

    // 当前处于哪个战斗点
    public Battle CurBattle { get; private set; }
    public List<Unit> AllUnits => CurBattle.allUnits;

    BattlePanelController BattlePanel => UIManager.Instance.GetPanel(PanelName.BattlePanel) as BattlePanelController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) NextTurn();
    }

    public void StartBattle(Battle battle)
    {
        isBattling = true;
        CurBattle = battle;
        battle.StartBattle();

        // UI
        UIManager.Instance.OpenPanel(PanelName.BattlePanel);

        EventCenter.Instance.NotifyEvent(EventType.OnBattleStart);

        // 开始第一回合
        NextTurn();
    }

    public void NextTurn()
    {
        // 判断当前战斗是否已经结束
        if (PlayerManager.Instance.PlayerUnit.IsDead)
        {
            Debug.Log("玩家死亡");
            QuitBattle();
            return;
        }
        bool isEnemiesAllDead = true;
        foreach (var unit in AllUnits)
        {
            if (unit is EnemyUnit && !unit.IsDead)
            {
                isEnemiesAllDead = false;
            }
        }
        if (isEnemiesAllDead)
        {
            Debug.Log("玩家胜利");
            QuitBattle();
            return;
        }

        CurBattle.NextTurn();
        BattlePanel?.UpdateBattleQueue(CurBattle.BattleQueue);

        // 如果当前是敌人行动，那么由BattleManager调用行动
        // HACK
        if (CurBattle.BattleQueue.FirstOrDefault() is EnemyUnit enemyUnit)
        {
            EnemySkill enemySkill = enemyUnit.Model.skills[0];//HACK
            Unit target = PlayerManager.Instance.PlayerUnit;
            UnityTools.WaitThenCallFun(this, 1.0f, () =>
            {
                enemyUnit.ReleaseSkill(enemySkill, target);
                Debug.Log(enemyUnit.name + "正在向" + target.name + "释放" + enemySkill.name);
                NextTurn();
            });
        }
    }

    public void ReleasePlayerSkill(PlayerSkill playerSkill, PlayerUnit attacker, Unit target)
    {
        PlayerManager.Instance.PlayerUnit.ReleaseSkill(playerSkill, target);
        Debug.Log(attacker.name + "正在向" + target.name + "释放" + playerSkill.name);
        NextTurn();
    }


    public void QuitBattle()
    {
        if (!isBattling) return;
        isBattling = false;

        CurBattle.QuitBattle();
        CameraManager.Instance.SwitchToPlayerFollowCamera();
        PlayerManager.Instance.UnlockMove();
        UIManager.Instance.ClosePanel(PanelName.BattlePanel);
    }

    #region 事件集
    public void OnBattleStart()
    {

    }
    #endregion
}