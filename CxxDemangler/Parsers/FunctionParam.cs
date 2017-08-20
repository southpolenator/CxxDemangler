namespace CxxDemangler.Parsers
{
    internal class FunctionParam : IParsingResult
    {
        public FunctionParam(CvQualifiers cvQualifiers, int scope, int? param)
        {
            CvQualifiers = cvQualifiers;
            Scope = scope;
            Param = param;
        }

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
                    return new FunctionParam(qualifiers, scope, param);
                }
            }

            context.Rewind(rewind);
            return null;
        }
    }
}
