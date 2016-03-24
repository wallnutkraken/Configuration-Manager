namespace ConfigManager
{
    public interface IConfigLine
    {
        bool HasComment { get; }
        int? CommentPos { get; }
        string OriginalLine { get; }
        string Regenerate();
        string Setting { get; set; }
        string Value { get; set; }

    }
}
