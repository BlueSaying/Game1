using System.Collections.Generic;

public class PlayerModel : UnitModel
{
    public List<PlayerSkill> physcialSkills;
    public List<PlayerSkill> manaSkills;

    //private event UnityAction<PlayerModel> onPlayerModelChanged;

    public PlayerModel(int maxHP, int maxMP, int mana, int strength, int defence, int speed, string name)
        : base(maxHP, maxMP, mana, strength, defence, speed, name)
    {
        physcialSkills = new List<PlayerSkill>();
        manaSkills = new List<PlayerSkill>();

        // HACK
        physcialSkills.Add(new SlashOnceSkill("SlashOnce", "SlashOnceToEnemy", 20, 0, 1));
    }

    //public void AddListener(UnityAction<PlayerModel> func)
    //{
    //    onPlayerModelChanged += func;
    //}
    //
    //public void RemoveListener(UnityAction<PlayerModel> func)
    //{
    //    onPlayerModelChanged -= func;
    //}
    //
    //public void LevelUp()
    //{
    //    onPlayerModelChanged?.Invoke(this);
    //}
}

public class PlayerUnit : Unit
{
    public new PlayerModel Model { get => base.Model as PlayerModel; set => base.Model = value; }

    private void Awake()
    {
        Model = new PlayerModel(100, 50, 1, 1, 1, 200, "Player");
    }

    public override void TakeDamage(Skill skill, Unit attcker)
    {
        base.TakeDamage(skill, attcker);

        (UIManager.Instance.GetPanel(PanelName.BattlePanel) as BattlePanelController).UpdatePlayerInfo(Model);
    }

    public override void ReleaseSkill(Skill skill, Unit target)
    {
        base.ReleaseSkill(skill, target);

        (UIManager.Instance.GetPanel(PanelName.BattlePanel) as BattlePanelController).UpdatePlayerInfo(Model);
    }
}