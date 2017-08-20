namespace CxxDemangler.Parsers
{
    // <unresolved-type> ::= <template-param> [ <template-args> ]            # T:: or T<X,Y>::
    //                   ::= <decltype>                                      # decltype(p)::
    //                   ::= <substitution>
    internal class UnresolvedType
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult param = TemplateParam.Parse(context);

            if (param != null)
            {
                IParsingResult args = TemplateArgs.Parse(context);
                IParsingResult result = new Template(param, args);

                context.SubstitutionTable.Add(result);
                return result;
            }

            IParsingResult decltype = Decltype.Parse(context);

            if (decltype != null)
            {
                context.SubstitutionTable.Add(decltype);
                return decltype;
            }

            return Substitution.Parse(context);
        }

        internal class Template : IParsingResult
        {
            public Template(IParsingResult parameter, IParsingResult arguments)
            {
                Parameter = parameter;
                Arguments = arguments;
            }

            public IParsingResult Arguments;

            public IParsingResult Parameter;
        }
    }
}
