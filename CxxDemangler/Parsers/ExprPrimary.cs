namespace CxxDemangler.Parsers
{
    internal class ExprPrimary
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (!context.Parser.VerifyString("L"))
            {
                return null;
            }

            IParsingResult type = Type.Parse(context);

            if (type != null)
            {
                string literal = context.Parser.ParseUntil('E');

                if (context.Parser.VerifyString("E"))
                {
                    return new Literal(type, literal);
                }

                context.Rewind(rewind);
                return null;
            }

            IParsingResult name = MangledName.Parse(context);

            if (name != null && context.Parser.VerifyString("E"))
            {
                return new External(name);
            }

            context.Rewind(rewind);
            return null;
        }

        internal class External : IParsingResult
        {
            private IParsingResult name;

            public External(IParsingResult name)
            {
                this.name = name;
            }
        }

        internal class Literal : IParsingResult
        {
            private string literal;
            private IParsingResult type;

            public Literal(IParsingResult type, string literal)
            {
                this.type = type;
                this.literal = literal;
            }
        }
    }
}
