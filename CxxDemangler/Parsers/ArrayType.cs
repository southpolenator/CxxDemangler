using System.Diagnostics;

namespace CxxDemangler.Parsers
{
    // <array-type> ::= A <positive dimension number> _ <element type>
    //              ::= A[< dimension expression >] _<element type>
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
            public DimensionExpression(IParsingResult expression, IParsingResult type)
            {
                Expression = expression;
                Type = type;
            }

            public IParsingResult Expression { get; private set; }
            public IParsingResult Type { get; private set; }
        }

        internal class DimensionNumber : IParsingResult
        {
            public DimensionNumber(int number, IParsingResult type)
            {
                Number = number;
                Type = type;
            }

            public int Number { get; private set; }
            public IParsingResult Type { get; private set; }
        }

        internal class NoDimension : IParsingResult
        {
            public NoDimension(IParsingResult type)
            {
                Type = type;
            }

            public IParsingResult Type { get; private set; }
        }
    }
}
