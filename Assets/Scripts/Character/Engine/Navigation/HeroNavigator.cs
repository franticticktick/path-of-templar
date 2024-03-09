using UnityEngine;
using UnityEngine.AI;

public class HeroNavigator : CharacterNavigator
{
    private readonly PathPointer pathPointer;
    private bool attack = false;

    public HeroNavigator(NavMeshAgent agent, PathPointer pathPointer)
        : base(agent)
    {
        this.pathPointer = pathPointer;
    }

    protected override void StartMove(Vector3 targetPosition)
    {
        if (!attack)
        {
            PutPathPointer(targetPosition);
        }
        base.StartMove(targetPosition);
    }

    private void PutPathPointer(Vector3 targetPosition)
    {
        Vector3 position = new(targetPosition.x, targetPosition.y + 0.1f, targetPosition.z);

        pathPointer.EnableWithPosition(position);
    }


    public override void Move(Vector3 characterPosition, Vector3 targetPosition)
    {
        attack = false;

        base.Move(characterPosition, targetPosition);
        if (distance <= 1.1f)
        {
            pathPointer.Disable();
        }
    }

    public override void Attack(Transform characherTransform, Vector3 enemyPosition)
    {
        attack = true;
        pathPointer.Disable();

        base.Attack(characherTransform, enemyPosition);
    }

    public override void ComeUp(Transform characherTransform, Vector3 targetInteractPosition)
    {
        pathPointer.Disable();
        base.ComeUp(characherTransform, targetInteractPosition);
    }
}
