using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NpcComponent : BasicNpcComponent<Npc>
{
    private float timer;

    [SerializeField]
    private List<GameObject> wayPoints = new();

    [SerializeField]
    private List<DropItem> dropItems;

    protected override void Init()
    {
        character = (Npc)character.Copy();

        character.OnHealthReducedByDamage += healthBar.OnHealthReducedByDamage;
        healthBar.OnHealthReducedByDamage(character.CurrentHealth);

        base.Init();
    }

    private void TakeBattlePose()
    {
        if (character.IsAattacked())
        {
            characterNavigator.TurnTowardsTheTarget(transform, interactionTarget.position);
            characterAnimator.StartAnimationIfCharacterAttacked();
        }
        else
        {
            characterAnimator.StartAnimationIfCharacterStopAttacked();
        }
    }

    private float GeneratePatrollTimer() => Random.Range(25, 36);

    private void Drop()
    {
        var items = new HashSet<DropItem>();

        for (int i = 0; i < dropItems.Count; i++)
        {
            var dropItem = ChanceUtil.TakeRandomByChance(dropItems, 0);
            items.Add(dropItem);
        }

        foreach (var item in items)
        {
            item.Drop(transform.position);
        }
    }

    public override bool ReceiveDamage(Hit hit, Transform target)
    {
        AttackTarget(target);
        return base.ReceiveDamage(hit, target);
    }

    private void Patroll()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && !character.IsAattacked())
        {
            var wayPointIndex = Random.Range(0, wayPoints.Count);
            MoveToTarget(wayPoints[wayPointIndex].transform.position);

            timer = GeneratePatrollTimer();
        }
    }

    private void SetAttackedToFalseIfEnemyDead()
    {
        if (interactionTarget != null)
        {
            var enemy = interactionTarget.GetComponent<ICharacterComponent>();
            character.SetAttackedToFalseIfEnemyDead(enemy.Character());
        }
    }

    protected override void RunBehavior()
    {
        if (!character.IsDead())
        {
            if (patroll) Patroll();
            SetAttackedToFalseIfEnemyDead();
            TakeBattlePose();
        }
    }

    protected override void OnDeath(bool dead)
    {
        if (dead)
        {
            targetPointer.Disable();

            base.OnDeath(dead);
            Destroy(gameObject, 5f);

            Drop();
        }
    }

    public class Factory : PlaceholderFactory<NpcComponent> { }
}
