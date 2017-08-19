namespace CxxDemangler.Parsers
{
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
                IParsingResult args = TemplateArgs.Parse(context);

                if (args == null)
                {
                    context.Rewind(rewind);
                    return null;
                }

                return AddToSubstitutionTable(context, new TemplateTemplate(type, args));
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
                return AddToSubstitutionTable(context, type);
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

                IParsingResult args = TemplateArgs.Parse(context);

                type = Parse(context);
                return AddToSubstitutionTable(context, new VendorExtension(name, args, type));
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
            // TODO: context.SubstitutionTable.Add();
            return result;
        }

        internal class Complex : IParsingResult
        {
            private IParsingResult type;

            public Complex(IParsingResult type)
            {
                this.type = type;
            }
        }

        internal class Imaginary : IParsingResult
        {
            private IParsingResult type;

            public Imaginary(IParsingResult type)
            {
                this.type = type;
            }
        }

        internal class LvalueRef : IParsingResult
        {
            private IParsingResult type;

            public LvalueRef(IParsingResult type)
            {
                this.type = type;
            }
        }

        internal class PackExtension : IParsingResult
        {
            private IParsingResult type;

            public PackExtension(IParsingResult type)
            {
                this.type = type;
            }
        }

        internal class PointerTo : IParsingResult
        {
            private IParsingResult type;

            public PointerTo(IParsingResult type)
            {
                this.type = type;
            }
        }

        internal class QualifiedBuiltin : IParsingResult
        {
            private CvQualifiers qualifiers;
            private IParsingResult type;

            public QualifiedBuiltin(CvQualifiers qualifiers, IParsingResult type)
            {
                this.qualifiers = qualifiers;
                this.type = type;
            }
        }

        internal class RvalueRef : IParsingResult
        {
            private IParsingResult type;

            public RvalueRef(IParsingResult type)
            {
                this.type = type;
            }
        }

        internal class TemplateTemplate : IParsingResult
        {
            private IParsingResult args;
            private IParsingResult type;

            public TemplateTemplate(IParsingResult type, IParsingResult args)
            {
                this.type = type;
                this.args = args;
            }
        }

        internal class VendorExtension : IParsingResult
        {
            private IParsingResult args;
            private IParsingResult name;
            private IParsingResult type;

            public VendorExtension(IParsingResult name, IParsingResult args, IParsingResult type)
            {
                this.name = name;
                this.args = args;
                this.type = type;
            }
        }
    }
}
