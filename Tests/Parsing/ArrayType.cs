using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class ArrayType : TestBase
    {
        [TestMethod]
        public void ArrayTypeDimensionNumber1()
        {
            Verify("A10_S_...",
                new Parsers.ArrayType.DimensionNumber(10,
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void ArrayTypeDimensionNumber2()
        {
            Verify("A10_Sb...",
                new Parsers.ArrayType.DimensionNumber(10,
                    new Parsers.WellKnownComponent(Parsers.WellKnownComponent.Values.StdString1)));
        }

        [TestMethod]
        public void ArrayTypeDimensionExpression()
        {
            Verify("Atr_S_...",
                new Parsers.ArrayType.DimensionExpression(
                    new Parsers.Expression.Retrow(),
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void ArrayTypeNoDimension()
        {
            Verify("A_S_...",
                new Parsers.ArrayType.NoDimension(
                    new Parsers.Substitution(0)));
        }

        [TestMethod]
        public void ArrayTypeFailures()
        {
            Assert.IsNull(Parse("A10_"));
            Assert.IsNull(Parse("A10"));
            Assert.IsNull(Parse("A"));
            Assert.IsNull(Parse(""));
            Assert.IsNull(Parse("A10_..."));
            Assert.IsNull(Parse("A10..."));
            Assert.IsNull(Parse("A..."));
            Assert.IsNull(Parse("..."));
        }

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.Expression.Retrow();
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.ArrayType.Parse(context);
        }
    }
}
