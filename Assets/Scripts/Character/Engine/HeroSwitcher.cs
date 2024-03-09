using UnityEngine;

public class HeroSwitcher : MonoBehaviour
{
    [SerializeField]
    private HeroComponent grizaKorvo;

    [SerializeField]
    private HeroComponent brunaAglo;

    [SerializeField]
    private HeroComponent defaultHero;

    private Vector3 currentPosition;

    public void OnArmorWorn(Armor armor)
    {
        switch (armor)
        {
            case GrizaKorvoArmor:
                brunaAglo.Disable();
                defaultHero.Disable();
                currentPosition = defaultHero.transform.position;
                grizaKorvo.Enable(currentPosition);
                break;
            case BrunaAgloArmor:
                grizaKorvo.Disable();
                defaultHero.Disable();
                currentPosition = defaultHero.transform.position;
                brunaAglo.Enable(currentPosition);
                break;
        }
    }

    public void OnDefaultArmorWorn()
    {
        grizaKorvo.Disable();
        brunaAglo.Disable();
        defaultHero.Enable(currentPosition);
    }
}
