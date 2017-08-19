namespace CxxDemangler.Parsers
{
    internal class FunctionParam : IParsingResult
    {
        public int Scope { get; private set; }

        public CvQualifiers CvQualifiers { get; private set; }

        public int? Param { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (!context.Parser.VerifyString("f"))
            {
                return null;
            }

            int scope = 0;

            if (context.Parser.VerifyString("L"))
            {
                if (!context.Parser.ParseNumber(out scope))
                {
                    context.Rewind(rewind);
                    return null;
                }
            }

            if (context.Parser.VerifyString("p"))
            {
                CvQualifiers qualifiers = CvQualifiers.Parse(context);
                int? param = context.Parser.ParseNumber();

                if (context.Parser.VerifyString("_"))
                {
                    return new FunctionParam()
                    {
                        Scope = scope,
                        CvQualifiers = qualifiers,
                        Param = param,
                    };
                }
            }

            context.Rewind(rewind);
            return null;
        }
    }
}
