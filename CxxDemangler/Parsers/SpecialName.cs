namespace CxxDemangler.Parsers
{
    internal class SpecialName
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (context.Parser.VerifyString("TV"))
            {
                IParsingResult type = Type.Parse(context);

                if (type != null)
                {
                    return new VirtualTable(type);
                }
                context.Rewind(rewind);
                return null;
            }
            if (context.Parser.VerifyString("TT"))
            {
                IParsingResult type = Type.Parse(context);

                if (type != null)
                {
                    return new Vtt(type);
                }
                context.Rewind(rewind);
                return null;
            }
            if (context.Parser.VerifyString("TI"))
            {
                IParsingResult type = Type.Parse(context);

                if (type != null)
                {
                    return new TypeInfo(type);
                }
                context.Rewind(rewind);
                return null;
            }
            if (context.Parser.VerifyString("TS"))
            {
                IParsingResult type = Type.Parse(context);

                if (type != null)
                {
                    return new TypeInfoName(type);
                }
                context.Rewind(rewind);
                return null;
            }
            if (context.Parser.VerifyString("Tc"))
            {
                IParsingResult thisOffset = CallOffset.Parse(context);

                if (thisOffset != null)
                {
                    IParsingResult resultOffset = CallOffset.Parse(context);

                    if (resultOffset != null)
                    {
                        IParsingResult encoding = Encoding.Parse(context);

                        if (encoding != null)
                        {
                            return new VirtualOverrideThunkCovariant(thisOffset, resultOffset, encoding);
                        }
                    }
                }
                context.Rewind(rewind);
                return null;
            }
            if (context.Parser.VerifyString("GV"))
            {
                IParsingResult name = Name.Parse(context);

                if (name != null)
                {
                    return new Guard(name);
                }
                context.Rewind(rewind);
                return null;
            }
            if (context.Parser.VerifyString("GR"))
            {
                IParsingResult name = Name.Parse(context);

                if (name != null)
                {
                    int index;

                    if (!context.Parser.ParseNumberBase36(out index))
                    {
                        index = -1;
                    }
                    index++;
                    if (context.Parser.VerifyString("_"))
                    {
                        return new GuardTemporary(name, index);
                    }
                }
                context.Rewind(rewind);
                return null;
            }
            if (context.Parser.VerifyString("T"))
            {
                IParsingResult offset = CallOffset.Parse(context);

                if (offset != null)
                {
                    IParsingResult encoding = Encoding.Parse(context);

                    if (encoding != null)
                    {
                        return new VirtualOverrideThunk(offset, encoding);
                    }
                }
                context.Rewind(rewind);
                return null;
            }
            return null;
        }

        internal class VirtualTable : IParsingResult
        {
            private IParsingResult type;

            public VirtualTable(IParsingResult type)
            {
                this.type = type;
            }
        }

        internal class Vtt : IParsingResult
        {
            private IParsingResult type;

            public Vtt(IParsingResult type)
            {
                this.type = type;
            }
        }

        internal class TypeInfo : IParsingResult
        {
            private IParsingResult type;

            public TypeInfo(IParsingResult type)
            {
                this.type = type;
            }
        }

        internal class TypeInfoName : IParsingResult
        {
            private IParsingResult type;

            public TypeInfoName(IParsingResult type)
            {
                this.type = type;
            }
        }

        internal class VirtualOverrideThunkCovariant : IParsingResult
        {
            private IParsingResult encoding;
            private IParsingResult resultOffset;
            private IParsingResult thisOffset;

            public VirtualOverrideThunkCovariant(IParsingResult thisOffset, IParsingResult resultOffset, IParsingResult encoding)
            {
                this.thisOffset = thisOffset;
                this.resultOffset = resultOffset;
                this.encoding = encoding;
            }
        }

        internal class Guard : IParsingResult
        {
            private IParsingResult name;

            public Guard(IParsingResult name)
            {
                this.name = name;
            }
        }

        internal class GuardTemporary : IParsingResult
        {
            private int index;
            private IParsingResult name;

            public GuardTemporary(IParsingResult name, int index)
            {
                this.name = name;
                this.index = index;
            }
        }

        internal class VirtualOverrideThunk : IParsingResult
        {
            private IParsingResult encoding;
            private IParsingResult offset;

            public VirtualOverrideThunk(IParsingResult offset, IParsingResult encoding)
            {
                this.offset = offset;
                this.encoding = encoding;
            }
        }
    }
}
