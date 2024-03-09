using UnityEngine;

public interface ICharacterComponent
{

    public abstract void Damage();

    public abstract void Move();

    public abstract void Attack();

    public abstract bool ReceiveDamage(Hit hit, Transform target);

    public abstract Character Character();
}
