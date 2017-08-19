namespace CxxDemangler.Parsers
{
    internal class TemplateTemplateParam : IParsingResult
    {
        public IParsingResult Param { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult substitution = Substitution.Parse(context);

            if (substitution != null)
            {
                return substitution;
            }

            IParsingResult param = TemplateParam.Parse(context);

            if (param != null)
            {
                IParsingResult result = new TemplateTemplateParam()
                {
                    Param = param,
                };
                // TODO: context.SubstitutionTable.Add(result);
                return result;
            }
            return null;
        }
    }
}
