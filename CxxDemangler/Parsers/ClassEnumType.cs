namespace CxxDemangler.Parsers
{
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
            private IParsingResult name;

            public ElaboratedEnum(IParsingResult name)
            {
                this.name = name;
            }
        }

        internal class ElaboratedStruct : IParsingResult
        {
            private IParsingResult name;

            public ElaboratedStruct(IParsingResult name)
            {
                this.name = name;
            }
        }

        internal class ElaboratedUnion : IParsingResult
        {
            private IParsingResult name;

            public ElaboratedUnion(IParsingResult name)
            {
                this.name = name;
            }
        }
    }
}
