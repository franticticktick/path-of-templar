using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    private List<SkillSlot> skillSlots = new();

    public event Action<Skill> SkillInvoked;

    private void Start()
    {
        skillSlots = GetComponentsInChildren<SkillSlot>().ToList();
    }

    private void Update()
    {
        InvokeSkillByKeyCode(KeyCode.F1);
        InvokeSkillByKeyCode(KeyCode.F2);
        InvokeSkillByKeyCode(KeyCode.F3);
        InvokeSkillByKeyCode(KeyCode.F4);
        InvokeSkillByKeyCode(KeyCode.F5);
        InvokeSkillByKeyCode(KeyCode.F6);
        InvokeSkillByKeyCode(KeyCode.F7);
        InvokeSkillByKeyCode(KeyCode.F8);
        InvokeSkillByKeyCode(KeyCode.F9);
    }

    private void InvokeSkillByKeyCode(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            var skillSlot = skillSlots.Find(skillSlot => skillSlot.KeyCode == keyCode);
            SkillInvoked.Invoke(skillSlot.GetSkill());
        }
    }
}
