namespace CxxDemangler.Parsers
{
    // <closure-type-name> ::= Ul <lambda-sig> E [ <nonnegative number> ] _
    internal class ClosureTypeName : IParsingResult
    {
        public ClosureTypeName(IParsingResult signature, int? number)
        {
            Signature = signature;
            Number = number;
        }

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

            return new ClosureTypeName(signature, number);
        }
    }
}
