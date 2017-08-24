using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CxxDemangler.Tests
{
    [TestClass]
    public class GccDemanglingTests
    {
        [TestMethod]
        public void VirtualTables()
        {
            Verify("_ZTVSt15_Sp_counted_ptrIPiLN9__gnu_cxx12_Lock_policyE2EE", "vtable for std::_Sp_counted_ptr<int*, (__gnu_cxx::_Lock_policy)2>");
            Verify("_ZTVSt16_Sp_counted_baseILN9__gnu_cxx12_Lock_policyE2EE", "vtable for std::_Sp_counted_base<(__gnu_cxx::_Lock_policy)2>");
            Verify("_ZTVSt23_Sp_counted_ptr_inplaceIiSaIiELN9__gnu_cxx12_Lock_policyE2EE", "vtable for std::_Sp_counted_ptr_inplace<int, std::allocator<int>, (__gnu_cxx::_Lock_policy)2>");
        }

        private void Verify(string input, string expectedOutput)
        {
            string actualOutput = CxxDemangler.Demangle(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}
