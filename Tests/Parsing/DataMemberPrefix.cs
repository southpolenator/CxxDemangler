using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class DataMemberPrefix : TestBase
    {
        [TestMethod]
        public void DataMemberPrefixSourceName()
        {
            Verify("3fooM...",
                new Parsers.SourceName.Identifier("foo"));
        }

        [TestMethod]
        public void DataMemberPrefixFailures()
        {
            Assert.IsNull(Parse("zzz"));
            Assert.IsNull(Parse("1"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.DataMemberPrefix.Parse(context);
        }
    }
}
