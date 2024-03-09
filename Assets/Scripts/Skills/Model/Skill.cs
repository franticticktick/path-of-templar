using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [SerializeField]
    private SkillType type;

    [SerializeField]
    private string skillName;

    [SerializeField]
    private string skillDescription;

    enum SkillType
    {
        DAMAGE,
        BUFF,
        AURA
    }

    public abstract Damage Damage(Character character);

    public bool IsDamageSkill() => type == SkillType.DAMAGE;

    public bool IsBuffSkill() => type == SkillType.BUFF;

    public bool IsAuraSkill() => type == SkillType.AURA;

    public string SkillName { get { return skillName; } }

    public string SkillDescription { get { return skillDescription; } }
}
