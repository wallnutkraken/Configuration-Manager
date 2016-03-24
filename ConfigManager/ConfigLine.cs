namespace ConfigManager
{
    public class ConfigLine : IConfigLine
    {
        private char[] _lineChars;
        private readonly char[] _keyValueSplitterChar = { '=' };
        private string Comment { get; }
        public ConfigLine(string line)
        {
            if (line.Trim() == string.Empty) /* Empty line */
            {
                HasComment = true;
                CommentPos = 0;
                return;
            }
            _lineChars = line.ToCharArray();
            OriginalLine = line;
            bool lineIsOnlyComment = false;
            if (_lineChars[0] == '#')
            {
                lineIsOnlyComment = true;
                HasComment = true;
                CommentPos = 0;
                Setting = null;
                Value = null;
                Comment = line;
                return;
            }
            bool continueLoop = true;
            for (int i = 0; i < _lineChars.Length && lineIsOnlyComment == false && continueLoop; i++)
            {
                if (i > 0 && (_lineChars[i] == '#' && _lineChars[i - 1] != '\\'))
                {
                    HasComment = true;
                    CommentPos = i;
                    continueLoop = false;
                    Comment = line.Remove(0, i);
                }
            }
            string lineWithoutComment;
            if (HasComment && CommentPos != 0)
            {
                lineWithoutComment = line.Remove(CommentPos.Value);
            }
            else
            {
                lineWithoutComment = line;
            }
            string[] parts = lineWithoutComment.Split(_keyValueSplitterChar, 2);
            Setting = parts[0].Trim();
            Value = parts[1].Trim();

        }
        public bool HasComment { get; }
        public int? CommentPos { get; }
        public string OriginalLine { get; }
        public string Regenerate()
        {
            string generatedLine;

            if (HasComment == false)
            {
                generatedLine = $"{Setting}={Value}";
            }
            else if (HasComment && CommentPos == 0)
            {
                generatedLine = Comment;
            }
            else
            {
                generatedLine = $"{Setting}={Value} {Comment}";
            }

            return generatedLine;
        }

        public string Setting { get; set; }
        public string Value { get; set; }
    }
}
