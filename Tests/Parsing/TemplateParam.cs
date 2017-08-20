using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class TemplateParam : TestBase
    {
        [TestMethod]
        public void TemplateParam0()
        {
            Verify("T_...",
                new Parsers.TemplateParam(0));
        }

        [TestMethod]
        public void TemplateParam4()
        {
            Verify("T3_...",
                new Parsers.TemplateParam(4));
        }

        [TestMethod]
        public void TemplateParamFailures()
        {
            Assert.IsNull(Parse("wtf"));
            Assert.IsNull(Parse("Twtf"));
            Assert.IsNull(Parse("T3wtf"));
            Assert.IsNull(Parse("T"));
            Assert.IsNull(Parse("T3"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.TemplateParam.Parse(context);
        }
    }
}
