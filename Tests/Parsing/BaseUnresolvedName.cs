using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class BaseUnresolvedName : TestBase
    {
        [TestMethod]
        public void BaseUnresolvedNameSimpleId()
        {
            Verify("3abc...",
                new Parsers.SimpleId(
                    new Parsers.SourceName.Identifier("abc"),
                    arguments: null));
        }

        [TestMethod]
        public void BaseUnresolvedNameOperator()
        {
            Verify("onnw...",
                new Parsers.BaseUnresolvedName.Operator(
                    new Parsers.SimpleOperatorName(Parsers.SimpleOperatorName.Values.New),
                    arguments: null));
        }

        [TestMethod]
        public void BaseUnresolvedNameOperatorNameWithArgs()
        {
            Verify("onnwIS_E...",
                new Parsers.BaseUnresolvedName.Operator(
                    new Parsers.SimpleOperatorName(Parsers.SimpleOperatorName.Values.New),
                    new Parsers.TemplateArgs(
                        new[]
                        {
                            new Parsers.Substitution(0),
                        })));
        }

        [TestMethod]
        public void BaseUnresolvedNameDestructor()
        {
            Verify("dn3abc...",
                new Parsers.DestructorName(
                    new Parsers.SimpleId(
                        new Parsers.SourceName.Identifier("abc"),
                        arguments: null)));
        }

        [TestMethod]
        public void BaseUnresolvedNameFailures()
        {
            Assert.IsNull(Parse("ozzz"));
            Assert.IsNull(Parse("dzzz"));
            Assert.IsNull(Parse("dn"));
            Assert.IsNull(Parse("on"));
            Assert.IsNull(Parse(""));
        }

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.Expression.Retrow();
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.BaseUnresolvedName.Parse(context);
        }
    }
}
