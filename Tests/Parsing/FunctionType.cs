using CxxDemangler.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests.Parsing
{
    [TestClass]
    public class FunctionType : TestBase
    {
        [TestMethod]
        public void FunctionType1()
        {
            Verify("KDxFYS_RE...",
                new Parsers.FunctionType(
                    new BareFunctionType(
                        new[]
                        {
                            new Substitution(0)
                        }),
                    new CvQualifiers(@const: true),
                    new RefQualifier(RefQualifier.Values.LValueRef),
                    transactionSafe: true,
                    externC: true));
        }

        [TestMethod]
        public void FunctionType2()
        {
            Verify("DxFYS_RE...",
                new Parsers.FunctionType(
                    new BareFunctionType(
                        new[]
                        {
                            new Substitution(0)
                        }),
                    null,
                    new RefQualifier(RefQualifier.Values.LValueRef),
                    transactionSafe: true,
                    externC: true));
        }

        [TestMethod]
        public void FunctionType3()
        {
            Verify("FYS_RE...",
                new Parsers.FunctionType(
                    new BareFunctionType(
                        new[]
                        {
                            new Substitution(0)
                        }),
                    null,
                    new RefQualifier(RefQualifier.Values.LValueRef),
                    transactionSafe: false,
                    externC: true));
        }

        [TestMethod]
        public void FunctionType4()
        {
            Verify("FS_RE...",
                new Parsers.FunctionType(
                    new BareFunctionType(
                        new[]
                        {
                            new Substitution(0)
                        }),
                    null,
                    new RefQualifier(RefQualifier.Values.LValueRef),
                    transactionSafe: false,
                    externC: false));
        }

        [TestMethod]
        public void FunctionType5()
        {
            Verify("FS_E...",
                new Parsers.FunctionType(
                    new BareFunctionType(
                        new[]
                        {
                            new Substitution(0)
                        }),
                    cvQualifiers: null,
                    refQualifier: null,
                    transactionSafe: false,
                    externC: false));
        }

        [TestMethod]
        public void MangledNameFailures()
        {
            Assert.IsNull(Parse("DFYS_E"));
            Assert.IsNull(Parse("KKFS_E"));
            Assert.IsNull(Parse("FYS_..."));
            Assert.IsNull(Parse("FYS_"));
            Assert.IsNull(Parse("F"));
            Assert.IsNull(Parse(""));
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.FunctionType.Parse(context);
        }
    }
}
