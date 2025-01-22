using System;
using UnityEngine;

[CreateAssetMenu(menuName = "HealthPotion", fileName = "HealthPotion")]
public class HealthPotion : Item
{
    [SerializeField]
    private float value;

    public HealthPotion(float value)
    {
        this.value = value;
    }

    public float Value
    {
        get { return value; }
    }

    public override string PrepareDescription()
    {
        description.Append("�������� ����� \n");
        description.Append("��������: ");
        description.Append(value.ToString());
        description.Append("������ ��������");

        return description.ToString();
    }

    public override void Use(Hero hero)
    {
        hero.UseHealthPotion(this);
    }

    private void OnValidate()
    {
        if (value <= 0)
        {
            throw new ArgumentException("HealthPotion value should be greater 0");
        }
    }
}
