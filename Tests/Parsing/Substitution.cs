using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class Substitution : TestBase
    {
        [TestMethod]
        public void Substitution0()
        {
            Verify("S_...",
                new Parsers.Substitution(0));
        }

        [TestMethod]
        public void Substitution2()
        {
            Verify("S1_...",
                new Parsers.Substitution(2));
        }

        [TestMethod]
        public void SubstitutionStd()
        {
            Verify("St...",
                new Parsers.WellKnownComponent(Parsers.WellKnownComponent.Values.Std));
        }

        [TestMethod]
        public void SubstitutionStdAllocator()
        {
            Verify("Sa...",
                new Parsers.WellKnownComponent(Parsers.WellKnownComponent.Values.StdAllocator));
        }

        [TestMethod]
        public void SubstitutionStdString1()
        {
            Verify("Sb...",
                new Parsers.WellKnownComponent(Parsers.WellKnownComponent.Values.StdString1));
        }

        [TestMethod]
        public void SubstitutionStdString2()
        {
            Verify("Ss...",
                new Parsers.WellKnownComponent(Parsers.WellKnownComponent.Values.StdString2));
        }

        [TestMethod]
        public void SubstitutionStdIstream()
        {
            Verify("Si...",
                new Parsers.WellKnownComponent(Parsers.WellKnownComponent.Values.StdIstream));
        }

        [TestMethod]
        public void SubstitutionStdOstream()
        {
            Verify("So...",
                new Parsers.WellKnownComponent(Parsers.WellKnownComponent.Values.StdOstream));
        }

        [TestMethod]
        public void SubstitutionStdIostream()
        {
            Verify("Sd...",
                new Parsers.WellKnownComponent(Parsers.WellKnownComponent.Values.StdIostream));
        }

        [TestMethod]
        public void SubstitutionFailures()
        {
            Assert.IsNull(Parse("S999_"));
            Assert.IsNull(Parse("Sz"));
            Assert.IsNull(Parse("zzz"));
            Assert.IsNull(Parse("S1"));
            Assert.IsNull(Parse("S"));
            Assert.IsNull(Parse(""));
        }

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.Expression.Retrow();
            yield return new Parsers.Expression.Retrow();
            yield return new Parsers.Expression.Retrow();
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.Substitution.Parse(context);
        }
    }
}
