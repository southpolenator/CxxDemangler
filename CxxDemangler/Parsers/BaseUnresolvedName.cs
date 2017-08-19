namespace CxxDemangler.Parsers
{
    internal class BaseUnresolvedName
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;
            IParsingResult name = SimpleId.Parse(context);

            if (name != null)
            {
                return name;
            }
            if (context.Parser.VerifyString("on"))
            {
                IParsingResult operatorName = OperatorName.Parse(context);

                if (operatorName != null)
                {
                    IParsingResult arguments = TemplateArgs.Parse(context);

                    return new Operator(operatorName, arguments);
                }
                context.Rewind(rewind);
                return null;
            }
            if (context.Parser.VerifyString("dn"))
            {
                return DestructorName.Parse(context);
            }
            return null;
        }

        internal class Operator : IParsingResult
        {
            private IParsingResult arguments;
            private IParsingResult operatorName;

            public Operator(IParsingResult operatorName, IParsingResult arguments)
            {
                this.operatorName = operatorName;
                this.arguments = arguments;
            }
        }
    }
}
