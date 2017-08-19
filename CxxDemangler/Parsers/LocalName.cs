namespace CxxDemangler.Parsers
{
    internal class LocalName
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (!context.Parser.VerifyString("Z"))
            {
                return null;
            }

            IParsingResult encoding = Encoding.Parse(context), name;
            Discriminator discriminator;

            if (encoding == null || !context.Parser.VerifyString("E"))
            {
                context.Rewind(rewind);
                return null;
            }

            if (context.Parser.VerifyString("s"))
            {
                discriminator = Discriminator.Parse(context);
                return new Relative(encoding, null, discriminator);
            }

            if (context.Parser.VerifyString("d"))
            {
                int? param = context.Parser.ParseNumber();

                if (context.Parser.VerifyString("_"))
                {
                    name = Name.Parse(context);
                    if (name != null)
                    {
                        return new Default(encoding, param, name);
                    }
                }
                context.Rewind(rewind);
                return null;
            }
            name = Name.Parse(context);
            if (name != null)
            {
                discriminator = Discriminator.Parse(context);
                return new Relative(encoding, name, discriminator);
            }
            context.Rewind(rewind);
            return null;
        }

        internal class Default : IParsingResult
        {
            private IParsingResult encoding;
            private IParsingResult name;
            private int? param;

            public Default(IParsingResult encoding, int? param, IParsingResult name)
            {
                this.encoding = encoding;
                this.param = param;
                this.name = name;
            }
        }

        internal class Relative : IParsingResult
        {
            private Discriminator discriminator;
            private IParsingResult encoding;
            private IParsingResult name;

            public Relative(IParsingResult encoding, IParsingResult name, Discriminator discriminator)
            {
                this.encoding = encoding;
                this.name = name;
                this.discriminator = discriminator;
            }
        }
    }
}
