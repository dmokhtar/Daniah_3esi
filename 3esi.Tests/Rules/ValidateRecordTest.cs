using _3esi.Tests.Extensions;
using _3esi.Tests.Rules.Abstraction;
using Esi_BusinessLayer.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Esi_BusinessLayer.Common;
using System.Collections.Generic;
using Esi_BusinessLayer.Abstraction;

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
            try
            {
                validateRecord.Records = RecordsList;
                validateRecord.ValidateNameUniqness();
                Assert.AreEqual(ActualRecordCount, validateRecord.Records.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
            }
        }

        [TestMethod]
        public void AreGroupsIntersecting_RemoveGroupsIntersections()
        {
            try
            {
                #region Arrange
                int expected = 5;
                int actual = 0;
                #endregion

                #region Act
                List<IRecord> records = validateRecord.AreGroupsIntersecting(IntersectingGroupsList);
                #endregion

                #region Assert
                actual = records.Count;
                Assert.AreEqual(expected, actual);
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

        [TestMethod]
        public void CalculateDistance_ReturnDistanceBetweenTwoPoints()
        {
            try
            {
                #region Arrange
                double expected = 3;
                double actual = 0;

                GroupDetails group = new GroupDetails();
                group.LocationX = 6;
                group.LocationY = 6;

                WellDetails well = new WellDetails();
                well.TopX = 5;
                well.TopY = 3;

                #endregion

                #region Act
                actual = validateRecord.CalculateDistance(group.LocationX, group.LocationY, well.TopX, well.TopY);
                #endregion

                #region Assert
                Assert.IsTrue(actual > expected);
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
        [TestMethod]
        public void IsGroupChild_True()
        {
            try
            {
                #region Arrange
                bool expected = true;
                bool actual = false;

                GroupDetails group = new GroupDetails();
                group.LocationX = 6;
                group.LocationY = 6;
                group.Radius = 5;

                WellDetails well = new WellDetails();
                well.TopX = 5;
                well.TopY = 3;
                #endregion

                #region Act
                validateRecord.IsGroupChild(group, well);
                #endregion

                #region Assert
                actual = group.WellDetailsList.Count > 0;
                Assert.AreEqual(expected, actual);
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

        [TestMethod]
        public void SetWellType_Vertical()
        {
            try
            {
                #region Arrange
                String expected = Esi_BusinessLayer.Common.WellType.Vertical.ToDescription();
                String actual = String.Empty;

                WellDetails well = new WellDetails();
                well.TopX = 5;
                well.TopY = 3;
                well.BottomX = 5;
                well.BottomY = 3;
                #endregion

                #region Act
                validateRecord.SetWellType(well);
                #endregion

                #region Assert
                actual = well.WellType;
                Assert.AreEqual(expected, actual);
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

        [TestMethod]
        public void SetWellType_Slanted()
        {
            try
            {
                #region Arrange
                String expected = Esi_BusinessLayer.Common.WellType.Slanted.ToDescription();
                String actual = String.Empty;

                WellDetails well = new WellDetails();
                well.TopX = 8;
                well.TopY = 9;
                well.BottomX = 9;
                well.BottomY = 10;
                #endregion

                #region Act
                validateRecord.SetWellType(well);
                #endregion

                #region Assert
                actual = well.WellType;
                Assert.AreEqual(expected, actual);
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

        [TestMethod]
        public void SetWellType_Horizontal()
        {
            try
            {
                #region Arrange
                String expected = Esi_BusinessLayer.Common.WellType.Horizontal.ToDescription();
                String actual = String.Empty;

                WellDetails well = new WellDetails();
                well.TopX = 12;
                well.TopY = 4;
                well.BottomX = 18;
                well.BottomY = 2;
                #endregion

                #region Act
                validateRecord.SetWellType(well);
                #endregion

                #region Assert
                actual = well.WellType;
                Assert.AreEqual(expected, actual);
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