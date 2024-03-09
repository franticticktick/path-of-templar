using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private KeyCode keyCode;

    public UnityEvent<Skill> SkillExecuted;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            var skillItem = dropped.GetComponent<SkillSlotItem>();

            skillItem.parentAfterDrag = transform;
        }
    }

    public Skill GetSkill()
    {
        var skillSlotItem = GetComponentInChildren<SkillSlotItem>();
        if (skillSlotItem != null)
        {
            return skillSlotItem.Skill;
        }
        return null;
    }

    public KeyCode KeyCode { get { return keyCode; } }
}
