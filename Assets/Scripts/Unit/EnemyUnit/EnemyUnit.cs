using System.Collections.Generic;

public class EnemyModel : UnitModel
{
    public List<EnemySkill> skills;

    public EnemyModel(int maxHP, int maxMP, int mana, int strength, int defence, int speed, string name)
        : base(maxHP, maxMP, mana, strength, defence, speed, name)
    {
        skills = new List<EnemySkill>();

        // HACK
        skills.Add(new EnemyKickSkill("EnemyKick", "KickPlayer", 10, 0, 1));
    }
}

public class EnemyUnit : Unit
{
    public new EnemyModel Model { get => base.Model as EnemyModel; set => base.Model = value; }

    void Awake()
    {
        Model = new EnemyModel(40, 0, 1, 1, 1, 99, "Enemy");
    }

    public override void TakeDamage(Skill skill, Unit attcker)
    {
        base.TakeDamage(skill, attcker);
        infoCanvasController.UpdateHPInfo(CurHP, MaxHP);
    }

    public override void ReleaseSkill(Skill skill, Unit target)
    {
        base.ReleaseSkill(skill, target);
    }
}