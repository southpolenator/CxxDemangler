namespace CxxDemangler.Parsers
{
    internal class UnresolvedQualifierLevel
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            return SimpleId.Parse(context);
        }
    }
}
