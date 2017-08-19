using CxxDemangler;
using static CxxDemangler.CxxDemangler;
using CxxDemangler.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class MangledName
    {
        [TestMethod]
        public void ParseMangledName()
        {
            IParsingResult result = Parse("_Z3foo...");
            SourceName.Identifier identifier = result as SourceName.Identifier;

            Assert.IsNotNull(result);
            Assert.IsNotNull(identifier);
            Assert.AreEqual("foo", identifier.Name);
        }

        [TestMethod]
        public void FailureTests()
        {
            Assert.IsNull(Parse("_Y"));
            Assert.IsNull(Parse("_"));
            Assert.IsNull(Parse(""));
        }
    }
}
