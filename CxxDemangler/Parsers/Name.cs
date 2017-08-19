namespace CxxDemangler.Parsers
{
    internal class Name
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;
            IParsingResult name = NestedName.Parse(context);

            if (name != null)
            {
                return name;
            }

            name = UnscopedName.Parse(context);
            if (name != null)
            {
                if (context.Parser.Peek == 'I')
                {
                    // TODO: context.SubstitutionTable.Add(name);
                    IParsingResult args = TemplateArgs.Parse(context);

                    if (args == null)
                    {
                        context.Rewind(rewind);
                        // TODO: Remove all table elements (Rewind should do it)
                        return null;
                    }

                    return new UnscopedTemplate(name, args);
                }
                else
                {
                    return name;
                }
            }

            name = UnscopedTemplateName.Parse(context);
            if (name != null)
            {
                IParsingResult args = TemplateArgs.Parse(context);

                if (args == null)
                {
                    context.Rewind(rewind);
                    return null;
                }

                return new UnscopedTemplate(name, args);
            }

            return LocalName.Parse(context);
        }

        internal class UnscopedTemplate : IParsingResult
        {
            public UnscopedTemplate(IParsingResult name, IParsingResult args)
            {
                Name = name;
                Args = args;
            }

            public IParsingResult Name { get; private set; }

            public IParsingResult Args { get; private set; }
        }
    }
}
