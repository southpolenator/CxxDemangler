namespace CxxDemangler.Parsers
{
    // <pointer-to-member-type> ::= M <class type> <member type>
    internal class PointerToMemberType : IParsingResult
    {
        public PointerToMemberType(IParsingResult type, IParsingResult member)
        {
            Type = type;
            Member = member;
        }

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
                        return new PointerToMemberType(type, member);
                    }
                }
                context.Rewind(rewind);
            }
            return null;
        }
    }
}
