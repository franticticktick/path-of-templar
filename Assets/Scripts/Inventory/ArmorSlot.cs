using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ArmorSlot : Slot
{
    public UnityEvent<Armor> ArmorWorn;
    public UnityEvent DefaultArmorWorn;

    private bool empty = true;

    protected override void SetParent(PointerEventData eventData)
    {
        var slotItem = GetSlotItem(eventData);
        if (slotItem.ItemType().IsSubclassOf(typeof(Armor)))
        {
            base.SetParent(eventData);
            var itemComponent = slotItem.FindFirstItemComponent();

            ArmorWorn.Invoke((Armor)itemComponent.InventoryItem);
            empty = false;
        }
    }

    void Update()
    {
        UseDefaultArmorIfArmorItemMoved();
    }

    private void UseDefaultArmorIfArmorItemMoved()
    {
        if (transform.childCount == 0 && !empty)
        {
            DefaultArmorWorn.Invoke();
            empty = true;
        }
    }

    protected override void OnDoubleClick(PointerEventData eventData)
    {
    }
}
