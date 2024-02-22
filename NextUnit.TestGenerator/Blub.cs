namespace NextUnit.TestGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public class OutputGenerator
    {
        public bool EmbedIntoNamespace { get; set; } = true;
        public bool Indentation { get; set; } = true;
        public string IndentationString { get; set; } = "    "; // Default to 4 spaces

        public Dictionary<Type, List<string>> Output { get; set; } = new Dictionary<Type, List<string>>();

        public virtual string Create()
        {
            string result = string.Empty;
            foreach (Type type in Output.Keys)
            {
                List<string> tests = Output[type];
                string testsSourceCode = IndentLines(string.Join(Environment.NewLine, tests), 2); // Indent test methods

                string typeText = EmbedIntoNamespace ? type.Name : type.ToString();
                string classText =
$@"class {typeText}
{{
{testsSourceCode}
}}";
                classText = IndentLines(classText, EmbedIntoNamespace ? 1 : 0); // Indent class definition if inside namespace

                if (EmbedIntoNamespace)
                {
                    string testsNameSpace = type.Namespace ?? "DefaultNamespace";
                    classText = $@"namespace {testsNameSpace}
{{
{classText}
}}";
                }

                result += classText + Environment.NewLine + Environment.NewLine;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="indentLevel"></param>
        /// <returns></returns>
        private string IndentLines(string text, int indentLevel)
        {
            if (!Indentation || indentLevel < 1)
                return text;

            string indent = new String(IndentationString[0], IndentationString.Length * indentLevel);
            string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            string indentedText = string.Join(Environment.NewLine, lines.Select(line => indent + line));
            return indentedText;
        }
    }
}
