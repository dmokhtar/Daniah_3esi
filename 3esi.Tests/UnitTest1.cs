using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Esi_BusinessLayer;
using Esi_BusinessLayer.Parsing;

namespace _3esi.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ReadCSVFile_TestMethod()
        {
            try
            {
                #region Arrange
                string filePath = @"..\..\..\..\Daniah_3esi\3esi.csv";
                CSVParser csvParser = new CSVParser();
                #endregion

                #region Apply
                object[] result = csvParser.ReadWellGroupCSVFile(filePath);
                #endregion

                #region Assert
                Assert.IsNotNull(result);
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
            }
        }
    }
}
