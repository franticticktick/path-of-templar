using UnityEngine;

[CreateAssetMenu(menuName = "DefaultHeroArmor", fileName = "DefaultHeroArmor")]
public class DefaultHeroArmor : Armor
{
    public DefaultHeroArmor(Resistance resistance) : base(resistance)
    {
    }

    public override void Use(Hero hero)
    {
        hero.Armor = this;
    }
}
