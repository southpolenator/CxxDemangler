namespace CxxDemangler.Parsers
{
    internal class UnresolvedType
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult param = TemplateParam.Parse(context);

            if (param != null)
            {
                IParsingResult args = TemplateArgs.Parse(context);
                IParsingResult result = new Template(param, args);

                // TODO: context.SubstitutionTable.Add(result);
                return result;
            }

            IParsingResult decltype = Decltype.Parse(context);

            if (decltype != null)
            {
                // TODO: context.SubstitutionTable.Add(decltype);
                return decltype;
            }

            return Substitution.Parse(context);
        }

        internal class Template : IParsingResult
        {
            private IParsingResult args;
            private IParsingResult param;

            public Template(IParsingResult param, IParsingResult args)
            {
                this.param = param;
                this.args = args;
            }
        }
    }
}
