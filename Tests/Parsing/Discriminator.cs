using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class Discriminator : TestBase
    {
        [TestMethod]
        public void Discriminator0()
        {
            Verify("_0...",
                new Parsers.Discriminator(0));
        }

        [TestMethod]
        public void Discriminator9()
        {
            Verify("_9...",
                new Parsers.Discriminator(9));
        }

        [TestMethod]
        public void Discriminator99()
        {
            Verify("__99_...",
                new Parsers.Discriminator(99));
        }

        [TestMethod]
        public void DiscriminatorFailures()
        {
            Assert.IsNull(Parse("_n1"));
            Assert.IsNull(Parse("__99..."));
            Assert.IsNull(Parse("__99"));
            Assert.IsNull(Parse("..."));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.Discriminator.Parse(context);
        }
    }
}
