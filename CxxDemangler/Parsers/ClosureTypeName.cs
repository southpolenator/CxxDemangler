namespace CxxDemangler.Parsers
{
    internal class ClosureTypeName : IParsingResult
    {
        public IParsingResult Signature { get; private set; }

        public int? Number { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (!context.Parser.VerifyString("Ul"))
            {
                return null;
            }

            IParsingResult signature = LambdaSig.Parse(context);

            if (signature == null || !context.Parser.VerifyString("E"))
            {
                context.Rewind(rewind);
                return null;
            }

            int? number = context.Parser.ParseNumber();

            if (!context.Parser.VerifyString("_"))
            {
                context.Rewind(rewind);
                return null;
            }

            return new ClosureTypeName()
            {
                Signature = signature,
                Number = number,
            };
        }
    }
}
