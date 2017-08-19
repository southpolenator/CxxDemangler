namespace CxxDemangler.Parsers
{
    internal class UnqualifiedName
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            return OperatorName.Parse(context) ?? CtorDtorName.Parse(context) ?? SourceName.Parse(context) ?? UnnamedTypeName.Parse(context);
        }

        public static bool StartsWith(ParsingContext context)
        {
            return OperatorName.StartsWith(context) || CtorDtorName.StartsWith(context) || SourceName.StartsWith(context) || UnnamedTypeName.StartsWith(context);
        }
    }
}
