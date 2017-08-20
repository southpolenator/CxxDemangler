using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
                        new Parsers.Substitution(1),
                        new Parsers.SourceName.Identifier("abc")),
                    null,
                    null));
        }

        [TestMethod]
        public void NameUnscoped()
        {
            Verify("3abc...",
                new Parsers.SourceName.Identifier("abc"));
        }

        [TestMethod]
        public void NameUnscopedTemplate()
        {
            Verify("dlIcE...",
                new Parsers.Name.UnscopedTemplate(
                    new Parsers.SimpleOperatorName(Parsers.SimpleOperatorName.Values.Delete),
                    new Parsers.TemplateArgs(
                        new[]
                        {
                            new Parsers.StandardBuiltinType(Parsers.StandardBuiltinType.Values.Char),
                        })));
        }

        [TestMethod]
        public void NameLocal()
        {
            Verify("Z3abcEs...",
                new Parsers.LocalName.Relative(
                    new Parsers.SourceName.Identifier("abc"),
                    null,
                    null));
        }

        [TestMethod]
        public void NameFailures()
        {
            Assert.IsNull(Parse("zzz"));
            Assert.IsNull(Parse(""));
        }

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.SimpleOperatorName(Parsers.SimpleOperatorName.Values.New);
            yield return new Parsers.SimpleOperatorName(Parsers.SimpleOperatorName.Values.New);
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.Name.Parse(context);
        }
    }
}
