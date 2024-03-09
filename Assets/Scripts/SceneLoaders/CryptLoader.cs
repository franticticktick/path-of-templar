using UnityEngine;
using UnityEngine.SceneManagement;

public class CryptLoader : MonoBehaviour
{
    [SerializeField]
    private int sceneNumber;

    [SerializeField]
    private Canvas canvas;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Hero"))
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
