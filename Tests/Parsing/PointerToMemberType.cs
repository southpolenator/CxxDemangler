using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class PointerToMemberType : TestBase
    {
        [TestMethod]
        public void PointerToMemberType1()
        {
            Verify("MS_S_...",
                new Parsers.PointerToMemberType(
                    new Parsers.Substitution(0),
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void PointerToMemberTypeFailures()
        {
            Assert.IsNull(Parse("MS_S"));
            Assert.IsNull(Parse("MS_"));
            Assert.IsNull(Parse("MS"));
            Assert.IsNull(Parse("M"));
            Assert.IsNull(Parse(""));
            Assert.IsNull(Parse("MS_..."));
            Assert.IsNull(Parse("M..."));
            Assert.IsNull(Parse("..."));
        }

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.Expression.Retrow();
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.PointerToMemberType.Parse(context);
        }
    }
}
