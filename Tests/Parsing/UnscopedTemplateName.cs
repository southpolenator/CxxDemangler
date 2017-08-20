using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class UnscopedTemplateName : TestBase
    {
        [TestMethod]
        public void UnscopedTemplateNameUnscopedName()
        {
            Verify("dl...",
                new Parsers.SimpleOperatorName(Parsers.SimpleOperatorName.Values.Delete));
        }

        [TestMethod]
        public void UnscopedTemplateNameSubstitution()
        {
            Verify("S_...",
                new Parsers.Substitution(0));
        }

        [TestMethod]
        public void UnscopedTemplateNameFailures()
        {
            Assert.IsNull(Parse("zzzz"));
            Assert.IsNull(Parse(""));
        }

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.SimpleOperatorName(Parsers.SimpleOperatorName.Values.New);
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.UnscopedTemplateName.Parse(context);
        }
    }
}
