namespace CxxDemangler.Parsers
{
    internal class NestedName : IParsingResult
    {
        public IParsingResult Prefix { get; private set; }

        public CvQualifiers CvQualifiers { get; private set; }

        public RefQualifier RefQualifier { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (!context.Parser.VerifyString("N"))
            {
                return null;
            }

            CvQualifiers cvQualifiers = CvQualifiers.Parse(context);
            RefQualifier refQualifier = RefQualifier.Parse(context);
            IParsingResult prefix = Parsers.Prefix.Parse(context);

            if (prefix == null)
            {
                context.Rewind(rewind);
                return null;
            }

            if (!context.Parser.VerifyString("E"))
            {
                context.Rewind(rewind);
                return null;
            }

            return new NestedName()
            {
                Prefix = prefix,
                CvQualifiers = cvQualifiers,
                RefQualifier = refQualifier,
            };
        }
    }
}
