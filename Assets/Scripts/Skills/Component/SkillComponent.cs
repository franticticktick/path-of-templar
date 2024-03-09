using UnityEngine;

public class SkillComponent : MonoBehaviour
{
    [SerializeField]
    private Skill skill;

    public Skill Skill { get { return skill; } }
}
