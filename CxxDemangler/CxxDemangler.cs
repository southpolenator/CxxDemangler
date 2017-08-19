using CxxDemangler.Parsers;
using System.Collections.Generic;

namespace CxxDemangler
{
    public class CxxDemangler
    {
        internal delegate IParsingResult ParsingFunction(ParsingContext context);

        public static string Demangle(string input)
        {
            IParsingResult result = Parse(input);

            if (result != null)
            {
                // TODO:
                return result.ToString();
            }

            return input;
        }

        internal static IParsingResult Parse(string input)
        {
            ParsingContext context = CreateContext(input);

            return Parse(context);
        }

        internal static IParsingResult Parse(ParsingContext context)
        {
            return MangledName.Parse(context);
        }

        internal static List<IParsingResult> ParseList(ParsingFunction parse, ParsingContext context)
        {
            List<IParsingResult> results = new List<IParsingResult>();

            while (true)
            {
                IParsingResult result = parse(context);

                if (result == null)
                {
                    break;
                }

                results.Add(result);
            }

            return results;
        }

        internal static ParsingContext CreateContext(string input)
        {
            SubstitutionTable table = new SubstitutionTable();
            SimpleStringParser parser = new SimpleStringParser(input);

            return new ParsingContext()
            {
                Parser = parser,
                SubstitutionTable = table,
            };
        }
    }
}
