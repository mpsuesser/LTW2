public class StandardTwoBodiesTooltip : Tooltip {
    public string Title { get; private set; }
    public string CornerNote { get; private set; }
    public string Body { get; private set; }
    public string SecondBody { get; private set; }

    public StandardTwoBodiesTooltip(string title, string cornerNote, string body, string secondBody) : base() {
        Title = title;
        CornerNote = cornerNote;
        Body = body;
        SecondBody = secondBody;
    }

    public StandardTwoBodiesTooltip() {
        Title = "";
        CornerNote = "";
        Body = "";
        SecondBody = "";
    }
}