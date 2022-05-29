using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class UnscopedName : TestBase
    {
        [TestMethod]
        public void UnscopedNameStd()
        {
            Verify("St5hello...",
                new Parsers.UnscopedName.Std(
                    new Parsers.SourceName.Identifier("hello")));
        }

        [TestMethod]
        public void UnscopedNameUnqualified()
        {
            Verify("5hello...",
                new Parsers.SourceName.Identifier("hello"));
        }

        [TestMethod]
        public void UnscopedNameFixedLength()
        {
            Verify("L10helloworld...",
                new Parsers.SourceName.Identifier("helloworld"));
        }

        [TestMethod]
        public void UnscopedNameFailures()
        {
            Assert.IsNull(Parse("St..."));
            Assert.IsNull(Parse("..."));
            Assert.IsNull(Parse(""));
            Assert.IsNull(Parse("L..."));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.UnscopedName.Parse(context);
        }
    }
}
