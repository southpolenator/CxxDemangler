namespace CxxDemangler.Parsers
{
    internal class SimpleId : IParsingResult
    {
        public IParsingResult Name { get; private set; }
        public IParsingResult Arguments { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult name = SourceName.Parse(context);

            if (name != null)
            {
                IParsingResult args = TemplateArgs.Parse(context);

                return new SimpleId()
                {
                    Name = name,
                    Arguments = args,
                };
            }

            return null;
        }
    }
}
