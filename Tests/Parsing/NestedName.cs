using CxxDemangler.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class NestedName : TestBase
    {
        [TestMethod]
        public void NestedNameUnqualified()
        {
            Verify("NKOS_3abcE...",
                new Parsers.NestedName(
                    new Parsers.Prefix.NestedName(
                        new Substitution(0),
                        new SourceName.Identifier("abc")),
                    new CvQualifiers(@const: true),
                    new RefQualifier(RefQualifier.Values.RValueRef)));
        }

        [TestMethod]
        public void NestedNameUnqualified2()
        {
            Verify("NOS_3abcE...",
                new Parsers.NestedName(
                    new Parsers.Prefix.NestedName(
                        new Substitution(0),
                        new SourceName.Identifier("abc")),
                    null,
                    new RefQualifier(RefQualifier.Values.RValueRef)));
        }

        [TestMethod]
        public void NestedNameUnqualified3()
        {
            Verify("NS_3abcE...",
                new Parsers.NestedName(
                    new Parsers.Prefix.NestedName(
                        new Substitution(0),
                        new SourceName.Identifier("abc")),
                    null,
                    null));
        }

        [TestMethod]
        public void NestedNameTemplate()
        {
            Verify("NKOS_3abcIJEEE...",
                new Parsers.NestedName(
                    new Parsers.Prefix.Template(
                        new Parsers.Prefix.NestedName(
                            new Substitution(0),
                            new SourceName.Identifier("abc")),
                        new TemplateArgs(
                            new IParsingResult[]
                            {
                                new TemplateArg.ArgPack(
                                    new IParsingResult[]
                                    {
                                    }),
                            })),
                    new CvQualifiers(@const: true),
                    new RefQualifier(RefQualifier.Values.RValueRef)));
        }

        [TestMethod]
        public void NestedNameTemplate2()
        {
            Verify("NOS_3abcIJEEE...",
                new Parsers.NestedName(
                    new Parsers.Prefix.Template(
                        new Parsers.Prefix.NestedName(
                            new Substitution(0),
                            new SourceName.Identifier("abc")),
                        new TemplateArgs(
                            new IParsingResult[]
                            {
                                new TemplateArg.ArgPack(
                                    new IParsingResult[]
                                    {
                                    }),
                            })),
                    null,
                    new RefQualifier(RefQualifier.Values.RValueRef)));
        }

        [TestMethod]
        public void NestedNameTemplate3()
        {
            Verify("NS_3abcIJEEE...",
                new Parsers.NestedName(
                    new Parsers.Prefix.Template(
                        new Parsers.Prefix.NestedName(
                            new Substitution(0),
                            new SourceName.Identifier("abc")),
                        new TemplateArgs(
                            new IParsingResult[]
                            {
                                new TemplateArg.ArgPack(
                                    new IParsingResult[]
                                    {
                                    }),
                            })),
                    null,
                    null));
        }

        [TestMethod]
        public void NestedNameFailures()
        {
            //Assert.IsNull(Parse("NS_E..."));
            //Assert.IsNull(Parse("NS_DttrEE..."));
            Assert.IsNull(Parse("zzz"));
            Assert.IsNull(Parse("Nzzz"));
            Assert.IsNull(Parse("NKzzz"));
            Assert.IsNull(Parse("NKOzzz"));
            Assert.IsNull(Parse("NKO3abczzz"));
            Assert.IsNull(Parse("NKO3abc3abczzz"));
            Assert.IsNull(Parse(""));
            Assert.IsNull(Parse("N"));
            Assert.IsNull(Parse("NK"));
            Assert.IsNull(Parse("NKO"));
            Assert.IsNull(Parse("NKO3abc"));
            Assert.IsNull(Parse("NKO3abc3abc"));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.NestedName.Parse(context);
        }
    }
}
