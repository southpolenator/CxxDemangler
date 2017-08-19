using System;
using System.Collections.Generic;

namespace CxxDemangler.Parsers
{
    internal class Expression
    {
        public static IParsingResult Parse(ParsingContext context)
        {
            RewindState rewind = context.RewindState;

            if (context.Parser.VerifyString("pp_"))
            {
                return Parse<PrefixInc>(rewind, context, Expression.Parse);
            }
            if (context.Parser.VerifyString("mm_"))
            {
                return Parse<PrefixDec>(rewind, context, Expression.Parse);
            }
            if (context.Parser.VerifyString("cl"))
            {
                return Parse<Call>(rewind, context, Expression.Parse, ZeroOrMore(Expression.Parse));
            }
            if (context.Parser.VerifyString("cv"))
            {
                IParsingResult type = Type.Parse(context);

                if (type != null)
                {
                    if (context.Parser.VerifyString("_"))
                    {
                        List<IParsingResult> expressions = CxxDemangler.ParseList(Expression.Parse, context);

                        if (context.Parser.VerifyString("E"))
                        {
                            return new ConversionMany(type, expressions);
                        }
                    }
                    else
                    {
                        IParsingResult expression = Expression.Parse(context);

                        if (expression != null)
                        {
                            return new ConversionOne(type, expression);
                        }
                    }
                }
                context.Rewind(rewind);
                return null;
            }
            if (context.Parser.VerifyString("tl"))
            {
                return ParseWithEnd<ConversionBraced>(rewind, context, Type.Parse, ZeroOrMore(Expression.Parse));
            }
            if (context.Parser.VerifyString("il"))
            {
                return ParseWithEnd<BracedInitList>(rewind, context, Expression.Parse);
            }
            if (context.Parser.VerifyString("dc"))
            {
                return Parse<DynamicCast>(rewind, context, Type.Parse, Expression.Parse);
            }
            if (context.Parser.VerifyString("sc"))
            {
                return Parse<StaticCast>(rewind, context, Type.Parse, Expression.Parse);
            }
            if (context.Parser.VerifyString("cc"))
            {
                return Parse<ConstCast>(rewind, context, Type.Parse, Expression.Parse);
            }
            if (context.Parser.VerifyString("rc"))
            {
                return Parse<ReinterpretCast>(rewind, context, Type.Parse, Expression.Parse);
            }
            if (context.Parser.VerifyString("ti"))
            {
                return Parse<TypeIdType>(rewind, context, Type.Parse);
            }
            if (context.Parser.VerifyString("te"))
            {
                return Parse<TypeIdExpression>(rewind, context, Expression.Parse);
            }
            if (context.Parser.VerifyString("st"))
            {
                return Parse<SizeOfType>(rewind, context, Type.Parse);
            }
            if (context.Parser.VerifyString("sz"))
            {
                return Parse<SizeOfExpression>(rewind, context, Expression.Parse);
            }
            if (context.Parser.VerifyString("at"))
            {
                return Parse<AlignOfType>(rewind, context, Type.Parse);
            }
            if (context.Parser.VerifyString("az"))
            {
                return Parse<AlignOfExpression>(rewind, context, Expression.Parse);
            }
            if (context.Parser.VerifyString("nx"))
            {
                return Parse<Noexcept>(rewind, context, Expression.Parse);
            }
            if (context.Parser.VerifyString("dt"))
            {
                return Parse<Member>(rewind, context, Expression.Parse, UnresolvedName.Parse);
            }
            if (context.Parser.VerifyString("pt"))
            {
                return Parse<DeferMember>(rewind, context, Expression.Parse, UnresolvedName.Parse);
            }
            if (context.Parser.VerifyString("ds"))
            {
                return Parse<PointerToMember>(rewind, context, Expression.Parse, Expression.Parse);
            }
            if (context.Parser.VerifyString("sZ"))
            {
                IParsingResult param = TemplateParam.Parse(context);

                if (param != null)
                {
                    return new SizeOfTemplatepack(param);
                }
                param = FunctionParam.Parse(context);
                if (param != null)
                {
                    return new SizeOfFunctionPack(param);
                }
                context.Rewind(rewind);
                return null;
            }
            if (context.Parser.VerifyString("sP"))
            {
                return ParseWithEnd<SizeofCapturedTemplatePack>(rewind, context, ZeroOrMore(TemplateArg.Parse));
            }
            if (context.Parser.VerifyString("sP"))
            {
                return Parse<PackExpansion>(rewind, context, Expression.Parse);
            }
            if (context.Parser.VerifyString("tw"))
            {
                return Parse<Throw>(rewind, context, Expression.Parse);
            }
            if (context.Parser.VerifyString("tr"))
            {
                return new Retrow();
            }
            if (context.Parser.VerifyString("gs"))
            {
                return CanBeGlobal(rewind, context, true);
            }

            IParsingResult result = CanBeGlobal(rewind, context, false) ?? TemplateParam.Parse(context) ?? FunctionParam.Parse(context)
                ?? UnresolvedName.Parse(context) ?? ExprPrimary.Parse(context);

            if (result != null)
            {
                return result;
            }

            IParsingResult operatorName = OperatorName.Parse(context);

            if (operatorName != null)
            {
                IParsingResult first = Expression.Parse(context);

                if (first != null)
                {
                    IParsingResult second = Expression.Parse(context);

                    if (second != null)
                    {
                        IParsingResult third = Expression.Parse(context);

                        if (third != null)
                        {
                            return new Ternary(operatorName, first, second, third);
                        }
                        else
                        {
                            return new Binary(operatorName, first, second);
                        }
                    }
                    else
                    {
                        return new Unary(operatorName, first);
                    }
                }
                context.Rewind(rewind);
                return null;
            }
            return null;
        }

