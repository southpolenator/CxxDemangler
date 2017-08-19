namespace CxxDemangler.Parsers
{
    internal class WellKnownComponent : IParsingResult
    {
        public enum Values
        {
            [DictionaryValue("St", "std")]
            Std,

            [DictionaryValue("Sa", "std::allocator")]
            StdAllocator,

            [DictionaryValue("Sb", "std::basic_string")]
            StdString1,

            [DictionaryValue("Ss", "std::string")]
            StdString2,

            [DictionaryValue("Si", "std::basic_istream<char, std::char_traits<char> >")]
            StdIstream,

            [DictionaryValue("So", "std::ostream")]
            StdOstream,

            [DictionaryValue("Sd", "std::basic_iostream<char, std::char_traits<char> >")]
            StdIostream,
        }

        public Values Value { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            Values value;

            if (DictionaryParser<Values>.Parse(context, out value))
            {
                return new WellKnownComponent()
                {
                    Value = value,
                };
            }

            return null;
        }

        public static bool StartsWith(ParsingContext context)
        {
            return DictionaryParser<Values>.StartsWith(context);
        }
    }
}
