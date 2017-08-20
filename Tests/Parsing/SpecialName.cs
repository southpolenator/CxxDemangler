using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class SpecialName : TestBase
    {
        [TestMethod]
        public void SpecialNameVirtualTable()
        {
            Verify("TVi...",
                new Parsers.SpecialName.VirtualTable(
                    new Parsers.StandardBuiltinType(Parsers.StandardBuiltinType.Values.Int)));
        }

        [TestMethod]
        public void SpecialNameVtt()
        {
            Verify("TTi...",
                new Parsers.SpecialName.Vtt(
                    new Parsers.StandardBuiltinType(Parsers.StandardBuiltinType.Values.Int)));
        }

        [TestMethod]
        public void SpecialNameTypeinfo()
        {
            Verify("TIi...",
                new Parsers.SpecialName.TypeInfo(
                    new Parsers.StandardBuiltinType(Parsers.StandardBuiltinType.Values.Int)));
        }

        [TestMethod]
        public void SpecialNameTypeinfoName()
        {
            Verify("TSi...",
                new Parsers.SpecialName.TypeInfoName(
                    new Parsers.StandardBuiltinType(Parsers.StandardBuiltinType.Values.Int)));
        }

        [TestMethod]
        public void SpecialNameVirtualOverrideThunk()
        {
            Verify("Tv42_36_3abc...",
                new Parsers.SpecialName.VirtualOverrideThunk(
                    new Parsers.CallOffset.Virtual(42, 36),
                    new Parsers.SourceName.Identifier("abc")));
        }

        [TestMethod]
        public void SpecialNameVirtualOverrideThunkCovariant()
        {
            Verify("Tcv42_36_v42_36_3abc...",
                new Parsers.SpecialName.VirtualOverrideThunkCovariant(
                    new Parsers.CallOffset.Virtual(42, 36),
                    new Parsers.CallOffset.Virtual(42, 36),
                    new Parsers.SourceName.Identifier("abc")));
        }

        [TestMethod]
        public void SpecialNameGuard()
        {
            Verify("GV3abc...",
                new Parsers.SpecialName.Guard(
                    new Parsers.SourceName.Identifier("abc")));
        }

        [TestMethod]
        public void SpecialNameGuardTemporary()
        {
            Verify("GR3abc_...",
                new Parsers.SpecialName.GuardTemporary(
                    new Parsers.SourceName.Identifier("abc"),
                    index: 0));
        }

        [TestMethod]
        public void SpecialNameGuardTemporary2()
        {
            Verify("GR3abc0_...",
                new Parsers.SpecialName.GuardTemporary(
                    new Parsers.SourceName.Identifier("abc"),
                    index: 1));
        }

        [TestMethod]
        public void SpecialNameFailures()
        {
            Assert.IsNull(Parse("TZ"));
            Assert.IsNull(Parse("GZ"));
            Assert.IsNull(Parse("GR3abcz"));
            Assert.IsNull(Parse("GR3abc0z"));
            Assert.IsNull(Parse("T"));
            Assert.IsNull(Parse("G"));
            Assert.IsNull(Parse(""));
            Assert.IsNull(Parse("GR3abc"));
            Assert.IsNull(Parse("GR3abc0"));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.SpecialName.Parse(context);
        }
    }
}
