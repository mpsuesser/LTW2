public class StandardTooltip : Tooltip {
    public string Title { get; private set; }
    public string Subtitle { get; private set; }
    public string CornerNote { get; private set; }
    public string Body { get; private set; }
    public string Footer { get; private set; }

    public StandardTooltip(string title, string subtitle, string cornerNote, string body, string footer) : base() {
        Title = title;
        Subtitle = subtitle;
        CornerNote = cornerNote;
        Body = body;
        Footer = footer;
    }

    public StandardTooltip() {
        Title = "";
        Subtitle = "";
        CornerNote = "";
        Body = "";
        Footer = "";
    }
}
