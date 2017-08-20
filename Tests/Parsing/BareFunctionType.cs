using CxxDemangler.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class BareFunctionType : TestBase
    {
        [TestMethod]
        public void BareFunctionType1()
        {
            Verify("S_S_...",
                new Parsers.BareFunctionType(
                    new[]
                    {
                        new Substitution(0),
                        new Substitution(0),
                    }));
        }

        [TestMethod]
        public void BareFunctionTypeFailures()
        {
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.BareFunctionType.Parse(context);
        }
    }
}
