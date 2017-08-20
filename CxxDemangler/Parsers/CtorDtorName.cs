namespace CxxDemangler.Parsers
{
    //<ctor-dtor-name> ::= C1 # complete object constructor
    //                 ::= C2 # base object constructor
    //                 ::= C3 # complete object allocating constructor
    //                 ::= D0 # deleting destructor
    //                 ::= D1 # complete object destructor
    //                 ::= D2 # base object destructor
    internal class CtorDtorName : IParsingResult
    {
        public enum Values
        {
            [DictionaryValue("C1", "complete object constructor")]
            CompleteConstructor,

            [DictionaryValue("C2", "base object constructor")]
            BaseConstructor,

            [DictionaryValue("C3", "complete object allocating constructor")]
            CompleteAllocatingConstructor,

            [DictionaryValue("C4", "maybe in-charge constructor")]
            MaybeInChargeConstructor,

            [DictionaryValue("D0", "deleting destructor")]
            DeletingDestructor,

            [DictionaryValue("D1", "complete object destructor")]
            CompleteDestructor,

            [DictionaryValue("D2", "base object destructor")]
            BaseDestructor,

            [DictionaryValue("D4", "maybe in-charge destructor")]
            MaybeInChargeDestructor,
        }

        public CtorDtorName(Values value)
        {
            Value = value;
        }

        public Values Value { get; private set; }

        public static IParsingResult Parse(ParsingContext context)
        {
            Values value;

            if (DictionaryParser<Values>.Parse(context, out value))
            {
                return new CtorDtorName(value);
            }

            return null;
        }

        public static bool StartsWith(ParsingContext context)
        {
            return DictionaryParser<Values>.StartsWith(context);
        }
    }
}
