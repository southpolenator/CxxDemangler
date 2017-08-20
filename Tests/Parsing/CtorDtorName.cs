using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class CtorDtorName : TestBase
    {
        [TestMethod]
        public void CtorDtorNameDeletingDestructor()
        {
            Verify("D0...",
                new Parsers.CtorDtorName(Parsers.CtorDtorName.Values.DeletingDestructor));
        }

        [TestMethod]
        public void CtorDtorNameCompleteConstructor()
        {
            Verify("C101",
                new Parsers.CtorDtorName(Parsers.CtorDtorName.Values.CompleteConstructor),
                endsWith: "01");
        }

        [TestMethod]
        public void CtorDtorNameFailures()
        {
            Assert.IsNull(Parse("gayagaya"));
            Assert.IsNull(Parse("C"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.CtorDtorName.Parse(context);
        }
    }
}
