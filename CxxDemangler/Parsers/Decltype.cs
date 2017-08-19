namespace CxxDemangler.Parsers
{
    internal class Decltype : IParsingResult
    {
        public Decltype(IParsingResult expression, bool idExpression)
        {
            Expression = expression;
            IdExpression = idExpression;
        }

        public bool IdExpression { get; private set; }

        public IParsingResult Expression { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            if (!context.Parser.VerifyString("D"))
            {
                return null;
            }

            RewindState rewind = context.RewindState;

            if (context.Parser.VerifyString("t"))
            {
                IParsingResult expression = Parsers.Expression.Parse(context);

                if (expression == null || !context.Parser.VerifyString("E"))
                {
                    context.Rewind(rewind);
                    return null;
                }

                return new Decltype(expression, idExpression: true);
            }

            if (context.Parser.VerifyString("T"))
            {
                IParsingResult expression = Parsers.Expression.Parse(context);

                if (expression == null || !context.Parser.VerifyString("E"))
                {
                    context.Rewind(rewind);
                    return null;
                }

                return new Decltype(expression, idExpression: false);
            }

            context.Rewind(rewind);
            return null;
        }
    }
}
