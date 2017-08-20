using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class Type : TestBase
    {
        [TestMethod]
        public void TypeSubstition()
        {
            Verify("S_...",
                new Parsers.Substitution(0));
        }

        [TestMethod]
        public void TypeBuiltin()
        {
            Verify("c...",
                new Parsers.StandardBuiltinType(Parsers.StandardBuiltinType.Values.Char));
        }

        [TestMethod]
        public void TypeFunction()
        {
            Verify("FS_E...",
                new Parsers.FunctionType(
                    new Parsers.BareFunctionType(
                        new[]
                        {
                            new Parsers.Substitution(0),
                        }),
                    cvQualifiers: null,
                    refQualifier: null,
                    transactionSafe: false,
                    externC: false));
        }

        [TestMethod]
        public void TypeArray()
        {
            Verify("A_S_...",
                new Parsers.ArrayType.NoDimension(
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void TypePointerToMember()
        {
            Verify("MS_S_...",
                new Parsers.PointerToMemberType(
                    new Parsers.Substitution(0),
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void TypeTemplateParam()
        {
            Verify("T_...",
                new Parsers.TemplateParam(0));
        }

        [TestMethod]
        public void TypeTemplateTemplateParam()
        {
            Verify("T_IS_E...",
                new Parsers.Type.TemplateTemplate(
                    new Parsers.TemplateTemplateParam(
                        new Parsers.TemplateParam(0)),
                    new Parsers.TemplateArgs(
                        new[]
                        {
                            new Parsers.Substitution(0),
                        })));
        }

        [TestMethod]
        public void TypeDecltype()
        {
            Verify("DTtrE...",
                new Parsers.Decltype(
                    new Parsers.Expression.Retrow(),
                    idExpression: false));
        }

        [TestMethod]
        public void TypeQualified()
        {
            Verify("KS_...",
                new Parsers.Type.Qualified(
                    new Parsers.CvQualifiers(@const: true),
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void TypePointerTo()
        {
            Verify("PS_...",
                new Parsers.Type.PointerTo(
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void TypeLvalueRef()
        {
            Verify("RS_...",
                new Parsers.Type.LvalueRef(
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void TypeRvalueRef()
        {
            Verify("OS_...",
                new Parsers.Type.RvalueRef(
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void TypeComplex()
        {
            Verify("CS_...",
                new Parsers.Type.Complex(
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void TypeImaginary()
        {
            Verify("GS_...",
                new Parsers.Type.Imaginary(
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void TypeVendorExtension()
        {
            Verify("U3abcIS_ES_...",
                new Parsers.Type.VendorExtension(
                    new Parsers.SourceName.Identifier("abc"),
                    new Parsers.TemplateArgs(
                        new[]
                        {
                            new Parsers.Substitution(0),
                        }),
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void TypePackExpansion()
        {
            Verify("DpS_...",
                new Parsers.Type.PackExtension(
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void TypeClassEnum()
        {
            Verify("3abc...",
                new Parsers.SourceName.Identifier("abc"));
        }

        [TestMethod]
        public void MangledNameFailures()
        {
            Assert.IsNull(Parse("P"));
            Assert.IsNull(Parse("R"));
            Assert.IsNull(Parse("O"));
            Assert.IsNull(Parse("C"));
            Assert.IsNull(Parse("G"));
            Assert.IsNull(Parse("Dp"));
            Assert.IsNull(Parse("D"));
            Assert.IsNull(Parse("P"));
            Assert.IsNull(Parse(""));
        }

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.StandardBuiltinType(Parsers.StandardBuiltinType.Values.Char);
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.Type.Parse(context);
        }
    }
}
