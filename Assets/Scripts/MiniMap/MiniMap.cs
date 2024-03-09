using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.transform.position.x, 40, target.transform.position.z);
    }
}
