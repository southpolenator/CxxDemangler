using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class OperatorName : TestBase
    {
        [TestMethod]
        public void OperatorNameSimple()
        {
            Verify("qu...",
                new Parsers.SimpleOperatorName(Parsers.SimpleOperatorName.Values.Question));
        }

        [TestMethod]
        public void OperatorNameCast()
        {
            Verify("cvi...",
                new Parsers.OperatorName.Cast(
                    new Parsers.StandardBuiltinType(Parsers.StandardBuiltinType.Values.Int)));
        }

        [TestMethod]
        public void OperatorNameLiteral()
        {
            Verify("li3Foo...",
                new Parsers.OperatorName.Literal(
                    new Parsers.SourceName.Identifier("Foo")));
        }

        [TestMethod]
        public void OperatorNameVendorExtension()
        {
            Verify("v33Foo...",
                new Parsers.OperatorName.VendorExtension(
                    3, new Parsers.SourceName.Identifier("Foo")));
        }

        [TestMethod]
        public void OperatorNameFailures()
        {
            Assert.IsNull(Parse("cv"));
            Assert.IsNull(Parse("li3ab"));
            Assert.IsNull(Parse("li"));
            Assert.IsNull(Parse("v33ab"));
            Assert.IsNull(Parse("v3"));
            Assert.IsNull(Parse("v"));
            Assert.IsNull(Parse(""));
            Assert.IsNull(Parse("q"));
            Assert.IsNull(Parse("c"));
            Assert.IsNull(Parse("l"));
            Assert.IsNull(Parse("zzz"));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.OperatorName.Parse(context);
        }
    }
}
