using System.Collections.Generic;

namespace CxxDemangler.Parsers
{
    internal class UnresolvedName
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;
            IParsingResult name, type;
            List<IParsingResult> levels;

            if (context.Parser.VerifyString("gs"))
            {
                name = BaseUnresolvedName.Parse(context);
                if (name != null)
                {
                    return new Global(name);
                }
                if (context.Parser.VerifyString("sr"))
                {
                    levels = CxxDemangler.ParseList(UnresolvedQualifierLevel.Parse, context);
                    if (levels.Count > 0 && context.Parser.VerifyString("E"))
                    {
                        name = BaseUnresolvedName.Parse(context);
                        if (name != null)
                        {
                            return new GlobalNested2(levels, name);
                        }
                    }
                }
                context.Rewind(rewind);
                return null;
            }
            name = BaseUnresolvedName.Parse(context);
            if (name != null)
            {
                return name;
            }
            if (!context.Parser.VerifyString("sr"))
            {
                return null;
            }
            if (context.Parser.VerifyString("N"))
            {
                type = UnresolvedType.Parse(context);
                if (type != null)
                {
                    levels = CxxDemangler.ParseList(UnresolvedQualifierLevel.Parse, context);
                    if (levels.Count > 0 && context.Parser.VerifyString("E"))
                    {
                        name = BaseUnresolvedName.Parse(context);
                        if (name != null)
                        {
                            return new Nested1(type, levels, name);
                        }
                    }
                }
                context.Rewind(rewind);
                return null;
            }
            type = UnresolvedType.Parse(context);
            if (type != null)
            {
                name = BaseUnresolvedName.Parse(context);
                if (name != null)
                {
                    return new Nested1(type, new List<IParsingResult>(), name);
                }
                context.Rewind(rewind);
                return null;
            }

            levels = CxxDemangler.ParseList(UnresolvedQualifierLevel.Parse, context);
            if (levels.Count > 0 && context.Parser.VerifyString("E"))
            {
                name = BaseUnresolvedName.Parse(context);
                if (name != null)
                {
                    return new Nested2(levels, name);
                }
            }
            context.Rewind(rewind);
            return null;
        }

        internal class Global : IParsingResult
        {
            private IParsingResult name;

            public Global(IParsingResult name)
            {
                this.name = name;
            }
        }

        internal class GlobalNested2 : IParsingResult
        {
            private List<IParsingResult> levels;
            private IParsingResult name;

            public GlobalNested2(List<IParsingResult> levels, IParsingResult name)
            {
                this.levels = levels;
                this.name = name;
            }
        }

        internal class Nested1 : IParsingResult
        {
            private List<IParsingResult> levels;
            private IParsingResult name;
            private IParsingResult type;

            public Nested1(IParsingResult type, List<IParsingResult> levels, IParsingResult name)
            {
                this.type = type;
                this.levels = levels;
                this.name = name;
            }
        }

        internal class Nested2 : IParsingResult
        {
            private List<IParsingResult> levels;
            private IParsingResult name;

            public Nested2(List<IParsingResult> levels, IParsingResult name)
            {
                this.levels = levels;
                this.name = name;
            }
        }
    }
}
