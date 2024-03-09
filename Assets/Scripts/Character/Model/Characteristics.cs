using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Characteristics", fileName = "Characteristics")]
public class Characteristics : ScriptableObject
{
    [SerializeField]
    private float strength;

    [SerializeField]
    private float dexterity;

    [SerializeField]
    private float intelligence;

    [SerializeField]
    private Damage damage;

    [SerializeField]
    private Resistance resistance;

    [SerializeField]
    private float incrementStep = 2;
    [SerializeField]
    private float damageCoefficient = 0.08f;

    public Characteristics(float strength, float dexterity,
        float intelligence, Damage damage, Resistance resistance)
    {
        this.strength = strength;
        this.dexterity = dexterity;
        this.intelligence = intelligence;
        this.damage = damage;
        this.resistance = resistance;
    }

    public Hit HitDamage()
    {
        return damage.Hit();
    }

    public void IncreaseByLevel()
    {
        strength *= incrementStep;
        dexterity *= incrementStep;
        intelligence *= incrementStep;

        damage.BasicPhysicalDamage = CalculateBasicPhysicalDamage();
    }

    private float CalculateBasicPhysicalDamage()
    {
        return dexterity * strength * damageCoefficient;
    }

    public Resistance Resistance
    {
        get { return resistance; }
    }

    public float Intelligence { get { return intelligence; } }

    public float Strength
    {
        get { return strength; }
    }

    public float Dexterity { get { return dexterity; } }

    public Damage Damage { get { return damage; } }

    private void OnValidate()
    {
        if (strength < 0 || dexterity < 0
            || intelligence < 0
            || incrementStep <= 0 || damageCoefficient <= 0)
        {
            throw new ArgumentException("Wrong characteristics values");
        }
    }
}
