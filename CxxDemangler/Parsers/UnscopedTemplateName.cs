namespace CxxDemangler.Parsers
{
    // <unscoped-template-name> ::= <unscoped-name>
    //                          ::= <substitution>
    internal class UnscopedTemplateName
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult name = UnscopedName.Parse(context);

            if (name != null)
            {
                context.SubstitutionTable.Add(name);
                return name;
            }

            return Substitution.Parse(context);
        }
    }
}