        private static IParsingResult CanBeGlobal(RewindState rewind, ParsingContext context, bool isGlobal)
        {
            if (context.Parser.VerifyString("nw"))
            {
                List<IParsingResult> expressions = CxxDemangler.ParseList(Expression.Parse, context);

                if (context.Parser.VerifyString("_"))
                {
                    IParsingResult type = Type.Parse(context);

                    if (type != null)
                    {
                        if (context.Parser.VerifyString("E"))
                        {
                            if (isGlobal)
                            {
                                return new GlobalNew(expressions, type);
                            }
                            else
                            {
                                return new New(expressions, type);
                            }
                        }
                        else
                        {
                            IParsingResult initializer = Initializer.Parse(context);

                            if (initializer != null)
                            {
                                if (isGlobal)
                                {
                                    return new GlobalNew(expressions, type, initializer);
                                }
                                else
                                {
                                    return new New(expressions, type, initializer);
                                }
                            }
                        }
                    }
                }
            }
            else if (context.Parser.VerifyString("na"))
            {
                List<IParsingResult> expressions = CxxDemangler.ParseList(Expression.Parse, context);

                if (context.Parser.VerifyString("_"))
                {
                    IParsingResult type = Type.Parse(context);

                    if (type != null)
                    {
                        if (context.Parser.VerifyString("E"))
                        {
                            if (isGlobal)
                            {
                                return new GlobalNewArray(expressions, type);
                            }
                            else
                            {
                                return new NewArray(expressions, type);
                            }
                        }
                        else
                        {
                            IParsingResult initializer = Initializer.Parse(context);

                            if (initializer != null)
                            {
                                if (isGlobal)
                                {
                                    return new GlobalNewArray(expressions, type, initializer);
                                }
                                else
                                {
                                    return new NewArray(expressions, type, initializer);
                                }
                            }
                        }
                    }
                }
            }
            else if (context.Parser.VerifyString("dl"))
            {
                IParsingResult expression = Expression.Parse(context);

                if (expression != null)
                {
                    if (isGlobal)
                    {
                        return new GlobalDelete(expression);
                    }
                    else
                    {
                        return new Delete(expression);
                    }
                }
            }
            else if (context.Parser.VerifyString("da"))
            {
                IParsingResult expression = Expression.Parse(context);

                if (expression != null)
                {
                    if (isGlobal)
                    {
                        return new GlobalDeleteArray(expression);
                    }
                    else
                    {
                        return new DeleteArray(expression);
                    }
                }
            }
            context.Rewind(rewind);
            return null;
        }

        private static Func<ParsingContext, List<IParsingResult>> ZeroOrMore(CxxDemangler.ParsingFunction parser)
        {
            return (ParsingContext context) =>
            {
                return CxxDemangler.ParseList(parser, context);
            };
        }

        private static IParsingResult Parse<T>(RewindState rewind, ParsingContext context, params Func<ParsingContext, object>[] paramsParse)
            where T : IParsingResult
        {
            object[] paramsValue = new IParsingResult[paramsParse.Length];

            for (int i = 0; i < paramsParse.Length; i++)
            {
                paramsValue[i] = paramsParse[i](context);
                if (paramsValue[i] == null)
                {
                    context.Rewind(rewind);
                    return null;
                }
            }
            return (T)Activator.CreateInstance(typeof(T), paramsValue);
        }

