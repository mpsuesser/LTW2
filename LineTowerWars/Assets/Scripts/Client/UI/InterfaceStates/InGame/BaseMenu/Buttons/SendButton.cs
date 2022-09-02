public class SendButton : ButtonWithTooltip
{
    protected override Tooltip GetTooltipContent()
    {
        return new StandardTwoBodiesTooltip(
            "Send Creeps",
            "S", // TODO: Remove hardcode
            "View a selection of creeps you can send to the lane in front of you.",
            "Sending creeps will affect your income for all future income rounds. Compound interest is a powerful thing!"
        );
    }
}