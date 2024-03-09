using UnityEngine;

public class CharacterAnimator
{
    private readonly Animator animator;

    private const string WALK = "Walk";
    private const string ATTACK = "Attack";
    private const string ATTACKED = "Attacked";

    public CharacterAnimator(Animator animator)
    {
        this.animator = animator;
    }

    public void DisableAnimations()
    {
        animator.SetBool(WALK, false);
        animator.SetBool(ATTACK, false);
    }

    public void DisableAnimationsWithAnimator()
    {
        DisableAnimations();
        animator.enabled = false;
    }

    public void StartWalkAnimation()
    {
        animator.SetBool(WALK, true);
        animator.SetBool(ATTACK, false);
    }

    public void StartAttackAnimation()
    {
        animator.SetBool(WALK, false);
        animator.SetBool(ATTACK, true);
    }

    public void StartAnimationIfCharacterAttacked()
    {
        animator.SetBool(ATTACKED, true);
    }

    public void StartAnimationIfCharacterStopAttacked()
    {
        animator.SetBool(ATTACKED, false);
    }

    public void StartSkillDamageAnimation(string skillName)
    {
        animator.SetTrigger(skillName);
    }
}
