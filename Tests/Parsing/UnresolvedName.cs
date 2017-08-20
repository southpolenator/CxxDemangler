using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class UnresolvedName : TestBase
    {
        [TestMethod]
        public void UnresolvedNameGlobal()
        {
            Verify("gs3abc...",
                new Parsers.UnresolvedName.Global(
                    new Parsers.SimpleId(
                        new Parsers.SourceName.Identifier("abc"),
                        arguments: null)));
        }

        [TestMethod]
        public void UnresolvedNameName()
        {
            Verify("3abc...",
                new Parsers.SimpleId(
                    new Parsers.SourceName.Identifier("abc"),
                    arguments: null));
        }

        [TestMethod]
        public void UnresolvedNameNested1()
        {
            Verify("srS_3abc...",
                new Parsers.UnresolvedName.Nested1(
                    new Parsers.Substitution(0),
                    new IParsingResult[0],
                    new Parsers.SimpleId(
                        new Parsers.SourceName.Identifier("abc"),
                        arguments: null)));
        }

        [TestMethod]
        public void UnresolvedNameNested1Long()
        {
            Verify("srNS_3abc3abcE3abc...",
                new Parsers.UnresolvedName.Nested1(
                    new Parsers.Substitution(0),
                    new IParsingResult[]
                    {
                        new Parsers.SimpleId(
                            new Parsers.SourceName.Identifier("abc"),
                            arguments: null),
                        new Parsers.SimpleId(
                            new Parsers.SourceName.Identifier("abc"),
                            arguments: null),
                    },
                    new Parsers.SimpleId(
                        new Parsers.SourceName.Identifier("abc"),
                        arguments: null)));
        }

        [TestMethod]
        public void UnresolvedNameGlobalNested2()
        {
            Verify("gssr3abcE3abc...",
                new Parsers.UnresolvedName.GlobalNested2(
                    new IParsingResult[]
                    {
                        new Parsers.SimpleId(
                            new Parsers.SourceName.Identifier("abc"),
                            arguments: null),
                    },
                    new Parsers.SimpleId(
                        new Parsers.SourceName.Identifier("abc"),
                        arguments: null)));
        }

        [TestMethod]
        public void UnresolvedNameNested2()
        {
            Verify("sr3abcE3abc...",
                new Parsers.UnresolvedName.Nested2(
                    new IParsingResult[]
                    {
                        new Parsers.SimpleId(
                            new Parsers.SourceName.Identifier("abc"),
                            arguments: null),
                    },
                    new Parsers.SimpleId(
                        new Parsers.SourceName.Identifier("abc"),
                        arguments: null)));
        }

        [TestMethod]
        public void UnresolvedNameFailures()
        {
            Assert.IsNull(Parse("zzzzzz"));
            Assert.IsNull(Parse("gszzz"));
            Assert.IsNull(Parse("gssrzzz"));
            Assert.IsNull(Parse("srNzzz"));
            Assert.IsNull(Parse("srzzz"));
            Assert.IsNull(Parse("srN3abczzzz"));
            Assert.IsNull(Parse("srN3abcE"));
            Assert.IsNull(Parse("srN3abc"));
            Assert.IsNull(Parse("srN"));
            Assert.IsNull(Parse("sr"));
            Assert.IsNull(Parse("gssr"));
            Assert.IsNull(Parse("gs"));
            Assert.IsNull(Parse(""));
        }

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.Expression.Retrow();
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.UnresolvedName.Parse(context);
        }
    }
}
