using UnityEngine;

public abstract class Armor: Item
{
    [SerializeField]
    private Resistance resistance;

    protected Armor(Resistance resistance)
    {
        this.resistance = resistance;
    }

    public Resistance Resistance
    {
        get { return resistance; }
    }

    public override string PrepareDescription()
    {
        throw new System.NotImplementedException();
    }
}
