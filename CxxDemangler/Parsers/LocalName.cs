namespace CxxDemangler.Parsers
{
    // <local-name> := Z <function encoding> E <entity name> [<discriminator>]
    //              := Z <function encoding> E s [<discriminator>]
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
            public Default(IParsingResult encoding, int? param, IParsingResult name)
            {
                Encoding = encoding;
                Param = param;
                Name = name;
            }

            public IParsingResult Encoding { get; private set; }

            public IParsingResult Name { get; private set; }

            public int? Param { get; private set; }
        }

        internal class Relative : IParsingResult
        {
            public Relative(IParsingResult encoding, IParsingResult name, Discriminator discriminator)
            {
                Encoding = encoding;
                Name = name;
                Discriminator = discriminator;
            }

            public Discriminator Discriminator { get; private set; }

            public IParsingResult Encoding { get; private set; }

            public IParsingResult Name { get; private set; }
        }
    }
}
