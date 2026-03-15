using UnityEngine;
using UnityEngine.Events;

public class UnitModel
{
    public int curHP;
    public int curMP;

    public int maxHP;
    public int maxMP;
    public int mana;
    public int strength;
    public int defence;
    public int speed;

    public string name;

    public UnitModel(int maxHP, int maxMP, int mana, int strength, int defence, int speed, string name)
    {
        curHP = maxHP;
        curMP = maxMP;

        this.maxHP = maxHP;
        this.maxMP = maxMP;
        this.mana = mana;
        this.strength = strength;
        this.defence = defence;
        this.speed = speed;
        this.name = name;
    }

    public virtual void Die()
    {
        curHP = 0;
    }

    public virtual void Revive()
    {
        curHP = maxHP;
        curMP = maxMP;
    }
}

public abstract class Unit : MonoBehaviour
{
    public UnitModel Model { get; protected set; }

    public int CurHP => Model.curHP;
    public int CurMP => Model.curMP;
    public int MaxHP => Model.maxHP;
    public int MaxMP => Model.maxMP;
    public int Mana => Model.mana;
    public int Strength => Model.strength;
    public int Defence => Model.defence;
    public int Speed => Model.speed;
    public string Name => Model.name;
    public new string name => Model.name;

    public bool IsDead { get; protected set; }

    public InfoCanvasController infoCanvasController;

    public UnityAction HideHPInfo => infoCanvasController.HideHPInfo;

    // 收到攻击
    public virtual void TakeDamage(Skill skill, Unit attcker)
    {
        Model.curHP -= skill.damage;
        if (Model.curHP > Model.maxHP) Model.curHP = Model.maxHP;
        if (Model.curHP <= 0)
        {
            Die();
        }

        infoCanvasController.ShowDamageNum(skill.damage);
    }

    // 释放技能
    public virtual void ReleaseSkill(Skill skill, Unit target)
    {
        if (Model.curMP < skill.MPConsumption) return;

        Model.curMP -= skill.MPConsumption;
        target.TakeDamage(skill, target);
    }

    public virtual void StartBattle()
    {
        IsDead = false;
    }

    public virtual void Die()
    {
        Debug.Log(name + "Die!");
        Model.Die();
        IsDead = true;
    }

    public void Revive()
    {
        IsDead = false;
        gameObject.SetActive(true);
        Model.Revive();
    }
}