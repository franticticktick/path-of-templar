using UnityEngine;

[CreateAssetMenu(menuName = "Hero", fileName = "Hero")]
public class Hero : Character
{
    [SerializeField]
    private Level level;

    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private Armor armor;

    [SerializeField]
    private Armor defaultArmor;

    public Hero(Health health, Characteristics characteristics,
        Weapon weapon, Level level, Armor armor)
        : base(health, characteristics)
    {
        this.level = level;
        this.weapon = weapon;
        this.armor = armor;
    }

    public void ChangeArmorToDefault()
    {
        if (defaultArmor != null)
        {
            armor = defaultArmor;
        }
    }

    public void TakeExpirience(Npc enemy)
    {
        if (enemy.IsDead())
        {
            var levelUpped = level.IncreaseExpirience(enemy.Expirience);

            if (levelUpped)
            {
                characteristics.IncreaseByLevel();
            }
        }
    }

    public void UseHealthPotion(HealthPotion healthPotion)
    {
        if (healthPotion != null)
        {
            health.Replenish(healthPotion);
        }
    }

    protected override Resistance GetResistance()
    {
        return base.GetResistance() + armor.Resistance;
    }

    public override Hit Damage()
    {
        return base.Damage() + weapon.HitDamage;
    }

    public float CurrentExpirience
    {
        get { return level.CurrentExpirience; }
    }

    public float UpExpirience
    {
        get { return level.UpExpirience; }
    }

    public float CurrentLevel
    {
        get { return level.Value; }
    }

    public Level Level
    {
        set { level = value; }
    }

    public Armor Armor
    {
        set { armor = value; }
    }

    public Weapon Weapon
    {
        set { weapon = value; }
    }
}
