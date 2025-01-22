using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class HeroComponent : CharacterComponent<Hero>
{
    //Tags
    protected const string GROUND = "Ground";
    protected const string ENEMY = "Enemy";
    protected const string HERO = "Hero";
    protected const string PERSON = "Person";
    protected const string ITEM = "Item";

    [Inject]
    private readonly PathPointer pathPointer;

    [Inject]
    private readonly ItemPanel itemPanel;

    [Inject]
    private readonly HealthBar healthBar;

    [Inject]
    private readonly Inventory inventory;

    [Inject]
    private readonly HeroCharacteristicsWindow characteristicsWindow;

    [Inject]
    private readonly SkillPanel skillPanel;

    protected override void Init()
    {
        character.OnHealthReducedByDamage += healthBar.OnHealthReducedByDamage;

        DontDestroyOnLoad(gameObject);

        base.Init();
        characterNavigator.OnStopMove += OnStopComingUp;
        skillPanel.SkillInvoked += OnSkillInvoked;
    }

    protected override void RunBehavior()
    {
        if (!character.IsDead())
        {
            healthBar.OnHealthReducedByDamage(character.CurrentHealth);
            UpdateAction();
        }
    }

    protected override CharacterNavigator CharacterNavigator(NavMeshAgent agent)
    {
        return new HeroNavigator(agent, pathPointer);
    }

    private void UpdateAction()
    {
        OnRightButtonClick();
        OnIButtonClick();
    }

    private void OnRightButtonClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                switch (hit.transform.tag)
                {
                    case GROUND: MoveToTarget(hit.point); break;
                    case ENEMY: AttackTarget(hit.transform); break;
                    case HERO: AttackTarget(hit.transform); break;
                    case PERSON: InteractWithTarget(hit.transform); break;
                    case ITEM: InteractWithTarget(hit.transform); break;
                }
            }
        }
    }

    private void OnIButtonClick()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            itemPanel.ToggleEnable();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            characteristicsWindow.ToggleEnable();
        }
    }

    protected override void AttackTarget(Transform interactionTarget)
    {
        this.interactionTarget?.GetComponent<ISelectable>().Unselected();
        interactionTarget.GetComponent<ISelectable>().Selected();

        base.AttackTarget(interactionTarget);
    }

    protected override void MoveToTarget(Vector3 movingTarget)
    {

        if (interactionTarget != null)
        {
            interactionTarget.transform.GetComponent<ISelectable>().Unselected();

        }
        base.MoveToTarget(movingTarget);
    }

    //Начинаем взаимодействовать с целью, например npc.
    private void InteractWithTarget(Transform interactionTarget)
    {
        this.interactionTarget?.GetComponent<ISelectable>().Unselected();
        interactionTarget.GetComponent<ISelectable>().Selected();

        this.interactionTarget = interactionTarget;
        action = ComeUpToTarget;
    }

    //Походим к цели, чтобы провзаимодействовать с ней.
    private void ComeUpToTarget()
    {
        characterNavigator.ComeUp(transform, interactionTarget.position);
    }

    //Нанести урон по цели от боевого умения и получить опыт от убитого проитвника.
    public void SkillDamage()
    {
        MakeDamage(character.SkillDamage());
        TakeEnemyExpirience();
    }

    //Нанести урон по цели и получить опыт от убитого проитвника.
    public override void Damage()
    {
        base.Damage();
        TakeEnemyExpirience();
    }

    private void TakeEnemyExpirience()
    {
        if (interactionTarget != null)
        {
            NpcComponent npcComponent = interactionTarget.GetComponent<NpcComponent>();
            Npc enemy = (Npc)npcComponent.Character();

            character.TakeExpirience(enemy);
        }
    }

    //Реакция на событие, когда предмет, находящийся в инвентаре, будет использован.
    public void OnItemUsed(ItemComponent itemComponent)
    {
        itemComponent.InventoryItem.Use(character);
    }

    //Реакция на событие, когда в инвентаре будет надета броня в слот для брони.
    public void OnArmorWorn(Armor armor)
    {
        character.Armor = armor;
    }

    private void OnSkillInvoked(Skill skill)
    {
        character.AssignCurrentSkill(skill);
        characterAnimator.StartSkillDamageAnimation(skill.SkillName);
    }

    //Реакция на момент, когда герой подходит к цели, например, к npc или выпавшему предмету.
    private void OnStopComingUp(bool stop)
    {
        if (stop)
        {
            DisableAction();

            if (interactionTarget != null)
            {
                if (interactionTarget.TryGetComponent<INteractable>(out var target))
                {
                    target.Interact();
                }
                if (interactionTarget.TryGetComponent<ItemComponent>(out var item))
                {
                    itemPanel.EnableInvisible();
                    inventory.AddItem(item);
                    itemPanel.DisableWisible();
                }

                interactionTarget = null;
            }
        }
    }

    public void Enable(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
