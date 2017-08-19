namespace CxxDemangler.Parsers
{
    internal class TemplateParam : IParsingResult
    {
        public TemplateParam(int number)
        {
            Number = number;
        }

        public int Number { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (context.Parser.VerifyString("T"))
            {
                int number;

                if (!context.Parser.ParseNumber(out number))
                {
                    number = -1;
                }
                number++;
                if (context.Parser.VerifyString("_"))
                {
                    return new TemplateParam(number);
                }
                context.Rewind(rewind);
            }

            return null;
        }
    }
}
