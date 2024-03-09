using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HeroCharacteristicsWindow : MonoBehaviour
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

    public void Enable()
    {
        HeroCharacteristicsWindowOpen.Invoke();
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void ToggleEnable()
    {
        if(gameObject.activeSelf == true)
        {
            Disable();
        }
        else
        {
            Enable();
        }
    }
}
