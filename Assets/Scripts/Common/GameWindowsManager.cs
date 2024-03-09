using UnityEngine;
using Zenject;

public class GameWindowsManager : MonoBehaviour
{
    [Inject]
    private readonly ItemPanel itemPanel;

    [Inject]
    private readonly HeroCharacteristicsWindow characteristicsWindow;

    public void DisableItemPanel()
    {
        itemPanel.MakeInvisible();
    }

    public void DisableHeroCharacteristicsWindow()
    {
        characteristicsWindow.Disable();
    }
}
