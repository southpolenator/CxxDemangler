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
        public void BareFunctionTypeFailures()
        {
            Assert.IsNull(Parse("Dtrtz"));
            Assert.IsNull(Parse("DTrtz"));
            Assert.IsNull(Parse("Dz"));
            Assert.IsNull(Parse("Dtrt"));
            Assert.IsNull(Parse("DTrt"));
            Assert.IsNull(Parse("Dt"));
            Assert.IsNull(Parse("DT"));
            Assert.IsNull(Parse("D"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.ClassEnumType.Parse(context);
        }
    }
}
