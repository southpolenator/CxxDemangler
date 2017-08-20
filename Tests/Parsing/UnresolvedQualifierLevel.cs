using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class UnresolvedQualifierLevel : TestBase
    {
        [TestMethod]
        public void UnresolvedQualifierLevelSimpleId()
        {
            Verify("3abc...",
                new Parsers.SimpleId(
                    new Parsers.SourceName.Identifier("abc"),
                    arguments: null));
        }

        [TestMethod]
        public void UnresolvedQualifierLevelSimpleIdTemplateArgs()
        {
            Verify("3abcIS_E...",
                new Parsers.SimpleId(
                    new Parsers.SourceName.Identifier("abc"),
                    new Parsers.TemplateArgs(
                        new[]
                        {
                            new Parsers.Substitution(0),
                        })));
        }

        [TestMethod]
        public void UnresolvedQualifierLevelFailures()
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
            return Parsers.UnresolvedQualifierLevel.Parse(context);
        }
    }
}
