namespace CxxDemangler.Parsers
{
    internal class PointerToMemberType : IParsingResult
    {
        public IParsingResult Type { get; private set; }

        public IParsingResult Member { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (context.Parser.VerifyString("M"))
            {
                IParsingResult type = Parsers.Type.Parse(context);

                if (type != null)
                {
                    IParsingResult member = Parsers.Type.Parse(context);

                    if (member != null)
                    {
                        return new PointerToMemberType()
                        {
                            Type = type,
                            Member = member,
                        };
                    }
                }
                context.Rewind(rewind);
            }
            return null;
        }
    }
}
