using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class BuiltinType : TestBase
    {
        [TestMethod]
        public void BuiltinType1()
        {
            Verify("c...",
                new Parsers.StandardBuiltinType(Parsers.StandardBuiltinType.Values.Char));
        }

        [TestMethod]
        public void BuiltinType2()
        {
            Verify("c",
                new Parsers.StandardBuiltinType(Parsers.StandardBuiltinType.Values.Char), "");
        }

        [TestMethod]
        public void BuiltinTypeExtension()
        {
            Verify("u3abc...",
                new Parsers.BuiltinType.Extension(
                    new Parsers.SourceName.Identifier("abc")));
        }

        [TestMethod]
        public void BuiltinTypeFailures()
        {
            Assert.IsNull(Parse("."));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.BuiltinType.Parse(context);
        }
    }
}
