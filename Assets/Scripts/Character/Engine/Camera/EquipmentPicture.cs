using UnityEngine;

public class EquipmentPicture : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //   transform.SetPositionAndRotation(new Vector3(target.transform.position.x, target.transform.position.y + 2, target.transform.position.z + 3.5f), Quaternion.Euler(0, 180, 0));
        transform.SetPositionAndRotation(new Vector3(target.transform.position.x, target.transform.position.y + 2, target.transform.position.z + 3.5f),
            Quaternion.Euler(target.rotation.x, target.rotation.y - 180, target.rotation.z));
    }
}
