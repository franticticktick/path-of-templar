using UnityEngine;
using Zenject;

public class HeroInstaller : MonoInstaller<HeroInstaller>
{
    [SerializeField]
    private HeroComponent heroComponent;

    [SerializeField]
    private DialogWindow dialogWindow;

    [SerializeField]
    private ItemPanel itemPanel;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private HealthBar healthBar;

    [SerializeField]
    private PathPointer pathPointer;

    [SerializeField]
    private PersonComponent personComponent;

    [SerializeField]
    private MiniMapWindow miniMapWindow;

    [SerializeField]
    private CloseButton closeButton;

    [SerializeField]
    private HeroCharacteristicsWindow heroCharacteristicsWindow;

    [SerializeField]
    private GameWindowsManager gameWindowsManager;

    [SerializeField]
    private SkillPanel skillPanel;

    public override void InstallBindings()
    {
        Container.Bind<HeroComponent>()
            .FromInstance(heroComponent)
            .AsSingle()
            .NonLazy();
        Container.Bind<DialogWindow>()
            .FromInstance(dialogWindow)
            .AsSingle()
            .NonLazy();
        Container.Bind<ItemPanel>()
            .FromInstance(itemPanel)
            .AsSingle()
            .NonLazy();
        Container.Bind<Inventory>()
            .FromInstance(inventory)
            .AsSingle()
            .NonLazy();
        Container.Bind<HealthBar>()
            .FromInstance(healthBar)
            .AsSingle()
            .NonLazy();
        Container.Bind<PathPointer>()
          .FromInstance(pathPointer)
          .AsSingle()
          .NonLazy();
        Container.Bind<PersonComponent>()
          .FromInstance(personComponent)
          .AsSingle()
          .NonLazy();
        Container.Bind<MiniMapWindow>()
          .FromInstance(miniMapWindow)
          .AsSingle()
          .NonLazy();
        Container.Bind<CloseButton>()
          .FromInstance(closeButton)
          .AsSingle()
          .NonLazy();
        Container.Bind<HeroCharacteristicsWindow>()
         .FromInstance(heroCharacteristicsWindow)
         .AsSingle()
         .NonLazy();
        Container.Bind<GameWindowsManager>()
         .FromInstance(gameWindowsManager)
         .AsSingle()
         .NonLazy();
        Container.Bind<SkillPanel>()
        .FromInstance(skillPanel)
        .AsSingle()
        .NonLazy();
    }
}