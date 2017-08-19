using CxxDemangler.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class Encoding : TestBase
    {
        [TestMethod]
        public void EncodingFunction()
        {
            Verify("3fooi...",
                new Parsers.Encoding.Function(
                    new SourceName.Identifier("foo"),
                    new BareFunctionType(
                        new[]
                        {
                            new StandardBuiltinType(StandardBuiltinType.Values.Int),
                        })));
        }

        [TestMethod]
        public void EncodingData()
        {
            Verify("3foo...",
                new SourceName.Identifier("foo"));
        }

        [TestMethod]
        public void EncodingSpecial()
        {
            Verify("GV3abc...",
                new SpecialName.Guard(
                    new SourceName.Identifier("abc")));
        }

        [TestMethod]
        public void EncodingFailures()
        {
            Assert.IsNull(Parse("zzz"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.Encoding.Parse(context);
        }
    }
}
