namespace CxxDemangler.Parsers
{
    // <operator-name> ::= nw	# new
    //                 ::= na	# new[]
    //                 ::= dl	# delete
    //                 ::= da	# delete[]
    //                 ::= ps   # + (unary)
    //                 ::= ng	# - (unary)
    //                 ::= ad	# & (unary)
    //                 ::= de	# * (unary)
    //                 ::= co	# ~
    //                 ::= pl	# +
    //                 ::= mi	# -
    //                 ::= ml	# *
    //                 ::= dv	# /
    //                 ::= rm	# %
    //                 ::= an	# &
    //                 ::= or	# |
    //                 ::= eo	# ^
    //                 ::= aS	# =
    //                 ::= pL	# +=
    //                 ::= mI	# -=
    //                 ::= mL	# *=
    //                 ::= dV	# /=
    //                 ::= rM	# %=
    //                 ::= aN	# &=
    //                 ::= oR	# |=
    //                 ::= eO	# ^=
    //                 ::= ls	# <<
    //                 ::= rs	# >>
    //                 ::= lS	# <<=
    //                 ::= rS	# >>=
    //                 ::= eq	# ==
    //                 ::= ne	# !=
    //                 ::= lt	# <
    //                 ::= gt	# >
    //                 ::= le	# <=
    //                 ::= ge	# >=
    //                 ::= nt	# !
    //                 ::= aa	# &&
    //                 ::= oo	# ||
    //                 ::= pp	# ++ (postfix in <expression> context)
    //                 ::= mm	# -- (postfix in <expression> context)
    //                 ::= cm	# ,
    //                 ::= pm	# ->*
    //                 ::= pt	# ->
    //                 ::= cl	# ()
    //                 ::= ix	# []
    //                 ::= qu	# ?
    //                 ::= cv <type>	# (cast)
    //                 ::= li <source-name>          # operator ""
    //                 ::= v <digit> <source-name>	# vendor extended operator
    internal class OperatorName
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult simple = SimpleOperatorName.Parse(context);

            if (simple != null)
            {
                return simple;
            }

            RewindState rewind = context.RewindState;

            if (context.Parser.VerifyString("cv"))
            {
                IParsingResult type = Type.Parse(context);

                if (type == null)
                {
                    context.Rewind(rewind);
                    return null;
                }

                return new Cast(type);
            }

            if (context.Parser.VerifyString("li"))
            {
                IParsingResult name = SourceName.Parse(context);

                if (name == null)
                {
                    context.Rewind(rewind);
                    return null;
                }

                return new Literal(name);
            }

            if (context.Parser.VerifyString("v"))
            {
                if (!char.IsDigit(context.Parser.Peek))
                {
                    context.Rewind(rewind);
                    return null;
                }

                int arity = context.Parser.Peek - '0';
                context.Parser.Position++;
                IParsingResult name = SourceName.Parse(context);

                if (name == null)
                {
                    context.Rewind(rewind);
                    return null;
                }

                return new VendorExtension(name);
            }

            return null;
        }

        public static bool StartsWith(ParsingContext context)
        {
            char peek = context.Parser.Peek;

            return peek == 'c' || peek == 'l' || peek == 'v' || SimpleOperatorName.StartsWith(context);
        }

        internal class Cast : IParsingResult
        {
            public Cast(IParsingResult type)
            {
                Type = type;
            }

            public IParsingResult Type { get; private set; }
        }

        internal class Literal : IParsingResult
        {
            public Literal(IParsingResult name)
            {
                Name = name;
            }

            public IParsingResult Name { get; private set; }
        }

        internal class VendorExtension : IParsingResult
        {
            public VendorExtension(IParsingResult name)
            {
                Name = name;
            }

            public IParsingResult Name { get; private set; }
        }
    }
}
