using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class Expression : TestBase
    {
        [TestMethod]
        public void ExpressionUnary()
        {
            Verify("psLS_1E...",
                new Parsers.Expression.Unary(
                    new Parsers.SimpleOperatorName(Parsers.SimpleOperatorName.Values.UnaryPlus),
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionBinary()
        {
            Verify("rsLS_1ELS_1E...",
                new Parsers.Expression.Binary(
                    new Parsers.SimpleOperatorName(Parsers.SimpleOperatorName.Values.RightShift),
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1"),
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionTernary()
        {
            Verify("quLS_1ELS_2ELS_3E...",
                new Parsers.Expression.Ternary(
                    new Parsers.SimpleOperatorName(Parsers.SimpleOperatorName.Values.Question),
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1"),
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "2"),
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "3")));
        }

        [TestMethod]
        public void ExpressionPrefixInc()
        {
            Verify("pp_LS_1E...",
                new Parsers.Expression.PrefixInc(
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionPrefixDec()
        {
            Verify("mm_LS_1E...",
                new Parsers.Expression.PrefixDec(
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionCall()
        {
            Verify("clLS_1E...",
                new Parsers.Expression.Call(
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1"),
                    new IParsingResult[0]));
        }

        [TestMethod]
        public void ExpressionConversionOne()
        {
            Verify("cvS_LS_1E...",
                new Parsers.Expression.ConversionOne(
                    new Parsers.Substitution(0),
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionConversionMany()
        {
            Verify("cvS__LS_1ELS_1EE...",
                new Parsers.Expression.ConversionMany(
                    new Parsers.Substitution(0),
                    new[]
                    {
                        new Parsers.ExprPrimary.Literal(
                            new Parsers.Substitution(0),
                            name: "1"),
                        new Parsers.ExprPrimary.Literal(
                            new Parsers.Substitution(0),
                            name: "1"),
                    }));
        }

        [TestMethod]
        public void ExpressionConversionBraced()
        {
            Verify("tlS_LS_1ELS_1EE...",
                new Parsers.Expression.ConversionBraced(
                    new Parsers.Substitution(0),
                    new[]
                    {
                        new Parsers.ExprPrimary.Literal(
                            new Parsers.Substitution(0),
                            name: "1"),
                        new Parsers.ExprPrimary.Literal(
                            new Parsers.Substitution(0),
                            name: "1"),
                    }));
        }

        [TestMethod]
        public void ExpressionBracedInitList()
        {
            Verify("ilLS_1EE...",
                new Parsers.Expression.BracedInitList(
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionGlobalNew()
        {
            Verify("gsnwLS_1E_S_E...",
                new Parsers.Expression.GlobalNew(
                    new[]
                    {
                        new Parsers.ExprPrimary.Literal(
                            new Parsers.Substitution(0),
                            name: "1"),
                    },
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void ExpressionNew()
        {
            Verify("nwLS_1E_S_E...",
                new Parsers.Expression.New(
                    new[]
                    {
                        new Parsers.ExprPrimary.Literal(
                            new Parsers.Substitution(0),
                            name: "1"),
                    },
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void ExpressionGlobalNewInitializer()
        {
            Verify("gsnwLS_1E_S_piE...",
                new Parsers.Expression.GlobalNew(
                    new[]
                    {
                        new Parsers.ExprPrimary.Literal(
                            new Parsers.Substitution(0),
                            name: "1"),
                    },
                    new Parsers.Substitution(0),
                    new Parsers.Initializer(
                        new IParsingResult[0])));
        }

        [TestMethod]
        public void ExpressionNewInitializer()
        {
            Verify("nwLS_1E_S_piE...",
                new Parsers.Expression.New(
                    new[]
                    {
                        new Parsers.ExprPrimary.Literal(
                            new Parsers.Substitution(0),
                            name: "1"),
                    },
                    new Parsers.Substitution(0),
                    new Parsers.Initializer(
                        new IParsingResult[0])));
        }

        [TestMethod]
        public void ExpressionGlobalNewArray()
        {
            Verify("gsnaLS_1E_S_E...",
                new Parsers.Expression.GlobalNewArray(
                    new[]
                    {
                        new Parsers.ExprPrimary.Literal(
                            new Parsers.Substitution(0),
                            name: "1"),
                    },
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void ExpressionNewArray()
        {
            Verify("naLS_1E_S_E...",
                new Parsers.Expression.NewArray(
                    new[]
                    {
                        new Parsers.ExprPrimary.Literal(
                            new Parsers.Substitution(0),
                            name: "1"),
                    },
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void ExpressionGlobalNewArrayInitializer()
        {
            Verify("gsnaLS_1E_S_piE...",
                new Parsers.Expression.GlobalNewArray(
                    new[]
                    {
                        new Parsers.ExprPrimary.Literal(
                            new Parsers.Substitution(0),
                            name: "1"),
                    },
                    new Parsers.Substitution(0),
                    new Parsers.Initializer(
                        new IParsingResult[0])));
        }

        [TestMethod]
        public void ExpressionNewArrayInitializer()
        {
            Verify("naLS_1E_S_piE...",
                new Parsers.Expression.NewArray(
                    new[]
                    {
                        new Parsers.ExprPrimary.Literal(
                            new Parsers.Substitution(0),
                            name: "1"),
                    },
                    new Parsers.Substitution(0),
                    new Parsers.Initializer(
                        new IParsingResult[0])));
        }

        [TestMethod]
        public void ExpressionGlobalDelete()
        {
            Verify("gsdlLS_1E...",
                new Parsers.Expression.GlobalDelete(
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionDelete()
        {
            Verify("dlLS_1E...",
                new Parsers.Expression.Delete(
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionGlobalDeleteArray()
        {
            Verify("gsdaLS_1E...",
                new Parsers.Expression.GlobalDeleteArray(
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionDeleteArray()
        {
            Verify("daLS_1E...",
                new Parsers.Expression.DeleteArray(
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionDynamicCast()
        {
            Verify("dcS_LS_1E...",
                new Parsers.Expression.DynamicCast(
                    new Parsers.Substitution(0),
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionStaticCast()
        {
            Verify("scS_LS_1E...",
                new Parsers.Expression.StaticCast(
                    new Parsers.Substitution(0),
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionConstCast()
        {
            Verify("ccS_LS_1E...",
                new Parsers.Expression.ConstCast(
                    new Parsers.Substitution(0),
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionReinterpretCast()
        {
            Verify("rcS_LS_1E...",
                new Parsers.Expression.ReinterpretCast(
                    new Parsers.Substitution(0),
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionTypeidType()
        {
            Verify("tiS_...",
                new Parsers.Expression.TypeIdType(
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void ExpressionTypeidExpr()
        {
            Verify("teLS_1E...",
                new Parsers.Expression.TypeIdExpression(
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionSizeofType()
        {
            Verify("stS_...",
                new Parsers.Expression.SizeOfType(
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void ExpressionSizeofExpr()
        {
            Verify("szLS_1E...",
                new Parsers.Expression.SizeOfExpression(
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionAlignofType()
        {
            Verify("atS_...",
                new Parsers.Expression.AlignOfType(
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void ExpressionAlignofExpr()
        {
            Verify("azLS_1E...",
                new Parsers.Expression.AlignOfExpression(
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionNoexcept()
        {
            Verify("nxLS_1E...",
                new Parsers.Expression.Noexcept(
                    new Parsers.ExprPrimary.Literal(
                        new Parsers.Substitution(0),
                        name: "1")));
        }

        [TestMethod]
        public void ExpressionTemplateParam()
        {
            Verify("T_...",
                new Parsers.TemplateParam(0));
        }

        [TestMethod]
        public void ExpressionFunctionParam()
        {
            Verify("fp_...",
                new Parsers.FunctionParam(
                    cvQualifiers: null,
                    scope: 0,
                    param: null));
        }

        [TestMethod]
        public void ExpressionMember()
        {
            Verify("dtT_3abc...",
                new Parsers.Expression.Member(
                    new Parsers.TemplateParam(0),
                    new Parsers.SimpleId(
                        new Parsers.SourceName.Identifier("abc"),
                        arguments: null)));
        }

        [TestMethod]
        public void ExpressionDerefMember()
        {
            Verify("ptT_3abc...",
                new Parsers.Expression.DeferMember(
                    new Parsers.TemplateParam(0),
                    new Parsers.SimpleId(
                        new Parsers.SourceName.Identifier("abc"),
                        arguments: null)));
        }

        [TestMethod]
        public void ExpressionPointerToMember()
        {
            Verify("dsT_T_...",
                new Parsers.Expression.PointerToMember(
                    new Parsers.TemplateParam(0),
                    new Parsers.TemplateParam(0)));
        }

        [TestMethod]
        public void ExpressionSizeofTemplatePack()
        {
            Verify("sZT_...",
                new Parsers.Expression.SizeOfTemplatepack(
                    new Parsers.TemplateParam(0)));
        }

        [TestMethod]
        public void ExpressionSizeofFunctionPack()
        {
            Verify("sZfp_...",
                new Parsers.Expression.SizeOfFunctionPack(
                    new Parsers.FunctionParam(
                        cvQualifiers: null,
                        scope: 0,
                        param: null)));
        }

        [TestMethod]
        public void ExpressionSizeofCapturedTemplatePack()
        {
            Verify("sPE...",
                new Parsers.Expression.SizeofCapturedTemplatePack(
                    new IParsingResult[0]));
        }

        [TestMethod]
        public void ExpressionPackExpansion()
        {
            Verify("spT_...",
                new Parsers.Expression.PackExpansion(
                    new Parsers.TemplateParam(0)));
        }

        [TestMethod]
        public void ExpressionThrow()
        {
            Verify("twT_...",
                new Parsers.Expression.Throw(
                    new Parsers.TemplateParam(0)));
        }

        [TestMethod]
        public void ExpressionRethrow()
        {
            Verify("tr...",
                new Parsers.Expression.Retrow());
        }

        [TestMethod]
        public void ExpressionUnresolvedName()
        {
            Verify("3abc...",
                new Parsers.SimpleId(
                    new Parsers.SourceName.Identifier("abc"),
                    arguments: null));
        }

        [TestMethod]
        public void ExpressionPrimary()
        {
            Verify("L_Z3abcE...",
                new Parsers.ExprPrimary.External(
                    new Parsers.SourceName.Identifier("abc")));
        }

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.Type.PointerTo(
                new Parsers.StandardBuiltinType(Parsers.StandardBuiltinType.Values.Int));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.Expression.Parse(context);
        }
    }
}
