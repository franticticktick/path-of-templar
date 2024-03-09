using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler, IPointerClickHandler
{

    public UnityEvent<ItemComponent> ItemUsed;

    public void OnDrop(PointerEventData eventData)
    {
        SetParent(eventData);
    }

    protected virtual void SetParent(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            SlotItem item = GetSlotItem(eventData);
            item.parentAfterDrag = transform;
        }
    }

    protected SlotItem GetSlotItem(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        return dropped.GetComponent<SlotItem>();
    }

    public void AddItem(ItemComponent item)
    {
        var slotItem = GetComponentInChildren<SlotItem>();
        slotItem.AddItem(item);
    }

    public bool HasItem() => GetComponentInChildren<SlotItem>() != null;

    public bool EqualsByInventoryItemType(Type inventoryItemType)
    {
        var slotItem = GetComponentInChildren<SlotItem>();
        if (slotItem != null)
        {
            return slotItem.EqualsByInventoryItemType(inventoryItemType);
        }
        return false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnDoubleClick(eventData);
    }

    protected virtual void OnDoubleClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            var slotItem = GetComponentInChildren<SlotItem>();

            if (slotItem != null)
            {
                var itemComponent = slotItem.FindFirstItemComponent();

                ItemUsed.Invoke(itemComponent);

                slotItem.RemoveItemComponentOrDestroy(itemComponent);
            }
        }
    }
}
