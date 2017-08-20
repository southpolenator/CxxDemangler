using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class SimpleId : TestBase
    {
        [TestMethod]
        public void SimpleIdNoArgs()
        {
            Verify("3abc...",
                new Parsers.SimpleId(
                    new Parsers.SourceName.Identifier("abc"),
                    arguments: null));
        }

        [TestMethod]
        public void SimpleIdTemplateArgs()
        {
            Verify("3abcIS_E...",
                new Parsers.SimpleId(
                    new Parsers.SourceName.Identifier("abc"),
                    new Parsers.TemplateArgs(
                        new[]
                        {
                            new Parsers.Substitution(0),
                        })));
        }

        [TestMethod]
        public void SimpleIdLevelFailures()
        {
            Assert.IsNull(Parse("zzz"));
            Assert.IsNull(Parse(""));
        }

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.Expression.Retrow();
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.SimpleId.Parse(context);
        }
    }
}
