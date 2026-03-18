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
    private List<Battle> allBattles;

    BattlePanelController BattlePanel => UIManager.Instance.GetPanel(PanelName.BattlePanel) as BattlePanelController;

    protected override void Awake()
    {
        base.Awake();
        EventCenter.Instance.RegisterEvent(EventType.OnSceneSwitchComplete, () =>
        {
            allBattles = new List<Battle>();

            GameObject go = GameObject.Find("-----Battles");
            if (go != null)
            {
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    allBattles.Add(go.transform.GetChild(i).GetComponent<Battle>());
                }
            }
        });

        EventCenter.Instance.RegisterEvent(EventType.OnPlayerGiveUpBattle, () =>
        {
            CurBattle.RefreshUnits();
        });
    }

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
            DefeatThenQuitBattle();
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
            VictoryThenQuitBattle();
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


    // 战斗失败，退出战斗
    private void DefeatThenQuitBattle()
    {
        if (!isBattling) return;
        isBattling = false;

        CurBattle.DefeatThenQuitBattle();
        CameraManager.Instance.SwitchToPlayerFollowCamera();
        PlayerManager.Instance.UnFreezePlayerMove();
        UIManager.Instance.ClosePanel(PanelName.BattlePanel);
    }

    // 战斗胜利，退出战斗
    private void VictoryThenQuitBattle()
    {
        if (!isBattling) return;
        isBattling = false;

        CurBattle.VictoryThenQuitBattle();
        CameraManager.Instance.SwitchToPlayerFollowCamera();
        PlayerManager.Instance.UnFreezePlayerMove();
        UIManager.Instance.ClosePanel(PanelName.BattlePanel);
    }
}