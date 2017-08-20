using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class UnqualifiedName : TestBase
    {
        [TestMethod]
        public void UnqualifiedNameOperator()
        {
            Verify("qu...",
                new Parsers.SimpleOperatorName(Parsers.SimpleOperatorName.Values.Question));
        }

        [TestMethod]
        public void UnqualifiedNameCtorDtor()
        {
            Verify("C1...",
                new Parsers.CtorDtorName(Parsers.CtorDtorName.Values.CompleteConstructor));
        }

        [TestMethod]
        public void UnqualifiedNameSource()
        {
            Verify("10abcdefghij...",
                new Parsers.SourceName.Identifier("abcdefghij"));
        }

        [TestMethod]
        public void UnqualifiedNameUnnamedType()
        {
            Verify("Ut5_...",
                new Parsers.UnnamedTypeName(5));
        }

        [TestMethod]
        public void UnqualifiedNameFailures()
        {
            Assert.IsNull(Parse("zzz"));
            Assert.IsNull(Parse("C"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.UnqualifiedName.Parse(context);
        }
    }
}
