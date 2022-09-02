public class BackButton : HoverTooltip {
    protected override Tooltip GetTooltipContent() {
        return new StandardTwoBodiesTooltip(
            "Back",
            "",
            "Go back to the base menu",
            ""
        );
    }
}