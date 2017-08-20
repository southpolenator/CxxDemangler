using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class TemplateArg : TestBase
    {
        [TestMethod]
        public void TemplateArgType()
        {
            Verify("S_...",
                new Parsers.Substitution(0));
        }

        [TestMethod]
        public void TemplateArgExpression()
        {
            Verify("XtrE...",
                new Parsers.Expression.Retrow());
        }

        [TestMethod]
        public void TemplateArgSimpleExpression()
        {
            Verify("LS_E...",
                new Parsers.ExprPrimary.Literal(
                    new Parsers.Substitution(0),
                    literal: ""));
        }

        [TestMethod]
        public void TemplateArgArgPack0()
        {
            Verify("JE...",
                new Parsers.TemplateArg.ArgPack(
                    new IParsingResult[]
                    {
                    }));
        }

        [TestMethod]
        public void TemplateArgArgPack4()
        {
            Verify("JS_XtrELS_EJEE...",
                new Parsers.TemplateArg.ArgPack(
                    new IParsingResult[]
                    {
                        new Parsers.Substitution(0),
                        new Parsers.Expression.Retrow(),
                        new Parsers.ExprPrimary.Literal(
                            new Parsers.Substitution(0),
                            literal: ""),
                        new Parsers.TemplateArg.ArgPack(
                            new IParsingResult[]
                            {
                            }),
                    }));
        }

        [TestMethod]
        public void TemplateArgFailures()
        {
            Assert.IsNull(Parse("..."));
            Assert.IsNull(Parse("X..."));
            Assert.IsNull(Parse("J..."));
            Assert.IsNull(Parse("JS_..."));
            Assert.IsNull(Parse("JS_"));
            Assert.IsNull(Parse("J"));
            Assert.IsNull(Parse("X"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.TemplateArg.Parse(context);
        }
    }
}
