using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class UnresolvedType : TestBase
    {
        [TestMethod]
        public void UnresolvedTypeSubstitution()
        {
            Verify("S_...",
                new Parsers.Substitution(0));
        }

        [TestMethod]
        public void UnresolvedTypeTemplateParam()
        {
            Verify("T_...",
                new Parsers.UnresolvedType.Template(
                    new Parsers.TemplateParam(0),
                    arguments: null));
        }

        [TestMethod]
        public void UnresolvedTypeTemplateParamTemplateArgs()
        {
            Verify("T_IS_E...",
                new Parsers.UnresolvedType.Template(
                    new Parsers.TemplateParam(0),
                    new Parsers.TemplateArgs(
                        new[]
                        {
                            new Parsers.Substitution(0),
                        })));
        }

        [TestMethod]
        public void UnresolvedTypeDecltype()
        {
            Verify("DTtrE...",
                new Parsers.Decltype(
                    new Parsers.Expression.Retrow(),
                    idExpression: false));
        }

        [TestMethod]
        public void UnresolvedTypeFailures()
        {
            Assert.IsNull(Parse("zzzzzzz"));
            Assert.IsNull(Parse(""));
        }

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.Expression.Retrow();
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.UnresolvedType.Parse(context);
        }
    }
}
