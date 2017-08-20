using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class TemplateArgs : TestBase
    {
        [TestMethod]
        public void TemplateArgs1()
        {
            Verify("IS_E...",
                new Parsers.TemplateArgs(
                    new[]
                    {
                        new Parsers.Substitution(0),
                    }));
        }

        [TestMethod]
        public void TemplateArgs4()
        {
            Verify("IS_S_S_S_E...",
                new Parsers.TemplateArgs(
                    new[]
                    {
                        new Parsers.Substitution(0),
                        new Parsers.Substitution(0),
                        new Parsers.Substitution(0),
                        new Parsers.Substitution(0),
                    }));
        }

        [TestMethod]
        public void TemplateArgsFailures()
        {
            Assert.IsNull(Parse("zzz"));
            Assert.IsNull(Parse("IE"));
            Assert.IsNull(Parse("IS_"));
            Assert.IsNull(Parse("I"));
            Assert.IsNull(Parse(""));
        }

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.Expression.Retrow();
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.TemplateArgs.Parse(context);
        }
    }
}
