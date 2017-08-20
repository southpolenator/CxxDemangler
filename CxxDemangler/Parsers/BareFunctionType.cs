using System.Collections.Generic;

namespace CxxDemangler.Parsers
{
    // <bare-function-type> ::= <signature type>+
    //     # types are possible return type, then parameter types
    internal class BareFunctionType : IParsingResult
    {
        public BareFunctionType(IReadOnlyList<IParsingResult> types)
        {
            Types = types;
        }

        public IReadOnlyList<IParsingResult> Types { get; private set; }

        public static BareFunctionType Parse(ParsingContext context)
        {
            List<IParsingResult> types = CxxDemangler.ParseList(Type.Parse, context);

            if (types.Count > 0)
            {
                return new BareFunctionType(types);
            }

            return null;
        }
    }
}
