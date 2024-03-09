using UnityEngine;

public class IWithVisibility : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
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
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void ToggleVisibility()
    {
        if (IsVisible())
        {
            MakeInvisible();
        }
        else
        {
            MakeVisible();
        }
    }

    protected virtual void MakeVisible()
    {
        canvasGroup.alpha = 1;
    }

    public void MakeInvisible()
    {
        canvasGroup.alpha = 0;
    }

    private bool IsVisible() => canvasGroup.alpha != 0;
}
