using CxxDemangler.Parsers;
using System.Collections.Generic;

namespace CxxDemangler
{
    public class CxxDemangler
    {
        internal delegate IParsingResult ParsingFunction(ParsingContext context);

        public static string Demangle(string input)
        {
            ParsingContext parsingContext = CreateContext(input);
            IParsingResult result = Parse(parsingContext);

            if (result != null)
            {
                DemanglingContext demanglingContext = DemanglingContext.Create(parsingContext);

                result.Demangle(demanglingContext);
                return demanglingContext.Writer.Text;
            }

            return input;
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
