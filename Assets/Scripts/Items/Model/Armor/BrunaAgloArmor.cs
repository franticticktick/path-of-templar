using UnityEngine;

[CreateAssetMenu(menuName = "BrunaAgloArmor", fileName = "BrunaAgloArmor")]
public class BrunaAgloArmor : Armor
{
    public BrunaAgloArmor(Resistance resistance) : base(resistance)
    {
    }

    public override void Use(Hero hero)
    {
        hero.Armor = this;
    }
}
