using UnityEngine;

public class BasicNpcComponent<T> : CharacterComponent<T>, ISelectable where T : Character
{
    [SerializeField]
    protected bool patroll = true;

    [SerializeField]
    protected TargetPointer targetPointer;

    [SerializeField]
    protected HealthBar healthBar;

    protected override void RunBehavior()
    {
    }

    public void Selected()
    {
        healthBar.OnHealthReducedByDamage(character.CurrentHealth);
        healthBar.Enable();

        targetPointer.Enable();
    }

    public void Unselected()
    {
        healthBar.Disable();
        targetPointer.Disable();
    }
}
