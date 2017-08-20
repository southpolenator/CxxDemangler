using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class Prefix : TestBase
    {
        [TestMethod]
        public void PrefixUnqualified()
        {
            Verify("3foo...",
                new Parsers.SourceName.Identifier("foo"));
        }

        [TestMethod]
        public void PrefixNested()
        {
            Verify("3abc3def...",
                new Parsers.Prefix.NestedName(
                    new Parsers.SourceName.Identifier("abc"),
                    new Parsers.SourceName.Identifier("def")));
        }

        [TestMethod]
        public void PrefixTemplate()
        {
            Verify("3fooIJEE...",
                new Parsers.Prefix.Template(
                    new Parsers.SourceName.Identifier("foo"),
                    new Parsers.TemplateArgs(
                        new IParsingResult[]
                        {
                            new Parsers.TemplateArg.ArgPack(
                                new IParsingResult[]
                                {
                                }),
                        })));
        }

        [TestMethod]
        public void PrefixTemplateParam()
        {
            Verify("T_...",
                new Parsers.TemplateParam(0));
        }

        [TestMethod]
        public void PrefixDecltype()
        {
            Verify("DTtrE...",
                new Parsers.Decltype(
                    new Parsers.Expression.Retrow(),
                    idExpression: false));
        }

        [TestMethod]
        public void PrefixDataMember()
        {
            Verify("3abc3defM...",
                new Parsers.Prefix.DataMember(
                    new Parsers.SourceName.Identifier("abc"),
                    new Parsers.SourceName.Identifier("def")));
        }

        [TestMethod]
        public void PrefixSubstitution()
        {
            Verify("S_...",
                new Parsers.Substitution(0));
        }

        [TestMethod]
        public void PrefixNestedE()
        {
            Verify("3abc3defE...",
                new Parsers.Prefix.NestedName(
                    new Parsers.SourceName.Identifier("abc"),
                    new Parsers.SourceName.Identifier("def")),
                "E...");
        }

        [TestMethod]
        public void MangledNameFailures()
        {
            Assert.IsNull(Parse("zzz"));
            Assert.IsNull(Parse(""));
        }

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.SimpleOperatorName(Parsers.SimpleOperatorName.Values.New);
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.Prefix.Parse(context);
        }
    }
}
