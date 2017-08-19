namespace CxxDemangler.Parsers
{
    internal class FunctionType : IParsingResult
    {
        public FunctionType(BareFunctionType bareType, CvQualifiers cvQualifiers, RefQualifier refQualifier, bool transactionSafe, bool externC)
        {
            CvQualifiers = cvQualifiers;
            TransactionSafe = transactionSafe;
            ExternC = externC;
            BareType = bareType;
            RefQualifier = refQualifier;
        }

        public CvQualifiers CvQualifiers { get; private set; }

        public bool TransactionSafe { get; private set; }

        public bool ExternC { get; private set; }

        public BareFunctionType BareType { get; private set; }

        public RefQualifier RefQualifier { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;
            CvQualifiers cvQualifiers = CvQualifiers.Parse(context);
            bool transactionSafe = context.Parser.VerifyString("Dx");

            if (context.Parser.VerifyString("F"))
            {
                bool externC = context.Parser.VerifyString("Y");
                BareFunctionType bareType = BareFunctionType.Parse(context);

                if (bareType != null)
                {
                    RefQualifier refQualifier = RefQualifier.Parse(context);

                    if (context.Parser.VerifyString("E"))
                    {
                        return new FunctionType(bareType, cvQualifiers, refQualifier, transactionSafe, externC);
                    }
                }
            }

            context.Rewind(rewind);
            return null;
        }
    }
}
