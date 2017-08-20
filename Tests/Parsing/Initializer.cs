using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class Initializer : TestBase
    {
        [TestMethod]
        public void InitializerEmpty()
        {
            Verify("piE...",
                new Parsers.Initializer(
                    new IParsingResult[0]));
        }

        [TestMethod]
        public void InitializerNonEmpty()
        {
            Verify("pitrtrtrE...",
                new Parsers.Initializer(
                    new IParsingResult[]
                    {
                        new Parsers.Expression.Retrow(),
                        new Parsers.Expression.Retrow(),
                        new Parsers.Expression.Retrow(),
                    }));
        }

        [TestMethod]
        public void InitializerFailures()
        {
            Assert.IsNull(Parse("pirtrtrt..."));
            Assert.IsNull(Parse("pi..."));
            Assert.IsNull(Parse("..."));
            Assert.IsNull(Parse("pirt"));
            Assert.IsNull(Parse("pi"));
            Assert.IsNull(Parse("p"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.Initializer.Parse(context);
        }
    }
}
