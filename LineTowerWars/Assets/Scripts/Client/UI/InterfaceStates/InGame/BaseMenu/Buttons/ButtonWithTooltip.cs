using UnityEngine.UI;

public abstract class ButtonWithTooltip : HoverTooltip {
    public Button Btn { get; private set; }

    protected override void Awake() {
        Btn = GetComponent<Button>();

        base.Awake();
    }
}