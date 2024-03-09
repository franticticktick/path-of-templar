using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;

public class CloseButton : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent<bool> DialogEnded;

    [Inject]
    private readonly MiniMapWindow miniMapWindow;

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponentInParent<DialogWindow>().gameObject.SetActive(false);
        miniMapWindow.Enable();
        DialogEnded.Invoke(true);
    }
}
