using System;
using UnityEngine;

public class ItemComponent : MonoBehaviour, ISelectable
{
    [SerializeField]
    private Texture2D commonCursorTexture;

    [SerializeField]
    private Texture2D takeCursorTexture;

    [SerializeField]
    private string imageSprite;

    [SerializeField]
    private Item inventoryItem;

    [SerializeField]
    private bool single = true;

    void OnMouseOver()
    {
        UnityEngine.Cursor.SetCursor(takeCursorTexture, Vector3.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        SetCursorToDefault();
    }

    private void SetCursorToDefault()
    {
        UnityEngine.Cursor.SetCursor(commonCursorTexture, Vector3.zero, CursorMode.Auto);
    }

    public void Disable()
    {
        SetCursorToDefault();
        gameObject.SetActive(false);
    }

    public void Selected()
    {
    }

    public void Unselected()
    {
    }

    public string ImageSprite
    {
        get { return imageSprite; }
    }

    public Item InventoryItem
    {
        get { return inventoryItem; }
    }

    public Type InventoryItemType() => inventoryItem.GetType();

    public bool IsSingle() => single;

}
