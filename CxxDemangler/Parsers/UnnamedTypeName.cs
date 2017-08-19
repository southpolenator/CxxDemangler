namespace CxxDemangler.Parsers
{
    internal class UnnamedTypeName : IParsingResult
    {
        public int? Number { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (context.Parser.VerifyString("Ut"))
            {
                int? number = context.Parser.ParseNumber();

                if (context.Parser.VerifyString("_"))
                {
                    return new UnnamedTypeName()
                    {
                        Number = number,
                    };
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
