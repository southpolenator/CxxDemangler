namespace CxxDemangler.Parsers
{
    internal class CvQualifiers
    {
        public bool Restrict { get; private set; }

        public bool Volatile { get; private set; }

        public bool Const { get; private set; }

        public static CvQualifiers Parse(ParsingContext context)
        {
            CvQualifiers qualifiers = null;

            if (context.Parser.VerifyString("r"))
            {
                qualifiers = qualifiers ?? new CvQualifiers();
                qualifiers.Restrict = true;
            }

            if (context.Parser.VerifyString("V"))
            {
                qualifiers = qualifiers ?? new CvQualifiers();
                qualifiers.Volatile = true;
            }

            if (context.Parser.VerifyString("K"))
            {
                qualifiers = qualifiers ?? new CvQualifiers();
                qualifiers.Const = true;
            }

            return qualifiers;
        }
    }
}
