using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Slot> inventorySlots;

    public void AddItem(ItemComponent item)
    {
        var freeSlot = FindFreeSlot(item);

        if (freeSlot.HasItem())
        {
            freeSlot.AddItem(item);
        }
        else
        {
            var gameItem = new GameObject("Item");
            var slotItem = gameItem.AddComponent<SlotItem>();
            gameItem.transform.parent = freeSlot.transform;

            slotItem.AddItem(item, item.ImageSprite);
        }

        item.Disable();

        Destroy(item);
    }


    private Slot FindFreeSlot(ItemComponent item)
    {
        if (!item.IsSingle())
        {
            var slot = inventorySlots.Find(slot => slot.EqualsByInventoryItemType(item.InventoryItemType()));
            if(slot == null)
            {
                return inventorySlots.Find(slot => !slot.HasItem());
            } else
            {
                return slot;
            }
        }
        return inventorySlots.Find(slot => !slot.HasItem());
    }

    void Start()
    {
        inventorySlots = GetComponentsInChildren<Slot>().ToList();
    }
}
