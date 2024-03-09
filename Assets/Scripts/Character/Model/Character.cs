using System;
using UnityEngine;

//Добавить реген
public abstract class Character : ScriptableObject
{
    [SerializeField]
    protected Health health;

    [SerializeField]
    protected Characteristics characteristics;

    public event Action<bool> OnDeath;
    public event Action<float> OnHealthReducedByDamage;

    private bool dead = false;

    private Skill currentDamageSkill;

    protected Character(Health health, Characteristics characteristics)
    {
        this.health = health;
        this.characteristics = characteristics;
    }

    public void AssignCurrentSkill(Skill skill)
    {
        if (skill.IsDamageSkill())
        {
            currentDamageSkill = skill;
        }
    }

    public virtual bool ReceiveDamage(Hit hit)
    {
        health.ReduceOnDamage(hit, GetResistance());
        OnHealthReducedByDamage?.Invoke(health.CalculatereducedHealthPercentage());

        if (health.IsLessOrEqualsZero())
        {
            Die();
        }

        return dead;
    }

    protected virtual Resistance GetResistance() => characteristics.Resistance;

    public virtual void Die()
    {
        dead = true;
        OnDeath?.Invoke(dead);
    }

    public virtual Hit Damage()
    {
        return characteristics.HitDamage();
    }

    public Hit SkillDamage()
    {
        if (currentDamageSkill != null)
        {
            var increasedDamage = currentDamageSkill.Damage(this);
            currentDamageSkill = null;

            return increasedDamage.Hit();
        }
        return characteristics.HitDamage();
    }

    public float CurrentHealth
    {
        get
        {
            return health.CalculatereducedHealthPercentage();
        }
    }

    public float Intelligence { get { return characteristics.Intelligence; } }


    public float Strength
    {
        get { return characteristics.Strength; }
    }

    public float Dexterity { get { return characteristics.Dexterity; } }

    public bool IsDead() => dead;

    public Damage IncreaseDamage(float damageCoefficient)
    {
        return characteristics.Damage * damageCoefficient;
    }

    public Character Copy()
    {
        Health copiedHealth = Instantiate(health);
        Characteristics copiedCharacteristics = Instantiate(characteristics);
        Character character = Instantiate(this);

        character.health = copiedHealth;
        character.characteristics = copiedCharacteristics;

        return character;
    }
}
