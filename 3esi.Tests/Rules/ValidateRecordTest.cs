using _3esi.Tests.Extensions;
using _3esi.Tests.Rules.Abstraction;
using Esi_BusinessLayer.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _3esi.Tests.Rules
{
    [TestClass]
    public class ValidateRecordTest : AbstractValidateTest
    {
        private ValidateRecord validateRecord;
        private const int ActualRecordCount = 1;

        [TestInitialize]
        public void Setup()
        {
            validateRecord = new ValidateRecord();
        }

        [TestCleanup]
        public void Cleanup()
        {
            validateRecord.Dispose();
            Assert.IsTrue(validateRecord.Records.IsNullOrEmpty());
        }

        [TestMethod]
        public void ValidateNameUniqness_RemovesDuplicatesFromRecordsList()
        {
            validateRecord.Records = RecordsList;
            validateRecord.ValidateNameUniqness();
            Assert.AreEqual(ActualRecordCount, validateRecord.Records.Count);
        }
    }
}