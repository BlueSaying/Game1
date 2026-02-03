using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    private float InitDistance => BattleManager.InitDistance;
    private int BattleQueueCount => BattleManager.BattleQueueCount;

    // 用于当玩家进入时的镜头聚焦
    public CinemachineVirtualCamera enemyFocusCamera;
    public CinemachineVirtualCamera battleCamera;

    public Transform playerPositioning;
    public Transform enemies;

    public List<Unit> allUnits;
    private List<float> remainingDistance;
    private List<float> remainingTime;

    private bool isSelectingTarget;
    private int selectingTargetIndex;
    private PlayerSkill selectedPlayerSkill;

    public List<Unit> BattleQueue { get; private set; }

    public int CurTurn { get; private set; }

    void Start()
    {
        GetComponent<TriggerDetector>().AddTriggerListener(TriggerEventType.OnTriggerEnter, "PlayerTrigger", (obj) =>
        {
            CameraManager.Instance.SwitchCamera(enemyFocusCamera);

            var c = UIManager.Instance.OpenPanel(PanelName.EnemyInfoBeforeBattlePanel) as EnemyInfoBeforeBattlePanelController;
            c.Init(this);

            PlayerManager.Instance.LockMove();
        });
    }

    void Update()
    {
        if (isSelectingTarget)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectingTargetIndex = (selectingTargetIndex + 1) % allUnits.Count;
                while (allUnits[selectingTargetIndex].IsDead)
                {
                    selectingTargetIndex = (selectingTargetIndex + 1) % allUnits.Count;
                }
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selectingTargetIndex = (selectingTargetIndex - 1 + allUnits.Count) % allUnits.Count;
                while (allUnits[selectingTargetIndex].IsDead)
                {
                    selectingTargetIndex = (selectingTargetIndex + 1) % allUnits.Count;
                }
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                BattleManager.Instance.ReleasePlayerSkill(selectedPlayerSkill, PlayerManager.Instance.PlayerUnit, allUnits[selectingTargetIndex]);
                isSelectingTarget = false;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {

            }
            UpdateSelectedArrow();
        }
    }

    public void SelectTargetAfterSkillSelected(PlayerSkill playerSkill)
    {
        isSelectingTarget = true;
        selectedPlayerSkill = playerSkill;

        selectingTargetIndex = 0;
        while (allUnits[selectingTargetIndex].IsDead)
        {
            selectingTargetIndex = (selectingTargetIndex + 1) % allUnits.Count;
        }
    }

    private void UpdateSelectedArrow()
    {
        foreach (var unit in allUnits)
        {
            unit.infoCanvasController.HideSelectedArrow();
        }

        if (isSelectingTarget)
        {
            allUnits[selectingTargetIndex].infoCanvasController.ShowSelectedArrow();
        }
    }

    public void StartBattle()
    {
        PlayerManager.Instance.SetPlayerTransform(playerPositioning);
        CameraManager.Instance.SwitchCamera(battleCamera);

        // 开始战斗后，将玩家和敌人加入units中
        allUnits = new List<Unit>();
        for (int i = 0; i < enemies.childCount; i++)
        {
            allUnits.Add(enemies.GetChild(i).GetComponent<Unit>());
        }
        Debug.Log("本次战斗共有" + allUnits.Count.ToString() + "个敌人");
        allUnits.Add(PlayerManager.Instance.PlayerController.GetComponent<PlayerUnit>());

        CurTurn = 0;

        BattleQueue = new List<Unit>();
        for (int i = 0; i < BattleQueueCount; i++)
        {
            BattleQueue.Add(null);
        }

        remainingDistance = new List<float>();
        remainingTime = new List<float>();
        for (int i = 0; i < allUnits.Count; i++)
        {
            remainingDistance.Add(InitDistance);
            remainingTime.Add(InitDistance / allUnits[i].Speed);
        }

        // 所有敌人亮出血条
        foreach (var unit in allUnits)
        {
            if (unit is EnemyUnit enemyUnit)
            {
                enemyUnit.infoCanvasController.ShowHPInfo();
                enemyUnit.infoCanvasController.UpdateHPInfo(enemyUnit.CurHP, enemyUnit.MaxHP);
            }
        }

        // 通知所有unit战斗开始
        foreach (var unit in allUnits)
        {
            unit.StartBattle();
        }
    }

    public void NextTurn()
    {
        CurTurn++;
        CalcBattleQueue();
    }

    public void QuitBattle()
    {
        gameObject.SetActive(false);
    }

    private void CalcBattleQueue()
    {
        List<float> tempRemainingDistance = new List<float>();
        for (int i = 0; i < BattleQueueCount; i++)
        {
            // 所需时间最短的单位的索引
            int min = 0;
            while (allUnits[min].IsDead) min++; // 避免找到已经死亡的单位

            for (int j = 0; j < allUnits.Count; j++)
            {
                if (allUnits[j].IsDead) continue;
                if (remainingTime[j] < remainingTime[min])
                {
                    min = j;
                }
            }

            for (int j = 0; j < allUnits.Count; j++)
            {
                if (j == min || allUnits[j].IsDead) continue;
                remainingDistance[j] -= remainingTime[min] * allUnits[j].Speed;
                remainingTime[j] = remainingDistance[j] / allUnits[j].Speed;
            }
            remainingDistance[min] = InitDistance;
            remainingTime[min] = InitDistance / allUnits[min].Speed;

            BattleQueue[i] = allUnits[min];
            if (i == 0) tempRemainingDistance = new List<float>(remainingDistance);
        }

        remainingDistance = new List<float>();
        remainingTime = new List<float>();
        for (int i = 0; i < allUnits.Count; i++)
        {
            remainingDistance.Add(tempRemainingDistance[i]);
            remainingTime.Add(tempRemainingDistance[i] / allUnits[i].Speed);
        }
    }

    #region 事件集
    public void OnBattleStart()
    {

    }
    #endregion
}