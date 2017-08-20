using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class LambdaSig : TestBase
    {
        [TestMethod]
        public void LambdaSigEmpty()
        {
            Verify("v...",
                new Parsers.LambdaSig(
                    new IParsingResult[0]));
        }

        [TestMethod]
        public void LambdaSigNonEmpty()
        {
            Verify("S_S_S_...",
                new Parsers.LambdaSig(
                    new IParsingResult[]
                    {
                        new Parsers.Substitution(0),
                        new Parsers.Substitution(0),
                        new Parsers.Substitution(0),
                    }));
        }

        [TestMethod]
        public void LambdaSigFailures()
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

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.Type.PointerTo(
                new Parsers.StandardBuiltinType(Parsers.StandardBuiltinType.Values.Bool));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.LambdaSig.Parse(context);
        }
    }
}
