namespace CxxDemangler.Parsers
{
    internal class Encoding
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult name = Name.Parse(context);

            if (name != null)
            {
                IParsingResult type = BareFunctionType.Parse(context);

                if (type != null)
                {
                    return new Function(name, type);
                }

                return name;
            }

            return SpecialName.Parse(context);
        }

        internal class Function : IParsingResult
        {
            public Function(IParsingResult name, IParsingResult type)
            {
                Name = name;
                Type = type;
            }

            public IParsingResult Name { get; private set; }

            public IParsingResult Type { get; private set; }
        }
    }
}
