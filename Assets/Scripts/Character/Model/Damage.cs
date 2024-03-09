using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Damage", fileName = "Damage")]
public class Damage : ScriptableObject
{
    [SerializeField]
    private float basicPhysicalDamage;

    [SerializeField]
    private float basicFireDamage;

    [SerializeField]
    private float basicIceDamage;

    [SerializeField]
    private float basicWindDamage;

    public Damage(float basicPhysicalDamage, float basicFireDamage, float basicIceDamage, float basicWindDamage)
    {
        this.basicPhysicalDamage = basicPhysicalDamage;
        this.basicFireDamage = basicFireDamage;
        this.basicIceDamage = basicIceDamage;
        this.basicWindDamage = basicWindDamage;
    }

    public static Damage operator *(Damage basic, float damageCoefficient) =>
       new(
           basic.basicPhysicalDamage * damageCoefficient + basic.basicPhysicalDamage,
           basic.basicFireDamage * damageCoefficient + basic.basicFireDamage,
           basic.basicWindDamage * damageCoefficient + basic.basicWindDamage,
           basic.basicIceDamage * damageCoefficient + basic.basicIceDamage
           );

    public Hit Hit()
    {
        return new Hit(basicPhysicalDamage, basicFireDamage,
             basicIceDamage, basicWindDamage);
    }

    public Hit Hit(float damageCoefficient)
    {
        var increasedDamage = this * damageCoefficient;
        return new Hit(
                increasedDamage.BasicPhysicalDamage, increasedDamage.BasicFireDamage,
                increasedDamage.BasicIceDamage, increasedDamage.BasicWindDamage
            );
    }

    public float BasicPhysicalDamage
    {
        get { return basicPhysicalDamage; }

        set { basicPhysicalDamage = value; }
    }

    public float BasicFireDamage
    {
        get { return basicFireDamage; }
    }

    public float BasicIceDamage
    {
        get { return basicIceDamage; }
    }

    public float BasicWindDamage
    {
        get { return basicWindDamage; }
    }

    private void OnValidate()
    {
        if (basicPhysicalDamage <= 0)
        {
            throw new ArgumentException("basicPhysicalDamage should be greater 0");
        }
    }
}
