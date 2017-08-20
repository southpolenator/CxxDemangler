namespace CxxDemangler.Parsers
{
    internal class SimpleId : IParsingResult
    {
        public SimpleId(IParsingResult name, IParsingResult arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public IParsingResult Name { get; private set; }
        public IParsingResult Arguments { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult name = SourceName.Parse(context);

            if (name != null)
            {
                IParsingResult arguments = TemplateArgs.Parse(context);

                return new SimpleId(name, arguments);
            }

            return null;
        }
    }
}
