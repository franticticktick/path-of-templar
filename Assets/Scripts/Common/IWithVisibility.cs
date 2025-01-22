using UnityEngine;

public class IWithVisibility : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void EnableInvisible()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
    }

    public void DisableWisible()
    {
        gameObject.SetActive(false);
        canvasGroup.alpha = 1;
    }

    public void ToggleEnable()
    {
        if (gameObject.activeSelf)
        {
            Disable();
        }
        else
        {
            Enable();
        }
    }

    public virtual void Enable()
    {
        gameObject.SetActive(true);
        audioSource?.Play();
    }

    public virtual void Disable()
    {
        audioSource?.Play();
        gameObject.SetActive(false);
    }

}