        private static IParsingResult ParseWithEnd<T>(RewindState rewind, ParsingContext context, params Func<ParsingContext, object>[] paramsParse)
            where T : IParsingResult
        {
            IParsingResult result = Parse<T>(rewind, context, paramsParse);

            if (result != null && context.Parser.VerifyString("E"))
            {
                return result;
            }
            context.Rewind(rewind);
            return null;
        }

        internal class PrefixDec : IParsingResult
        {
            private IParsingResult expression;

            public PrefixDec(IParsingResult expression)
            {
                this.expression = expression;
            }
        }

        internal class TypeIdExpression : IParsingResult
        {
            private IParsingResult expression;

            public TypeIdExpression(IParsingResult expression)
            {
                this.expression = expression;
            }
        }

        internal class TypeIdType : IParsingResult
        {
            private IParsingResult type;

            public TypeIdType(IParsingResult type)
            {
                this.type = type;
            }
        }

        internal class SizeOfType : IParsingResult
        {
            private IParsingResult type;

            public SizeOfType(IParsingResult type)
            {
                this.type = type;
            }
        }

        internal class SizeOfExpression : IParsingResult
        {
            private IParsingResult expression;

            public SizeOfExpression(IParsingResult expression)
            {
                this.expression = expression;
            }
        }

        internal class AlignOfType : IParsingResult
        {
            private IParsingResult type;

            public AlignOfType(IParsingResult type)
            {
                this.type = type;
            }
        }

        internal class AlignOfExpression : IParsingResult
        {
            private IParsingResult expression;

            public AlignOfExpression(IParsingResult expression)
            {
                this.expression = expression;
            }
        }

        internal class Noexcept : IParsingResult
        {
            private IParsingResult expression;

            public Noexcept(IParsingResult expression)
            {
                this.expression = expression;
            }
        }

        internal class PrefixInc : IParsingResult
        {
            private IParsingResult expression;

            public PrefixInc(IParsingResult expression)
            {
                this.expression = expression;
            }
        }

        internal class BracedInitList : IParsingResult
        {
            private IParsingResult expression;

            public BracedInitList(IParsingResult expression)
            {
                this.expression = expression;
            }
        }

        internal class Throw : IParsingResult
        {
            private IParsingResult expression;

            public Throw(IParsingResult expression)
            {
                this.expression = expression;
            }
        }

        internal class PackExpansion : IParsingResult
        {
            private IParsingResult expression;

            public PackExpansion(IParsingResult expression)
            {
                this.expression = expression;
            }
        }

        internal class Member : IParsingResult
        {
            private IParsingResult expression;
            private IParsingResult name;

            public Member(IParsingResult expression, IParsingResult name)
            {
                this.expression = expression;
                this.name = name;
            }
        }

        internal class DeferMember : IParsingResult
        {
            private IParsingResult expression;
            private IParsingResult name;

            public DeferMember(IParsingResult expression, IParsingResult name)
            {
                this.expression = expression;
                this.name = name;
            }
        }

        internal class PointerToMember : IParsingResult
        {
            private IParsingResult expression;
            private IParsingResult expression2;

            public PointerToMember(IParsingResult expression, IParsingResult expression2)
            {
                this.expression = expression;
                this.expression2 = expression2;
            }
        }

        internal class SizeofCapturedTemplatePack : IParsingResult
        {
            private List<IParsingResult> arguments;

            public SizeofCapturedTemplatePack(List<IParsingResult> arguments)
            {
                this.arguments = arguments;
            }
        }

        internal class Call : IParsingResult
        {
            private IParsingResult expression;
            private List<IParsingResult> arguments;

            public Call(IParsingResult expression, List<IParsingResult> arguments)
            {
                this.expression = expression;
                this.arguments = arguments;
            }
        }

        internal class ConversionMany : IParsingResult
        {
            private List<IParsingResult> expressions;
            private IParsingResult type;

            public ConversionMany(IParsingResult type, List<IParsingResult> expressions)
            {
                this.type = type;
                this.expressions = expressions;
            }
        }

        internal class ConversionBraced : IParsingResult
        {
            private List<IParsingResult> expressions;
            private IParsingResult type;

            public ConversionBraced(IParsingResult type, List<IParsingResult> expressions)
            {
                this.type = type;
                this.expressions = expressions;
            }
        }

        internal class ConversionOne : IParsingResult
        {
            private IParsingResult expression;
            private IParsingResult type;

            public ConversionOne(IParsingResult type, IParsingResult expression)
            {
                this.type = type;
                this.expression = expression;
            }
        }

