namespace CxxDemangler.Parsers
{
    // <builtin-type> ::= v  # void
    //                ::= w  # wchar_t
    //                ::= b  # bool
    //                ::= c  # char
    //                ::= a  # signed char
    //                ::= h  # unsigned char
    //                ::= s  # short
    //                ::= t  # unsigned short
    //                ::= i  # int
    //                ::= j  # unsigned int
    //                ::= l  # long
    //                ::= m  # unsigned long
    //                ::= x  # long long, __int64
    //                ::= y  # unsigned long long, __int64
    //                ::= n  # __int128
    //                ::= o  # unsigned __int128
    //                ::= f  # float
    //                ::= d  # double
    //                ::= e  # long double, __float80
    //                ::= g  # __float128
    //                ::= z  # ellipsis
    //                ::= Dd # IEEE 754r decimal floating point (64 bits)
    //                ::= De # IEEE 754r decimal floating point (128 bits)
    //                ::= Df # IEEE 754r decimal floating point (32 bits)
    //                ::= Dh # IEEE 754r half-precision floating point (16 bits)
    //                ::= DF <number> _ # ISO/IEC TS 18661 binary floating point type _FloatN (N bits)
    //                ::= Di # char32_t
    //                ::= Ds # char16_t
    //                ::= Da # auto
    //                ::= Dc # decltype(auto)
    //                ::= Dn # std::nullptr_t (i.e., decltype(nullptr))
    //                ::= u <source-name>    # vendor extended type
    internal class BuiltinType : IParsingResult
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult type = StandardBuiltinType.Parse(context);

            if (type != null)
            {
                return type;
            }

            RewindState rewind = context.RewindState;

            if (context.Parser.VerifyString("u"))
            {
                IParsingResult name = SourceName.Parse(context);

                return new Extension(name);
            }

            context.Rewind(rewind);
            return null;
        }

        internal class Extension : BuiltinType
        {
            public Extension(IParsingResult name)
            {
                Name = name;
            }

            public IParsingResult Name { get; private set; }
        }
    }
}
