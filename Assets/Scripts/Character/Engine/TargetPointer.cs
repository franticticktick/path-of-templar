using UnityEngine;

public class TargetPointer : MonoBehaviour
{
    public void Disable()
    { 
        gameObject.SetActive(false);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
}
