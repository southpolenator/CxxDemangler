using System.Collections.Generic;

namespace CxxDemangler.Parsers
{
    internal class TemplateArg
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;
            IParsingResult expression;

            if (context.Parser.VerifyString("X"))
            {
                expression = Expression.Parse(context);
                if (expression != null && context.Parser.VerifyString("E"))
                {
                    return expression;
                }
                context.Rewind(rewind);
                return null;
            }

            expression = ExprPrimary.Parse(context);
            if (expression != null)
            {
                return expression;
            }

            IParsingResult type = Type.Parse(context);
            if (type != null)
            {
                return type;
            }

            if (context.Parser.VerifyString("J"))
            {
                List<IParsingResult> args = CxxDemangler.ParseList(TemplateArg.Parse, context);

                if (context.Parser.VerifyString("E"))
                {
                    return new ArgPack(args);
                }
                context.Rewind(rewind);
                return null;
            }
            return null;
        }

        internal class ArgPack : IParsingResult
        {
            private List<IParsingResult> args;

            public ArgPack(List<IParsingResult> args)
            {
                this.args = args;
            }
        }
    }
}