        internal class DynamicCast : IParsingResult
        {
            private IParsingResult expression;
            private IParsingResult type;

            public DynamicCast(IParsingResult type, IParsingResult expression)
            {
                this.type = type;
                this.expression = expression;
            }
        }

        internal class StaticCast : IParsingResult
        {
            private IParsingResult expression;
            private IParsingResult type;

            public StaticCast(IParsingResult type, IParsingResult expression)
            {
                this.type = type;
                this.expression = expression;
            }
        }

        internal class ConstCast : IParsingResult
        {
            private IParsingResult expression;
            private IParsingResult type;

            public ConstCast(IParsingResult type, IParsingResult expression)
            {
                this.type = type;
                this.expression = expression;
            }
        }

        internal class ReinterpretCast : IParsingResult
        {
            private IParsingResult expression;
            private IParsingResult type;

            public ReinterpretCast(IParsingResult type, IParsingResult expression)
            {
                this.type = type;
                this.expression = expression;
            }
        }

        internal class SizeOfTemplatepack : IParsingResult
        {
            private IParsingResult param;

            public SizeOfTemplatepack(IParsingResult param)
            {
                this.param = param;
            }
        }

        internal class SizeOfFunctionPack : IParsingResult
        {
            private IParsingResult param;

            public SizeOfFunctionPack(IParsingResult param)
            {
                this.param = param;
            }
        }

        internal class Retrow : IParsingResult
        {
        }

        internal class GlobalNew : IParsingResult
        {
            private List<IParsingResult> expressions;
            private IParsingResult initializer;
            private IParsingResult type;

            public GlobalNew(List<IParsingResult> expressions, IParsingResult type, IParsingResult initializer = null)
            {
                this.expressions = expressions;
                this.type = type;
                this.initializer = initializer;
            }
        }

        internal class New : IParsingResult
        {
            private List<IParsingResult> expressions;
            private IParsingResult initializer;
            private IParsingResult type;

            public New(List<IParsingResult> expressions, IParsingResult type, IParsingResult initializer = null)
            {
                this.expressions = expressions;
                this.type = type;
                this.initializer = initializer;
            }
        }

        internal class GlobalNewArray : IParsingResult
        {
            private List<IParsingResult> expressions;
            private IParsingResult initializer;
            private IParsingResult type;

            public GlobalNewArray(List<IParsingResult> expressions, IParsingResult type, IParsingResult initializer = null)
            {
                this.expressions = expressions;
                this.type = type;
                this.initializer = initializer;
            }
        }

        internal class NewArray : IParsingResult
        {
            private List<IParsingResult> expressions;
            private IParsingResult initializer;
            private IParsingResult type;

            public NewArray(List<IParsingResult> expressions, IParsingResult type, IParsingResult initializer = null)
            {
                this.expressions = expressions;
                this.type = type;
                this.initializer = initializer;
            }
        }

        internal class Delete : IParsingResult
        {
            private IParsingResult expression;

            public Delete(IParsingResult expression)
            {
                this.expression = expression;
            }
        }

        internal class GlobalDelete : IParsingResult
        {
            private IParsingResult expression;

            public GlobalDelete(IParsingResult expression)
            {
                this.expression = expression;
            }
        }

        internal class GlobalDeleteArray : IParsingResult
        {
            private IParsingResult expression;

            public GlobalDeleteArray(IParsingResult expression)
            {
                this.expression = expression;
            }
        }

        internal class DeleteArray : IParsingResult
        {
            private IParsingResult expression;

            public DeleteArray(IParsingResult expression)
            {
                this.expression = expression;
            }
        }

        internal class Unary : IParsingResult
        {
            private IParsingResult first;
            private IParsingResult operatorName;

            public Unary(IParsingResult operatorName, IParsingResult first)
            {
                this.operatorName = operatorName;
                this.first = first;
            }
        }

        internal class Binary : IParsingResult
        {
            private IParsingResult first;
            private IParsingResult operatorName;
            private IParsingResult second;

            public Binary(IParsingResult operatorName, IParsingResult first, IParsingResult second)
            {
                this.operatorName = operatorName;
                this.first = first;
                this.second = second;
            }
        }

        internal class Ternary : IParsingResult
        {
            private IParsingResult first;
            private IParsingResult operatorName;
            private IParsingResult second;
            private IParsingResult third;

            public Ternary(IParsingResult operatorName, IParsingResult first, IParsingResult second, IParsingResult third)
            {
                this.operatorName = operatorName;
                this.first = first;
                this.second = second;
                this.third = third;
            }
        }
    }
}
