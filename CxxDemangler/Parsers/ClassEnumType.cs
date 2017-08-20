namespace CxxDemangler.Parsers
{
    // <class-enum-type> ::= <name>     # non-dependent type name, dependent type name, or dependent typename-specifier
    //                   ::= Ts <name>  # dependent elaborated type specifier using 'struct' or 'class'
    //                   ::= Tu <name>  # dependent elaborated type specifier using 'union'
    //                   ::= Te<name>  # dependent elaborated type specifier using 'enum'
    internal class ClassEnumType
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult name = Name.Parse(context);

            if (name != null)
            {
                return name;
            }

            RewindState rewind = context.RewindState;

            if (context.Parser.VerifyString("T"))
            {
                if (context.Parser.VerifyString("s"))
                {
                    name = Name.Parse(context);
                    if (name != null)
                    {
                        return new ElaboratedStruct(name);
                    }
                }
                else if (context.Parser.VerifyString("u"))
                {
                    name = Name.Parse(context);
                    if (name != null)
                    {
                        return new ElaboratedUnion(name);
                    }
                }
                else if (context.Parser.VerifyString("e"))
                {
                    name = Name.Parse(context);
                    if (name != null)
                    {
                        return new ElaboratedEnum(name);
                    }
                }
            }

            context.Rewind(rewind);
            return null;
        }

        internal class ElaboratedEnum : IParsingResult
        {
            public ElaboratedEnum(IParsingResult name)
            {
                Name = name;
            }

            public IParsingResult Name { get; private set; }
        }

        internal class ElaboratedStruct : IParsingResult
        {
            public ElaboratedStruct(IParsingResult name)
            {
                Name = name;
            }

            public IParsingResult Name { get; private set; }
        }

        internal class ElaboratedUnion : IParsingResult
        {
            public ElaboratedUnion(IParsingResult name)
            {
                Name = name;
            }

            public IParsingResult Name { get; private set; }
        }
    }
}
