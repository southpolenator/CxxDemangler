using CxxDemangler.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class Name : TestBase
    {
        [TestMethod]
        public void NameNested()
        {
            Verify("NS0_3abcE...",
                new Parsers.NestedName(
                    new Parsers.Prefix.NestedName(
                        new Substitution(1),
                        new SourceName.Identifier("abc")),
                    null,
                    null));
        }

        [TestMethod]
        public void NameUnscoped()
        {
            Verify("3abc...",
                new SourceName.Identifier("abc"));
        }

        [TestMethod]
        public void NameUnscopedTemplate()
        {
            Verify("dlIcE...",
                new Parsers.Name.UnscopedTemplate(
                    new SimpleOperatorName(SimpleOperatorName.Values.Delete),
                    new TemplateArgs(
                        new[]
                        {
                            new StandardBuiltinType(StandardBuiltinType.Values.Char),
                        })));
        }

        [TestMethod]
        public void NameLocal()
        {
            Verify("Z3abcEs...",
                new LocalName.Relative(
                    new SourceName.Identifier("abc"),
                    null,
                    null));
        }

        [TestMethod]
        public void NameFailures()
        {
            Assert.IsNull(Parse("zzz"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.Name.Parse(context);
        }
    }
}
