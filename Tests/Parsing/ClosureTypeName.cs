using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class ClosureTypeName : TestBase
    {
        [TestMethod]
        public void ClosureTypeNameEmpty()
        {
            Verify("UlvE_...",
                new Parsers.ClosureTypeName(
                    new Parsers.LambdaSig(
                        new IParsingResult[0]),
                    number: null));
        }

        [TestMethod]
        public void ClosureTypeNameNonEmpty()
        {
            Verify("UlvE36_...",
                new Parsers.ClosureTypeName(
                    new Parsers.LambdaSig(
                        new IParsingResult[0]),
                    number: 36));
        }

        [TestMethod]
        public void ClosureTypeNameFailures()
        {
            Assert.IsNull(Parse("UlvE36zzz"));
            Assert.IsNull(Parse("UlvEzzz"));
            Assert.IsNull(Parse("Ulvzzz"));
            Assert.IsNull(Parse("zzz"));
            Assert.IsNull(Parse("UlvE10"));
            Assert.IsNull(Parse("UlvE"));
            Assert.IsNull(Parse("Ulv"));
            Assert.IsNull(Parse("Ul"));
            Assert.IsNull(Parse("U"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.ClosureTypeName.Parse(context);
        }
    }
}
