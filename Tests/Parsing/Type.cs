using CxxDemangler.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class Type : TestBase
    {
        [TestMethod]
        public void TypeSubstition()
        {
            Verify("S_...",
                new Substitution(0));
        }

        [TestMethod]
        public void TypeBuiltin()
        {
            Verify("c...",
                new StandardBuiltinType(StandardBuiltinType.Values.Char));
        }

        [TestMethod]
        public void TypeFunction()
        {
            Verify("FS_E...",
                new Parsers.FunctionType(
                    new Parsers.BareFunctionType(
                        new[]
                        {
                            new Substitution(0),
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
                new ArrayType.NoDimension(
                    new Substitution(0)));
        }

        [TestMethod]
        public void TypePointerToMember()
        {
            Verify("MS_S_...",
                new PointerToMemberType(
                    new Substitution(0),
                    new Substitution(0)));
        }

        [TestMethod]
        public void TypeTemplateParam()
        {
            Verify("T_...",
                new TemplateParam(0));
        }

        [TestMethod]
        public void TypeTemplateTemplateParam()
        {
            Verify("T_IS_E...",
                new Parsers.Type.TemplateTemplate(
                    new TemplateTemplateParam(
                        new TemplateParam(0)),
                    new TemplateArgs(
                        new[]
                        {
                            new Substitution(0),
                        })));
        }

        [TestMethod]
        public void TypeDecltype()
        {
            Verify("DTtrE...",
                new Decltype(
                    new Expression.Retrow(),
                    idExpression: false));
        }

        [TestMethod]
        public void TypeQualified()
        {
            Verify("KS_...",
                new Parsers.Type.Qualified(
                    new CvQualifiers(@const: true),
                    new Substitution(0)));
        }

        [TestMethod]
        public void TypePointerTo()
        {
            Verify("PS_...",
                new Parsers.Type.PointerTo(
                    new Substitution(0)));
        }

        [TestMethod]
        public void TypeLvalueRef()
        {
            Verify("RS_...",
                new Parsers.Type.LvalueRef(
                    new Substitution(0)));
        }

        [TestMethod]
        public void TypeRvalueRef()
        {
            Verify("OS_...",
                new Parsers.Type.RvalueRef(
                    new Substitution(0)));
        }

        [TestMethod]
        public void TypeComplex()
        {
            Verify("CS_...",
                new Parsers.Type.Complex(
                    new Substitution(0)));
        }

        [TestMethod]
        public void TypeImaginary()
        {
            Verify("GS_...",
                new Parsers.Type.Imaginary(
                    new Substitution(0)));
        }

        [TestMethod]
        public void TypeVendorExtension()
        {
            Verify("U3abcIS_ES_...",
                new Parsers.Type.VendorExtension(
                    new SourceName.Identifier("abc"),
                    new TemplateArgs(
                        new[]
                        {
                            new Substitution(0),
                        }),
                    new Substitution(0)));
        }

        [TestMethod]
        public void TypePackExpansion()
        {
            Verify("DpS_...",
                new Parsers.Type.PackExtension(
                    new Substitution(0)));
        }

        [TestMethod]
        public void TypeClassEnum()
        {
            Verify("3abc...",
                new SourceName.Identifier("abc"));
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

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.Type.Parse(context);
        }
    }
}
