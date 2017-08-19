namespace CxxDemangler.Parsers
{
    internal class Substitution : IParsingResult
    {
        public int Reference { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult wellKnown = WellKnownComponent.Parse(context);

            if (wellKnown != null)
            {
                return wellKnown;
            }

            RewindState rewind = context.RewindState;

            if (!context.Parser.VerifyString("S"))
            {
                return null;
            }

            int number;

            if (!context.Parser.ParseNumberBase36(out number))
            {
                number = -1;
            }
            number++;


            if (!context.Parser.VerifyString("_"))// TODO: || !context.SubstitutionTable.Contains(number))
            {
                context.Rewind(rewind);
                return null;
            }

            return new Substitution()
            {
                Reference = number,
            };
        }
    }
}
