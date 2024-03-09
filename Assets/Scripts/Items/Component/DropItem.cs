using UnityEngine;

public class DropItem : MonoBehaviour, IWithProbability
{
    private const string GROUND = "Ground";

    [SerializeField]
    private int count;

    [SerializeField]
    private float probability;

    [SerializeField]
    private ItemComponent item;

    public void Drop(Vector3 position)
    {
        for (int i = 0; i < count; i++)
        {
            var dropPosition = DefineDropPosition(position);
            if (!dropPosition.Equals(Vector3.zero))
            {
                Instantiate(item, dropPosition, Quaternion.identity);
            }
        }
    }

    private Vector3 DefineDropPosition(Vector3 initialPosition)
    {
        var dropPosition = new Vector3(
                initialPosition.x + Random.Range(0.5f, 8),
                initialPosition.y,
                initialPosition.z + Random.Range(0.5f, 8));

        if (IsGround(dropPosition))
        {
            return dropPosition;

        }
        return Vector3.zero;
    }

    private bool IsGround(Vector3 dropPosition)
    {
        var ray = Camera.main.ScreenPointToRay(dropPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform.CompareTag(GROUND);
        }
        return false;
    }

    public float Probability()
    {
        return probability;
    }

    public ItemComponent Item
    {
        get { return item; }
    }

    public int Count
    {
        get { return count; }
    }
}
