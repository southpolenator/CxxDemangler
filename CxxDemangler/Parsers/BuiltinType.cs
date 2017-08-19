namespace CxxDemangler.Parsers
{
    internal class BuiltinType : IParsingResult
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult type = StandardBuiltinType.Parse(context);

            if (type != null)
            {
                return type;
            }

            RewindState rewind = context.RewindState;

            if (context.Parser.VerifyString("u"))
            {
                IParsingResult name = SourceName.Parse(context);

                return new Extension(name);
            }

            context.Rewind(rewind);
            return null;
        }

        internal class Extension : BuiltinType
        {
            private IParsingResult name;

            public Extension(IParsingResult name)
            {
                this.name = name;
            }
        }
    }
}
