using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class RefQualifier : TestBase
    {
        [TestMethod]
        public void RefQualifierLValueRef()
        {
            Verify("R...",
                new Parsers.RefQualifier(Parsers.RefQualifier.Values.LValueRef));
        }

        [TestMethod]
        public void RefQualifierRValueRef()
        {
            Verify("O...",
                new Parsers.RefQualifier(Parsers.RefQualifier.Values.RValueRef));
        }

        [TestMethod]
        public void RefQualifierFailures()
        {
            Assert.IsNull(Parse("..."));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.RefQualifier.Parse(context);
        }
    }
}
