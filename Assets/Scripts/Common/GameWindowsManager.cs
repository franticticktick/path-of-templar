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
        itemPanel.Disable();
    }

    public void DisableHeroCharacteristicsWindow()
    {
        characteristicsWindow.Disable();
    }
}
