using System.Collections.Generic;

namespace CxxDemangler.Parsers
{
    internal class Initializer : IParsingResult
    {
        public List<IParsingResult> Expressions { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (!context.Parser.VerifyString("pi"))
            {
                return null;
            }

            List<IParsingResult> expressions = CxxDemangler.ParseList(Expression.Parse, context);

            if (context.Parser.VerifyString("E"))
            {
                return new Initializer()
                {
                    Expressions = expressions,
                };
            }

            context.Rewind(rewind);
            return null;
        }
    }
}
