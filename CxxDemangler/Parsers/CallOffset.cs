namespace CxxDemangler.Parsers
{
    internal class CallOffset
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (context.Parser.VerifyString("h"))
            {
                int offset;

                if (!context.Parser.ParseNumber(out offset) || !context.Parser.VerifyString("_"))
                {
                    context.Rewind(rewind);
                    return null;
                }

                return new NonVirtual(offset);
            }
            else if (context.Parser.VerifyString("v"))
            {
                int offset, virtualOffset;

                if (!context.Parser.ParseNumber(out offset) || !context.Parser.VerifyString("_")
                    || !context.Parser.ParseNumber(out virtualOffset) || !context.Parser.VerifyString("_"))
                {
                    context.Rewind(rewind);
                    return null;
                }

                return new Virtual(offset, virtualOffset);
            }

            return null;
        }

        internal class NonVirtual : IParsingResult
        {
            private int offset;

            public NonVirtual(int offset)
            {
                this.offset = offset;
            }
        }

        internal class Virtual : IParsingResult
        {
            private int offset;
            private int virtualOffset;

            public Virtual(int offset, int virtualOffset)
            {
                this.offset = offset;
                this.virtualOffset = virtualOffset;
            }
        }
    }
}
