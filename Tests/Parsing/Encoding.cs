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
                    new Parsers.SourceName.Identifier("foo"),
                    new Parsers.BareFunctionType(
                        new[]
                        {
                            new Parsers.StandardBuiltinType(Parsers.StandardBuiltinType.Values.Int),
                        })));
        }

        [TestMethod]
        public void EncodingData()
        {
            Verify("3foo...",
                new Parsers.SourceName.Identifier("foo"));
        }

        [TestMethod]
        public void EncodingSpecial()
        {
            Verify("GV3abc...",
                new Parsers.SpecialName.Guard(
                    new Parsers.SourceName.Identifier("abc")));
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
