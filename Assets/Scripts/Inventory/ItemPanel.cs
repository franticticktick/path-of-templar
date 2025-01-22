using UnityEngine.Events;

public class ItemPanel : IWithVisibility
{

    public UnityEvent ItemPanelOpen;

    public override void Enable()
    {
        base.Enable();
        ItemPanelOpen?.Invoke();
    }

}
