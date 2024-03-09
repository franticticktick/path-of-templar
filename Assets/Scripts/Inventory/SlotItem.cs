using ModestTree;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image itemImage;

    [HideInInspector]
    public Transform parentAfterDrag;

    [SerializeField]
    private float cellSizeX = 27;

    [SerializeField]
    private float cellSizeY = 27;

    [SerializeField]
    private int paddingLeft = 4;

    [SerializeField]
    private int paddingTop = 4;

    [SerializeField]
    private int fontSize = 25;

    [SerializeField]
    private List<ItemComponent> items = new();

    [SerializeField]
    private TextMeshProUGUI itemsCount;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private const string INITIAL_ITEMS_COUNT = "2";

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        itemImage.raycastTarget = false;
        canvasGroup.blocksRaycasts = false;
    }

    public bool EqualsByInventoryItemType(Type inventoryItemType) =>
        inventoryItemType.Equals(InventoryItemTypeOfFirst());

    private Type InventoryItemTypeOfFirst()
    {
        if (!items.IsEmpty())
        {
            return items.First().InventoryItemType();
        }
        return null;
    }

    public void AddItem(ItemComponent item, string sprite)
    {
        AddImage(sprite);
        AddGridLayoutGroup();
        AddItemsCountFiled();

        items.Add(item);
    }

    public void AddItem(ItemComponent item)
    {
        items.Add(item);
        IncreaseItemsCount();
    }

    private void IncreaseItemsCount()
    {
        if (!itemsCount.text.IsEmpty())
        {
            var count = int.Parse(itemsCount.text) + 1;
            itemsCount.text = count.ToString();
        }
        else
        {
            itemsCount.text = INITIAL_ITEMS_COUNT;
        }
    }

    private void AddImage(string sprite)
    {
        var itemImage = gameObject.AddComponent<Image>();

        itemImage.sprite = Resources.Load<Sprite>(sprite);
        itemImage.transform.localScale = Vector3.one;

        this.itemImage = itemImage;
    }

    private void AddGridLayoutGroup()
    {
        var gridLayoutGroup = gameObject.AddComponent<GridLayoutGroup>();
        gridLayoutGroup.padding.left = paddingLeft;
        gridLayoutGroup.padding.top = paddingTop;
        gridLayoutGroup.cellSize = new Vector2(cellSizeX, cellSizeY);
    }

    private void AddItemsCountFiled()
    {
        var gameItem = new GameObject("ItemsCount");

        var itemsCount = gameItem.AddComponent<TextMeshProUGUI>();
        itemsCount.fontSize = fontSize;
        itemsCount.alignment = TextAlignmentOptions.Center;
        itemsCount.fontStyle = FontStyles.Bold;
        itemsCount.text = string.Empty;

        canvasGroup = itemsCount.gameObject.AddComponent<CanvasGroup>();

        gameItem.transform.SetParent(transform);

        this.itemsCount = itemsCount;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        itemImage.raycastTarget = true;
        canvasGroup.blocksRaycasts = true;
    }

    public ItemComponent FindFirstItemComponent() => items.First();

    public Type ItemType()
    {
        var itemComponent = FindFirstItemComponent();
        return itemComponent.InventoryItemType();
    }

    public void RemoveItemComponentOrDestroy(ItemComponent itemComponent)
    {
        items.Remove(itemComponent);
        Destroy(itemComponent);

        ReduceItemsCount();
        if (items.IsEmpty())
        {
            Destroy(gameObject);
        }
    }

    private void ReduceItemsCount()
    {
        if (!itemsCount.text.IsEmpty())
        {
            var count = int.Parse(itemsCount.text) - 1;
            if (count == 1)
            {
                itemsCount.text = string.Empty;
            }
            else
            {
                itemsCount.text = count.ToString();
            }
        }
    }
}
