namespace CxxDemangler.Parsers
{
    internal class OperatorName
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult simple = SimpleOperatorName.Parse(context);

            if (simple != null)
            {
                return simple;
            }

            RewindState rewind = context.RewindState;

            if (context.Parser.VerifyString("cv"))
            {
                IParsingResult type = Type.Parse(context);

                if (type == null)
                {
                    context.Rewind(rewind);
                    return null;
                }

                return new Cast(type);
            }

            if (context.Parser.VerifyString("li"))
            {
                IParsingResult name = SourceName.Parse(context);

                if (name == null)
                {
                    context.Rewind(rewind);
                    return null;
                }

                return new Literal(name);
            }

            if (context.Parser.VerifyString("v"))
            {
                if (!char.IsDigit(context.Parser.Peek))
                {
                    context.Rewind(rewind);
                    return null;
                }

                int arity = context.Parser.Peek - '0';
                context.Parser.Position++;
                IParsingResult name = SourceName.Parse(context);

                if (name == null)
                {
                    context.Rewind(rewind);
                    return null;
                }

                return new VendorExtension(name);
            }

            return null;
        }

        public static bool StartsWith(ParsingContext context)
        {
            char peek = context.Parser.Peek;

            return peek == 'c' || peek == 'l' || peek == 'v' || SimpleOperatorName.StartsWith(context);
        }

        internal class Cast : IParsingResult
        {
            private IParsingResult type;

            public Cast(IParsingResult type)
            {
                this.type = type;
            }
        }

        internal class Literal : IParsingResult
        {
            private IParsingResult name;

            public Literal(IParsingResult name)
            {
                this.name = name;
            }
        }

        internal class VendorExtension : IParsingResult
        {
            private IParsingResult name;

            public VendorExtension(IParsingResult name)
            {
                this.name = name;
            }
        }
    }
}
