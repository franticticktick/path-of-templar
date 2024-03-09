using UnityEngine;

public class PathPointer : MonoBehaviour
{
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void EnableWithPosition(Vector3 position)
    {
        transform.position = position;
        Enable();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
