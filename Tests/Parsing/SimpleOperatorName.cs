using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class SimpleOperatorName : TestBase
    {
        [TestMethod]
        public void SimpleOperatorNameNoTrailing()
        {
            Verify("qu",
                new Parsers.SimpleOperatorName(Parsers.SimpleOperatorName.Values.Question),
                endsWith: "");
        }

        [TestMethod]
        public void SimpleOperatorNameTrailing()
        {
            Verify("quokka",
                new Parsers.SimpleOperatorName(Parsers.SimpleOperatorName.Values.Question),
                endsWith: "okka");
        }

        [TestMethod]
        public void SimpleOperatorNameFailures()
        {
            Assert.IsNull(Parse("bu-buuuu"));
            Assert.IsNull(Parse("q"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.SimpleOperatorName.Parse(context);
        }
    }
}
