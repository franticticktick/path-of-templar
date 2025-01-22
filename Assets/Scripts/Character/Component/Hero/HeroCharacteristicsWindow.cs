using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HeroCharacteristicsWindow : IWithVisibility
{
    [SerializeField]
    private Hero hero;

    [SerializeField]
    private TextMeshProUGUI strengthField;

    [SerializeField]
    private TextMeshProUGUI dexterityField;

    [SerializeField]
    private TextMeshProUGUI intelligenceField;

    public UnityEvent HeroCharacteristicsWindowOpen;

    void Start()
    {
        strengthField.text = hero.Strength.ToString();
        dexterityField.text = hero.Dexterity.ToString();
        intelligenceField.text = hero.Intelligence.ToString();
    }

    public override void Enable()
    {
        base.Enable();
        HeroCharacteristicsWindowOpen.Invoke();
    }
}
