namespace CxxDemangler.Parsers
{
    // <unnamed-type-name> ::= Ut [ <nonnegative number> ] _
    internal class UnnamedTypeName : IParsingResult
    {
        public UnnamedTypeName(int? number)
        {
            Number = number;
        }

        public int? Number { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (context.Parser.VerifyString("Ut"))
            {
                int? number = context.Parser.ParseNumber();

                if (context.Parser.VerifyString("_"))
                {
                    return new UnnamedTypeName(number);
                }
                context.Rewind(rewind);
            }

            return ClosureTypeName.Parse(context);
        }

        public static bool StartsWith(ParsingContext context)
        {
            return context.Parser.Peek == 'U';
        }
    }
}
