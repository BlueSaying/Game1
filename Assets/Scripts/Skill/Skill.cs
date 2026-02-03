using System.Collections.Generic;

public class Skill
{
    public string name;
    public string description;
    public int damage;
    public int MPConsumption;

    public int targetCount;
    public List<Unit> targetList;

    public Skill(string name, string description, int damage, int MPConsumption, int targetCount)
    {
        this.name = name;
        this.description = description;
        this.damage = damage;
        this.MPConsumption = MPConsumption;
        this.targetCount = targetCount;

        targetList = new List<Unit>();
    }
}