namespace CxxDemangler.Parsers
{
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

                // TODO: Use std::
                return name;
            }

            return UnqualifiedName.Parse(context);
        }
    }
}
