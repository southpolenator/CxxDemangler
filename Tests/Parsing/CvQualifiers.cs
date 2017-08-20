using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class CvQualifiers : TestBase
    {
        [TestMethod]
        public void CvQualifiersEmpty()
        {
            Verify("", null, "");
        }

        [TestMethod]
        public void CvQualifiersEmptyTrailing()
        {
            Verify("...", null);
        }

        [TestMethod]
        public void CvQualifiers1()
        {
            Verify("r...",
                new Parsers.CvQualifiers(
                    restrict: true,
                    @volatile: false,
                    @const: false));
        }

        [TestMethod]
        public void CvQualifiers2()
        {
            Verify("rV...",
                new Parsers.CvQualifiers(
                    restrict: true,
                    @volatile: true,
                    @const: false));
        }

        [TestMethod]
        public void CvQualifiers3()
        {
            Verify("rVK...",
                new Parsers.CvQualifiers(
                    restrict: true,
                    @volatile: true,
                    @const: true));
        }

        [TestMethod]
        public void CvQualifiers4()
        {
            Verify("V...",
                new Parsers.CvQualifiers(
                    restrict: false,
                    @volatile: true,
                    @const: false));
        }

        [TestMethod]
        public void CvQualifiers5()
        {
            Verify("VK...",
                new Parsers.CvQualifiers(
                    restrict: false,
                    @volatile: true,
                    @const: true));
        }

        [TestMethod]
        public void CvQualifiers6()
        {
            Verify("K...",
                new Parsers.CvQualifiers(
                    restrict: false,
                    @volatile: false,
                    @const: true));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.CvQualifiers.Parse(context);
        }
    }
}
