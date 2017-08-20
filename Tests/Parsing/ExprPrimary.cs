using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class ExprPrimary : TestBase
    {
        [TestMethod]
        public void ExprPrimaryLiteral()
        {
            Verify("LS_12345E...",
                new Parsers.ExprPrimary.Literal(
                    new Parsers.Substitution(0),
                    name: "12345"));
        }

        [TestMethod]
        public void ExprPrimaryLiteral2()
        {
            Verify("LS_E...",
                new Parsers.ExprPrimary.Literal(
                    new Parsers.Substitution(0),
                    name: ""));
        }

        [TestMethod]
        public void ExprPrimaryExternal()
        {
            Verify("L_Z3abcE...",
                new Parsers.ExprPrimary.External(
                    new Parsers.SourceName.Identifier("abc")));
        }

        [TestMethod]
        public void ExprPrimaryFailures()
        {
            Assert.IsNull(Parse("zzz"));
            Assert.IsNull(Parse("LS_zzz"));
            Assert.IsNull(Parse("LS_12345"));
            Assert.IsNull(Parse("LS_"));
            Assert.IsNull(Parse("L"));
            Assert.IsNull(Parse(""));
        }

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.Expression.Retrow();
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.ExprPrimary.Parse(context);
        }
    }
}
