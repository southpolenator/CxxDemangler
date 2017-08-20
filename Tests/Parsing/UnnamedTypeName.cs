using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class UnnamedTypeName : TestBase
    {
        [TestMethod]
        public void UnnamedTypeName0()
        {
            Verify("Ut_abc",
                new Parsers.UnnamedTypeName(null), "abc");
        }

        [TestMethod]
        public void UnnamedTypeName42Trailing()
        {
            Verify("Ut42_abc",
                new Parsers.UnnamedTypeName(42), "abc");
        }

        [TestMethod]
        public void UnnamedTypeName42()
        {
            Verify("Ut42_",
                new Parsers.UnnamedTypeName(42), "");
        }

        [TestMethod]
        public void UnnamedTypeNameFailures()
        {
            Assert.IsNull(Parse("ut_"));
            Assert.IsNull(Parse("u"));
            Assert.IsNull(Parse("Ut"));
            Assert.IsNull(Parse("Ut._"));
            Assert.IsNull(Parse("Ut42"));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.UnnamedTypeName.Parse(context);
        }
    }
}
