using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class LocalName : TestBase
    {
        [TestMethod]
        public void LocalNameRelative()
        {
            Verify("Z3abcE3def_0...",
                new Parsers.LocalName.Relative(
                    new Parsers.SourceName.Identifier("abc"),
                    new Parsers.SourceName.Identifier("def"),
                    new Parsers.Discriminator(0)));
        }

        [TestMethod]
        public void LocalNameRelative2()
        {
            Verify("Z3abcE3def...",
                new Parsers.LocalName.Relative(
                    new Parsers.SourceName.Identifier("abc"),
                    new Parsers.SourceName.Identifier("def"),
                    discriminator: null));
        }

        [TestMethod]
        public void LocalNameRelative3()
        {
            Verify("Z3abcEs_0...",
                new Parsers.LocalName.Relative(
                    new Parsers.SourceName.Identifier("abc"),
                    null,
                    new Parsers.Discriminator(0)));
        }

        [TestMethod]
        public void LocalNameRelative4()
        {
            Verify("Z3abcEs...",
                new Parsers.LocalName.Relative(
                    new Parsers.SourceName.Identifier("abc"),
                    null,
                    null));
        }

        [TestMethod]
        public void LocalNameDefault()
        {
            Verify("Z3abcEd1_3abc...",
                new Parsers.LocalName.Default(
                    new Parsers.SourceName.Identifier("abc"),
                    1,
                    new Parsers.SourceName.Identifier("abc")));
        }

        [TestMethod]
        public void LocalNameDefault2()
        {
            Verify("Z3abcEd_3abc...",
                new Parsers.LocalName.Default(
                    new Parsers.SourceName.Identifier("abc"),
                    null,
                    new Parsers.SourceName.Identifier("abc")));
        }

        [TestMethod]
        public void LocalNameFailures()
        {
            Assert.IsNull(Parse("A"));
            Assert.IsNull(Parse("Z1a"));
            Assert.IsNull(Parse("Z1aE"));
            Assert.IsNull(Parse("Z"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.LocalName.Parse(context);
        }
    }
}
