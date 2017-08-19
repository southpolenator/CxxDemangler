using CxxDemangler.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests
{
    [TestClass]
    public class UnscopedTemplateName : TestBase
    {
        [TestMethod]
        public void UnscopedTemplateNameUnscopedName()
        {
            Verify("dl...",
                new SimpleOperatorName(SimpleOperatorName.Values.Delete));
        }

        [TestMethod]
        public void UnscopedTemplateNameSubstitution()
        {
            Verify("S_...",
                new Substitution(0));
        }

        [TestMethod]
        public void UnscopedTemplateNameFailures()
        {
            Assert.IsNull(Parse("zzzz"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.UnscopedTemplateName.Parse(context);
        }
    }
}
