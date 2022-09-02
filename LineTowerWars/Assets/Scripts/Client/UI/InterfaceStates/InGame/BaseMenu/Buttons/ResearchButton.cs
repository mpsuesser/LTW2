public class ResearchButton : ButtonWithTooltip
{
    protected override Tooltip GetTooltipContent()
    {
        return new StandardTwoBodiesTooltip(
            "Research Elemental Technology",
            ClientUtil.GetKeyCodeStringRepresentation(
                Settings.ResearchHotkey.Value
            ),
            "View a selection of elemental technologies you can purchase.",
            "By researching new elements, you open different upgrade paths from the base Elemental Core tower."
        );
    }
}