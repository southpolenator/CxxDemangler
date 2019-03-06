using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class SourceName : TestBase
    {
        [TestMethod]
        public void SourceName1()
        {
            Verify("1abc",
                new Parsers.SourceName.Identifier("a"),
                endsWith: "bc");
        }

        [TestMethod]
        public void SourceName2()
        {
            Verify("10abcdefghijklm",
                new Parsers.SourceName.Identifier("abcdefghij"),
                endsWith: "klm");
        }

        [TestMethod]
        public void SourceName3()
        {
            Verify("3$_0abcdefghijklm",
                new Parsers.SourceName.Identifier("$_0"),
                endsWith: "abcdefghijklm");
        }

        [TestMethod]
        public void SourceNameFailures()
        {
            Assert.IsNull(Parse("0abc"));
            Assert.IsNull(Parse("n1abc"));
            Assert.IsNull(Parse("10abcdef"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.SourceName.Parse(context);
        }
    }
}
