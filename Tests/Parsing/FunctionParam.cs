using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class FunctionParam : TestBase
    {
        [TestMethod]
        public void FunctionParam1()
        {
            Verify("fpK_...",
                new Parsers.FunctionParam(
                    new Parsers.CvQualifiers(@const: true),
                    scope: 0,
                    param: null));
        }

        [TestMethod]
        public void FunctionParam2()
        {
            Verify("fL1pK_...",
                new Parsers.FunctionParam(
                    new Parsers.CvQualifiers(@const: true),
                    scope: 1,
                    param: null));
        }

        [TestMethod]
        public void FunctionParam3()
        {
            Verify("fpK3_...",
                new Parsers.FunctionParam(
                    new Parsers.CvQualifiers(@const: true),
                    scope: 0,
                    param: 3));
        }

        [TestMethod]
        public void FunctionParam4()
        {
            Verify("fL1pK4_...",
                new Parsers.FunctionParam(
                    new Parsers.CvQualifiers(@const: true),
                    scope: 1,
                    param: 4));
        }

        [TestMethod]
        public void FunctionParamFailures()
        {
            Assert.IsNull(Parse("fz"));
            Assert.IsNull(Parse("fLp_"));
            Assert.IsNull(Parse("fpL_"));
            Assert.IsNull(Parse("fL1pK4z"));
            Assert.IsNull(Parse("fL1pK4"));
            Assert.IsNull(Parse("fL1p"));
            Assert.IsNull(Parse("fL1"));
            Assert.IsNull(Parse("fL"));
            Assert.IsNull(Parse("f"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.FunctionParam.Parse(context);
        }
    }
}
