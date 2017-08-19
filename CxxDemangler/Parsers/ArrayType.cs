using System.Diagnostics;

namespace CxxDemangler.Parsers
{
    internal class ArrayType
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (!context.Parser.VerifyString("A"))
            {
                return null;
            }

            int number;
            IParsingResult type;

            if (context.Parser.ParseNumber(out number))
            {
                Debug.Assert(number >= 0);
                if (context.Parser.VerifyString("_"))
                {
                    type = Type.Parse(context);
                    if (type != null)
                    {
                        return new DimensionNumber(number, type);
                    }
                }
                context.Rewind(rewind);
                return null;
            }

            IParsingResult expression = Expression.Parse(context);

            if (expression != null)
            {
                if (context.Parser.VerifyString("_"))
                {
                    type = Type.Parse(context);
                    if (type != null)
                    {
                        return new DimensionExpression(expression, type);
                    }
                }
                context.Rewind(rewind);
                return null;
            }

            if (context.Parser.VerifyString("_"))
            {
                type = Type.Parse(context);
                if (type != null)
                {
                    return new NoDimension(type);
                }
            }

            context.Rewind(rewind);
            return null;
        }

        internal class DimensionExpression : IParsingResult
        {
            private IParsingResult expression;
            private IParsingResult type;

            public DimensionExpression(IParsingResult expression, IParsingResult type)
            {
                this.expression = expression;
                this.type = type;
            }
        }

        internal class DimensionNumber : IParsingResult
        {
            private int number;
            private IParsingResult type;

            public DimensionNumber(int number, IParsingResult type)
            {
                this.number = number;
                this.type = type;
            }
        }

        internal class NoDimension : IParsingResult
        {
            private IParsingResult type;

            public NoDimension(IParsingResult type)
            {
                this.type = type;
            }
        }
    }
}
