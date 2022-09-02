public class BuildButton : ButtonWithTooltip
{
    protected override void Awake() {
        base.Awake();
        
        Btn.onClick.AddListener(BuildButtonClicked);
    }

    private static void BuildButtonClicked() {
        EventBus.OpenBuildMenuPressed();
    }
    
    protected override Tooltip GetTooltipContent()
    {
        return new StandardTwoBodiesTooltip(
            "Build Tower",
            ClientUtil.GetKeyCodeStringRepresentation(
                Settings.BuildHotkey.Value
            ),
            "View a selection of towers you can build in exchange for gold.",
            "Building towers will allow you to defend your lane from creeps sent to you by enemy lanes."
        );
    }
}
