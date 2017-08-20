using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class Decltype : TestBase
    {
        [TestMethod]
        public void DecltypeExpression()
        {
            Verify("DTtrE...",
                new Parsers.Decltype(
                    new Parsers.Expression.Retrow(),
                    idExpression: false));
        }

        [TestMethod]
        public void DecltypeIdExpression()
        {
            Verify("DttrE...",
                new Parsers.Decltype(
                    new Parsers.Expression.Retrow(),
                    idExpression: true));
        }

        [TestMethod]
        public void DecltypeFailures()
        {
            Assert.IsNull(Parse("Dtrtz"));
            Assert.IsNull(Parse("DTrtz"));
            Assert.IsNull(Parse("Dz"));
            Assert.IsNull(Parse("Dtrt"));
            Assert.IsNull(Parse("DTrt"));
            Assert.IsNull(Parse("Dt"));
            Assert.IsNull(Parse("DT"));
            Assert.IsNull(Parse("D"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.Decltype.Parse(context);
        }
    }
}
