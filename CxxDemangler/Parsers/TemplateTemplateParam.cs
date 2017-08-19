namespace CxxDemangler.Parsers
{
    internal class TemplateTemplateParam : IParsingResult
    {
        public TemplateTemplateParam(IParsingResult parameter)
        {
            Parameter = parameter;
        }

        public IParsingResult Parameter { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult substitution = Substitution.Parse(context);

            if (substitution != null)
            {
                return substitution;
            }

            IParsingResult parameter = TemplateParam.Parse(context);

            if (parameter != null)
            {
                IParsingResult result = new TemplateTemplateParam(parameter);
                // TODO: context.SubstitutionTable.Add(result);
                return result;
            }
            return null;
        }
    }
}
