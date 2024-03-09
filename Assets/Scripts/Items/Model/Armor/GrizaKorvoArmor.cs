using UnityEngine;

[CreateAssetMenu(menuName = "GrizaKorvoArmor", fileName = "GrizaKorvoArmor")]
public class GrizaKorvoArmor : Armor
{
    public GrizaKorvoArmor(Resistance resistance): base(resistance)
    {
    }

    public override void Use(Hero hero)
    {
        hero.Armor = this;
    }
}
