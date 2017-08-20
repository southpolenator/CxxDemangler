using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class CallOffset : TestBase
    {
        [TestMethod]
        public void CallOffsetNonVirtual()
        {
            Verify("hn42_...",
                new Parsers.CallOffset.NonVirtual(-42));
        }

        [TestMethod]
        public void CallOffsetVirtual()
        {
            Verify("vn42_36_...",
                new Parsers.CallOffset.Virtual(-42, 36));
        }

        [TestMethod]
        public void CallOffsetFailures()
        {
            Assert.IsNull(Parse("h1..."));
            Assert.IsNull(Parse("v1_1..."));
            Assert.IsNull(Parse("hh"));
            Assert.IsNull(Parse("vv"));
            Assert.IsNull(Parse("z"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.CallOffset.Parse(context);
        }
    }
}
