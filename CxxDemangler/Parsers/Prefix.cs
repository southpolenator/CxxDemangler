using System.Diagnostics;

namespace CxxDemangler.Parsers
{
    internal class Prefix
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;
            IParsingResult result = null;

            while (!context.Parser.IsEnd)
            {
                switch (context.Parser.Peek)
                {
                    case 'S':
                        {
                            IParsingResult substitution = Substitution.Parse(context);

                            if (substitution == null)
                            {
                                context.Rewind(rewind);
                                return null;
                            }

                            result = substitution;
                        }
                        break;
                    case 'T':
                        {
                            IParsingResult param = TemplateParam.Parse(context);

                            if (param == null)
                            {
                                context.Rewind(rewind);
                                return null;
                            }

                            result = AddToSubstitutionTable(context, param);
                        }
                        break;
                    case 'D':
                        {
                            IParsingResult decltype = Decltype.Parse(context);

                            if (decltype != null)
                            {
                                result = AddToSubstitutionTable(context, decltype);
                            }
                            else
                            {
                                IParsingResult name = UnqualifiedName.Parse(context);

                                if (name == null)
                                {
                                    context.Rewind(rewind);
                                    return null;
                                }

                                if (result != null)
                                {
                                    result = AddToSubstitutionTable(context, new NestedName(result, name));
                                }
                                else
                                {
                                    result = AddToSubstitutionTable(context, name);
                                }
                            }
                        }
                        break;
                    default:
                        if (context.Parser.Peek == 'I' && result != null /*TODO: && current.as_ref().unwrap().is_template_prefix(subs)*/)
                        {
                            IParsingResult args = TemplateArgs.Parse(context);

                            if (args == null)
                            {
                                context.Rewind(rewind);
                                return null;
                            }

                            result = AddToSubstitutionTable(context, new Template(result, args));
                        }
                        else if (result != null && SourceName.StartsWith(context))
                        {
                            Debug.Assert(UnqualifiedName.StartsWith(context));
                            Debug.Assert(DataMemberPrefix.StartsWith(context));

                            IParsingResult name = SourceName.Parse(context);

                            if (name == null)
                            {
                                context.Rewind(rewind);
                                return null;
                            }

                            if (context.Parser.VerifyString("M"))
                            {
                                result = AddToSubstitutionTable(context, new DataMember(result, name));
                            }
                            else
                            {
                                if (result != null)
                                {
                                    result = AddToSubstitutionTable(context, new NestedName(result, name));
                                }
                                else
                                {
                                    result = AddToSubstitutionTable(context, name);
                                }
                            }
                        }
                        else if (UnqualifiedName.StartsWith(context))
                        {
                            IParsingResult name = UnqualifiedName.Parse(context);

                            if (name == null)
                            {
                                context.Rewind(rewind);
                                return null;
                            }

                            if (result != null)
                            {
                                result = AddToSubstitutionTable(context, new NestedName(result, name));
                            }
                            else
                            {
                                result = AddToSubstitutionTable(context, name);
                            }
                        }
                        else
                        {
                            if (result != null)
                            {
                                return result;
                            }

                            context.Rewind(rewind);
                            return null;
                        }
                        break;
                }
            }

            return result;
        }

        private static IParsingResult AddToSubstitutionTable(ParsingContext context, IParsingResult result)
        {
            // TODO: context.SubstitutionTable.Add();
            return result;
        }

        internal class DataMember : IParsingResult
        {
            private IParsingResult name;
            private IParsingResult result;

            public DataMember(IParsingResult result, IParsingResult name)
            {
                this.result = result;
                this.name = name;
            }
        }

        internal class NestedName : IParsingResult
        {
            public NestedName(IParsingResult previous, IParsingResult name)
            {
                Previous = previous;
                Name = name;
            }

            public IParsingResult Previous { get; private set; }

            public IParsingResult Name { get; private set; }
        }

        internal class Template : IParsingResult
        {
            private IParsingResult args;
            private IParsingResult result;

            public Template(IParsingResult result, IParsingResult args)
            {
                this.result = result;
                this.args = args;
            }
        }
    }
}
