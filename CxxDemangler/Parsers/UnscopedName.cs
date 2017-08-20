namespace CxxDemangler.Parsers
{
    // <unscoped-name> ::= <unqualified-name>
    //                 ::= St <unqualified-name>   # ::std::
    internal class UnscopedName
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (context.Parser.VerifyString("St"))
            {
                IParsingResult name = UnqualifiedName.Parse(context);

                if (name == null)
                {
                    context.Rewind(rewind);
                    return null;
                }

                return new Std(name);
            }

            return UnqualifiedName.Parse(context);
        }

        internal class Std : IParsingResult
        {
            public Std(IParsingResult name)
            {
                Name = name;
            }

            public IParsingResult Name { get; private set; }
        }
    }
}
