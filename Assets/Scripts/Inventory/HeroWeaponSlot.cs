using UnityEngine.EventSystems;

public class HeroWeaponSlot : Slot
{
    protected override void SetParent(PointerEventData eventData)
    {
        var slotItem = GetSlotItem(eventData);
        if (slotItem.ItemType().IsSubclassOf(typeof(Weapon)))
        {
            base.SetParent(eventData);
        }
    }

    protected override void OnDoubleClick(PointerEventData eventData)
    {
    }
}
