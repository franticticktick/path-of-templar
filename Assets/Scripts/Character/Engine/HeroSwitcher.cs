using UnityEngine;

public class HeroSwitcher : MonoBehaviour
{
    [SerializeField]
    private HeroComponent grizaKorvo;

    [SerializeField]
    private HeroComponent brunaAglo;

    [SerializeField]
    private HeroComponent defaultHero;

    [SerializeField]
    private HeroComponent activeHero;

    public void OnArmorWorn(Armor armor)
    {
        switch (armor)
        {
            case GrizaKorvoArmor:
                brunaAglo.Disable();
                grizaKorvo.Enable(activeHero.transform.position);
                defaultHero.Disable();
                activeHero = grizaKorvo;
                break;
            case BrunaAgloArmor:
                grizaKorvo.Disable();
                defaultHero.Disable();
                brunaAglo.Enable(activeHero.transform.position);
                activeHero = brunaAglo;
                break;
        }
    }

    public void OnDefaultArmorWorn()
    {
        grizaKorvo.Disable();
        brunaAglo.Disable();
        defaultHero.Enable(activeHero.transform.position);
        activeHero = defaultHero;
    }
}
