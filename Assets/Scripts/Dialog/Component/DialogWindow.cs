using UnityEngine;
using Zenject;

public class DialogWindow : MonoBehaviour
{
    private AnswersWindow answersWindow;
    private CurrentDialogVariantWindow variantWindow;

    [Inject]
    private MiniMapWindow miniMapWindow;

    void Start()
    {
        answersWindow = GetComponentInChildren<AnswersWindow>();
        variantWindow = GetComponentInChildren<CurrentDialogVariantWindow>();
    }

    private void Awake()
    {
        answersWindow = GetComponentInChildren<AnswersWindow>();
        variantWindow = GetComponentInChildren<CurrentDialogVariantWindow>();
    }

    public void SetDialog(Dialog dialog)
    {
        answersWindow.InitAnswers(dialog.ShowCurrentAnswers());
        variantWindow.SetTextValue(dialog.CurrentVariantText);
    }

    public void ClearAnswersWindowAndSetDialog(Dialog dialog)
    {
        answersWindow.ClearAnswers();
        SetDialog(dialog);
    }

    public void EnableWithDialog(Dialog dialog)
    {
        gameObject.SetActive(true);
        SetDialog(dialog);
        miniMapWindow.Disable();
    }
}
