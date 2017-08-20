using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class ClassEnumType : TestBase
    {
        [TestMethod]
        public void ClassEnumTypeNamed()
        {
            Verify("3abc...",
                new Parsers.SourceName.Identifier("abc"));
        }

        [TestMethod]
        public void ClassEnumTypeElaboratedStruct()
        {
            Verify("Ts3abc...",
                new Parsers.ClassEnumType.ElaboratedStruct(
                    new Parsers.SourceName.Identifier("abc")));
        }

        [TestMethod]
        public void ClassEnumTypeElaboratedUnion()
        {
            Verify("Tu3abc...",
                new Parsers.ClassEnumType.ElaboratedUnion(
                    new Parsers.SourceName.Identifier("abc")));
        }

        [TestMethod]
        public void ClassEnumTypeElaboratedEnum()
        {
            Verify("Te3abc...",
                new Parsers.ClassEnumType.ElaboratedEnum(
                    new Parsers.SourceName.Identifier("abc")));
        }

        [TestMethod]
        public void ClassEnumTypeFailures()
        {
            Assert.IsNull(Parse("zzz"));
            Assert.IsNull(Parse("Tzzz"));
            Assert.IsNull(Parse("T"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.ClassEnumType.Parse(context);
        }
    }
}
