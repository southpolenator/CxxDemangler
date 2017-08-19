using System.Collections.Generic;

namespace CxxDemangler.Parsers
{
    internal class LambdaSig : IParsingResult
    {
        public List<IParsingResult> ArgumentTypes { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            if (context.Parser.VerifyString("v"))
            {
                return new LambdaSig()
                {
                    ArgumentTypes = new List<IParsingResult>(),
                };
            }

            List<IParsingResult> argumentTypes = CxxDemangler.ParseList(Type.Parse, context);

            if (argumentTypes.Count > 0)
            {
                return new LambdaSig()
                {
                    ArgumentTypes = argumentTypes,
                };
            }
            return null;
        }
    }
}
