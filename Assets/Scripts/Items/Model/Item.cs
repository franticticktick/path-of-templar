using System.Text;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    protected StringBuilder description = new();

    public abstract void Use(Hero hero);

    public abstract string PrepareDescription();
}
