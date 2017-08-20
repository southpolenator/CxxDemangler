using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
                    new Parsers.BareFunctionType(
                        new[]
                        {
                            new Parsers.Substitution(0)
                        }),
                    new Parsers.CvQualifiers(@const: true),
                    new Parsers.RefQualifier(Parsers.RefQualifier.Values.LValueRef),
                    transactionSafe: true,
                    externC: true));
        }

        [TestMethod]
        public void FunctionType2()
        {
            Verify("DxFYS_RE...",
                new Parsers.FunctionType(
                    new Parsers.BareFunctionType(
                        new[]
                        {
                            new Parsers.Substitution(0)
                        }),
                    null,
                    new Parsers.RefQualifier(Parsers.RefQualifier.Values.LValueRef),
                    transactionSafe: true,
                    externC: true));
        }

        [TestMethod]
        public void FunctionType3()
        {
            Verify("FYS_RE...",
                new Parsers.FunctionType(
                    new Parsers.BareFunctionType(
                        new[]
                        {
                            new Parsers.Substitution(0)
                        }),
                    null,
                    new Parsers.RefQualifier(Parsers.RefQualifier.Values.LValueRef),
                    transactionSafe: false,
                    externC: true));
        }

        [TestMethod]
        public void FunctionType4()
        {
            Verify("FS_RE...",
                new Parsers.FunctionType(
                    new Parsers.BareFunctionType(
                        new[]
                        {
                            new Parsers.Substitution(0)
                        }),
                    null,
                    new Parsers.RefQualifier(Parsers.RefQualifier.Values.LValueRef),
                    transactionSafe: false,
                    externC: false));
        }

        [TestMethod]
        public void FunctionType5()
        {
            Verify("FS_E...",
                new Parsers.FunctionType(
                    new Parsers.BareFunctionType(
                        new[]
                        {
                            new Parsers.Substitution(0)
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

        internal override IEnumerable<IParsingResult> SubstitutionTableList()
        {
            yield return new Parsers.StandardBuiltinType(Parsers.StandardBuiltinType.Values.Char);
        }

        internal override IParsingResult Parse(ParsingContext context)
        {
            return Parsers.FunctionType.Parse(context);
        }
    }
}
