using System.Collections.Generic;

namespace CxxDemangler.Parsers
{
    internal class TemplateArgs : IParsingResult
    {
        public IReadOnlyList<IParsingResult> Arguments { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (context.Parser.VerifyString("I"))
            {
                List<IParsingResult> args = CxxDemangler.ParseList(TemplateArg.Parse, context);

                if (args.Count > 0 && context.Parser.VerifyString("E"))
                {
                    return new TemplateArgs()
                    {
                        Arguments = args,
                    };
                }
                context.Rewind(rewind);
            }

            return null;
        }
    }
}
