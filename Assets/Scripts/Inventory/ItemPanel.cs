using UnityEngine.Events;

public class ItemPanel : IWithVisibility
{
    public UnityEvent ItemPanelOpen;

    protected override void MakeVisible()
    {
        base.MakeVisible();
        ItemPanelOpen?.Invoke();
    }
}
