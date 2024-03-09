using UnityEngine;

[CreateAssetMenu(menuName = "LastSplash", fileName = "LastSplash")]
public class LastSplash : Skill
{
    [SerializeField]
    private float damageCoefficient;

    public override Damage Damage(Character character)
    {
        return character.IncreaseDamage(damageCoefficient);
    }
}
