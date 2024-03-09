using UnityEngine;
using Zenject;

public class PersonComponent : BasicNpcComponent<Person>, INteractable
{
    [Inject]
    private readonly DialogWindow dialogWindow;

    [Inject]
    private readonly HeroComponent heroComponent;

    protected override void RunBehavior()
    {
    }

    protected override void Init()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        characterAnimator = new CharacterAnimator(GetComponent<Animator>());
    }

    public void Interact()
    {
        var dialog = character.Dialog;
        dialog.BackToDefaultVariant();

        dialogWindow.EnableWithDialog(dialog);
    }

    public void EndInteract()
    {
        targetPointer.Disable();
    }

    public void TakeAnswer(int answerId)
    {
        var hero = heroComponent.Character();
        character.TakeAnswer(answerId, hero.Intelligence);

        dialogWindow.ClearAnswersWindowAndSetDialog(character.Dialog);
    }
}
