namespace CxxDemangler.Parsers
{
    //  <type> ::= <builtin-type>
    //         ::= <function-type>
    //         ::= <class-enum-type>
    //         ::= <array-type>
    //         ::= <pointer-to-member-type>
    //         ::= <template-param>
    //         ::= <template-template-param> <template-args>
    //         ::= <decltype>
    //         ::= <substitution>
    internal class Type
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            IParsingResult type = BuiltinType.Parse(context);

            if (type != null)
            {
                return type;
            }

            type = ClassEnumType.Parse(context);
            if (type != null)
            {
                return AddToSubstitutionTable(context, type);
            }

            RewindState rewind = context.RewindState;

            type = Substitution.Parse(context);
            if (type != null)
            {
                if (context.Parser.Peek != 'I')
                {
                    return type;
                }

                context.Rewind(rewind);
            }

            type = FunctionType.Parse(context) ?? ArrayType.Parse(context) ?? PointerToMemberType.Parse(context);
            if (type != null)
            {
                return AddToSubstitutionTable(context, type);
            }

            type = TemplateParam.Parse(context);
            if (type != null)
            {
                if (context.Parser.Peek != 'I')
                {
                    return AddToSubstitutionTable(context, type);
                }

                context.Rewind(rewind);
            }

            type = TemplateTemplateParam.Parse(context);
            if (type != null)
            {
                IParsingResult arguments = TemplateArgs.Parse(context);

                if (arguments == null)
                {
                    context.Rewind(rewind);
                    return null;
                }

                return AddToSubstitutionTable(context, new TemplateTemplate(type, arguments));
            }

            type = Decltype.Parse(context);
            if (type != null)
            {
                return type;
            }

            CvQualifiers qualifiers = CvQualifiers.Parse(context);

            if (qualifiers != null)
            {
                type = Parse(context);
                if (type == null)
                {
                    context.Rewind(rewind);
                    return null;
                }

                if (type is BuiltinType)
                {
                    return new QualifiedBuiltin(qualifiers, type);
                }
                return AddToSubstitutionTable(context, new Qualified(qualifiers, type));
            }

            if (context.Parser.VerifyString("P"))
            {
                type = Parse(context);
                if (type == null)
                {
                    context.Rewind(rewind);
                    return null;
                }
                return AddToSubstitutionTable(context, new PointerTo(type));
            }

            if (context.Parser.VerifyString("R"))
            {
                type = Parse(context);
                if (type == null)
                {
                    context.Rewind(rewind);
                    return null;
                }
                return AddToSubstitutionTable(context, new LvalueRef(type));
            }

            if (context.Parser.VerifyString("O"))
            {
                type = Parse(context);
                if (type == null)
                {
                    context.Rewind(rewind);
                    return null;
                }
                return AddToSubstitutionTable(context, new RvalueRef(type));
            }

            if (context.Parser.VerifyString("C"))
            {
                type = Parse(context);
                if (type == null)
                {
                    context.Rewind(rewind);
                    return null;
                }
                return AddToSubstitutionTable(context, new Complex(type));
            }

            if (context.Parser.VerifyString("G"))
            {
                type = Parse(context);
                if (type == null)
                {
                    context.Rewind(rewind);
                    return null;
                }
                return AddToSubstitutionTable(context, new Imaginary(type));
            }

            if (context.Parser.VerifyString("U"))
            {
                IParsingResult name = SourceName.Parse(context);

                if (name == null)
                {
                    context.Rewind(rewind);
                    return null;
                }

                IParsingResult arguments = TemplateArgs.Parse(context);

                type = Parse(context);
                return AddToSubstitutionTable(context, new VendorExtension(name, arguments, type));
            }

            if (context.Parser.VerifyString("Dp"))
            {
                type = Parse(context);
                if (type == null)
                {
                    context.Rewind(rewind);
                    return null;
                }
                return AddToSubstitutionTable(context, new PackExtension(type));
            }

            return null;
        }

        private static IParsingResult AddToSubstitutionTable(ParsingContext context, IParsingResult result)
        {
            context.SubstitutionTable.Add(result);
            return result;
        }

        internal class Complex : IParsingResult
        {
            public Complex(IParsingResult type)
            {
                Type = type;
            }

            public IParsingResult Type { get; private set; }
        }

        internal class Imaginary : IParsingResult
        {
            public Imaginary(IParsingResult type)
            {
                Type = type;
            }

            public IParsingResult Type { get; private set; }
        }

        internal class LvalueRef : IParsingResult
        {
            public LvalueRef(IParsingResult type)
            {
                Type = type;
            }

            public IParsingResult Type { get; private set; }
        }

        internal class PackExtension : IParsingResult
        {
            public PackExtension(IParsingResult type)
            {
                Type = type;
            }

            public IParsingResult Type { get; private set; }
        }

        internal class PointerTo : IParsingResult
        {
            public PointerTo(IParsingResult type)
            {
                Type = type;
            }

            public IParsingResult Type { get; private set; }
        }

        internal class RvalueRef : IParsingResult
        {
            public RvalueRef(IParsingResult type)
            {
                Type = type;
            }

            public IParsingResult Type { get; private set; }
        }

        internal class QualifiedBuiltin : IParsingResult
        {
            public QualifiedBuiltin(CvQualifiers qualifiers, IParsingResult type)
            {
                CvQualifiers = qualifiers;
                Type = type;
            }

            public CvQualifiers CvQualifiers { get; private set; }
            public IParsingResult Type { get; private set; }
        }

        internal class Qualified : IParsingResult
        {
            public Qualified(CvQualifiers qualifiers, IParsingResult type)
            {
                CvQualifiers = qualifiers;
                Type = type;
            }

            public CvQualifiers CvQualifiers { get; private set; }
            public IParsingResult Type { get; private set; }
        }

        internal class TemplateTemplate : IParsingResult
        {
            public TemplateTemplate(IParsingResult type, IParsingResult arguments)
            {
                Type = type;
                Arguments = arguments;
            }

            public IParsingResult Arguments { get; private set; }
            public IParsingResult Type { get; private set; }
        }

        internal class VendorExtension : IParsingResult
        {
            public VendorExtension(IParsingResult name, IParsingResult arguments, IParsingResult type)
            {
                Name = name;
                Arguments = arguments;
                Type = type;
            }

            public IParsingResult Arguments { get; private set; }
            public IParsingResult Name { get; private set; }
            public IParsingResult Type { get; private set; }
        }
    }
}
