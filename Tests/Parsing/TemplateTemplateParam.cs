using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class TemplateTemplateParam : TestBase
    {
        [TestMethod]
        public void TemplateTemplateParamSubstitution()
        {
            Verify("S_...",
                new Parsers.Substitution(0));
        }

        [TestMethod]
        public void TemplateTemplateParamTemplateParam()
        {
            Verify("T1_...",
                new Parsers.TemplateTemplateParam(
                    new Parsers.TemplateParam(2)));
        }

        [TestMethod]
        public void TemplateTemplateParamFailures()
        {
            Assert.IsNull(Parse("S"));
            Assert.IsNull(Parse("T"));
            Assert.IsNull(Parse(""));
            Assert.IsNull(Parse("S..."));
            Assert.IsNull(Parse("T..."));
            Assert.IsNull(Parse("..."));
        }

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.TemplateTemplateParam(
                new Parsers.TemplateParam(0));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.TemplateTemplateParam.Parse(context);
        }
    }
}
