using System.Diagnostics;

namespace CxxDemangler.Parsers
{
    internal class SourceName
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;
            int nameLength;

            if (!context.Parser.ParseNumber(out nameLength))
            {
                return null;
            }

            Debug.Assert(nameLength >= 0);
            if (nameLength == 0)
            {
                context.Rewind(rewind);
                return null;
            }

            if (context.Parser.Position + nameLength <= context.Parser.Input.Length)
            {
                string identifier = context.Parser.Input.Substring(context.Parser.Position, nameLength);

                // Verify that identifier is correct
                bool correct = true;

                foreach (char c in identifier)
                {
                    if (c != '_' && c != '.' && !char.IsLetterOrDigit(c))
                    {
                        correct = false;
                        break;
                    }
                }
                if (!correct)
                {
                    context.Rewind(rewind);
                    return null;
                }

                context.Parser.Position += nameLength;

                return new Identifier(identifier);
            }

            return null;
        }

        public static bool StartsWith(ParsingContext context)
        {
            return char.IsDigit(context.Parser.Peek);
        }

        internal class Identifier : IParsingResult
        {
            public Identifier(string name)
            {
                Name = name;
            }

            public string Name { get; private set; }
        }
    }
}
