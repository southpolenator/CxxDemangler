namespace CxxDemangler.Parsers
{
    internal class RefQualifier
    {
        public enum Values
        {
            [DictionaryValue("R", "&")]
            LValueRef,

            [DictionaryValue("O", "&&")]
            RValueRef,
        }

        public Values Value { get; private set; }

        public static RefQualifier Parse(ParsingContext context)
        {
            Values value;

            if (DictionaryParser<Values>.Parse(context, out value))
            {
                return new RefQualifier()
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
