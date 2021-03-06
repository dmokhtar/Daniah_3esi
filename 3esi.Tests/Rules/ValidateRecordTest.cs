﻿using _3esi.Tests.Extensions;
using _3esi.Tests.Rules.Abstraction;
using Esi_BusinessLayer.Abstraction;
using Esi_BusinessLayer.Common;
using Esi_BusinessLayer.Parsing;
using Esi_BusinessLayer.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _3esi.Tests.Rules
{
    [TestClass]
    public class ValidateRecordTest : AbstractValidateTest
    {
        private ValidateRecord validateRecord;
        private const int ActualRecordCount = 2;

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
        public void ValidateGroupLocationUniqness_RemovesDuplicatesFromRecordsList()
        {
            try
            {
                #region Arrange
                List<GroupRecord> groupsList = new List<GroupRecord>{
                    new GroupRecord{
                        LocationX = 6,
                        LocationY = 6,
                        Radius = 5
                    },
                    new GroupRecord{
                        LocationX = 6,
                        LocationY = 6,
                        Radius = 5
                    },
                    new GroupRecord{
                        LocationX = 11,
                        LocationY = 10,
                        Radius = 1
                    }
                };
                validateRecord.GroupsList = groupsList;
                #endregion

                #region Act
                validateRecord.ValidateGroupLocationUniqness();
                #endregion

                #region Assert
                Assert.IsTrue(validateRecord.GroupsList.Count == 2);
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
        public void ValidateWellLocationUniqness_RemovesDuplicatesFromRecordsList()
        {
            try
            {
                #region Arrange
                List<WellRecord> wellsList = new List<WellRecord>{
                    new WellRecord{
                        Name = "SomeName",
                        BottomX= 6,
                        BottomY = 6,
                        TopX= 5
                    },
                    new WellRecord{
                        BottomX= 6,
                        BottomY = 6,
                        TopX= 5
                    }
                };
                validateRecord.WellsList = wellsList;
                #endregion

                #region Act
                validateRecord.ValidateWellLocationUniqness();
                #endregion

                #region Assert
                Assert.IsTrue(validateRecord.WellsList.Count == 1);
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
        public void RemoveGroupsIntersections_ReturnUpdatedList()
        {
            try
            {
                #region Arrange
                int expected = 3;
                int actual = 0;

                validateRecord.GroupsList = IntersectingGroupsList;
                #endregion

                #region Act
                validateRecord.RemoveGroupsIntersections();
                #endregion

                #region Assert
                actual = validateRecord.GroupsList.Count;
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

                GroupRecord group = new GroupRecord();
                group.LocationX = 6;
                group.LocationY = 6;

                WellRecord well = new WellRecord();
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
        public void SetGroupsChildren_UpdateList()
        {
            try
            {
                #region Arrange
                bool expected = true;
                bool actual = false;

                List<GroupRecord> groupsList = new List<GroupRecord>{
                    new GroupRecord{
                        LocationX = 6,
                        LocationY = 6,
                        Radius = 5
                    }
                };
                List<WellRecord> wellsList = new List<WellRecord>{
                    new WellRecord{
                        TopX = 5,
                        TopY = 3
                    }
                };

                validateRecord.WellsList = wellsList;
                validateRecord.GroupsList = groupsList;
                #endregion

                #region Act
                Dictionary<IRecord, List<WellRecord>> groupsDictionary = validateRecord.SetGroupsChildren();
                #endregion

                #region Assert
                Assert.IsNotNull(groupsDictionary);
                Assert.IsFalse(groupsDictionary.FirstOrDefault().Value.IsNullOrEmpty());
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

                List<WellRecord> wellsList = new List<WellRecord> {
                    new WellRecord{
               TopX = 5,
               TopY = 3,
               BottomX = 5,
               BottomY = 3
            }};

                validateRecord.WellsList = wellsList;
                #endregion

                #region Act
                validateRecord.SetWellType();
                #endregion

                #region Assert
                actual = wellsList.FirstOrDefault().WellType;
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

                List<WellRecord> wellsList = new List<WellRecord> {
                    new WellRecord{
               TopX = 8,
               TopY = 9,
               BottomX = 9,
               BottomY = 10
            }};

                validateRecord.WellsList = wellsList;
                #endregion

                #region Act
                validateRecord.SetWellType();
                #endregion

                #region Assert
                actual = wellsList.FirstOrDefault().WellType;
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

                List<WellRecord> wellsList = new List<WellRecord> {
                    new WellRecord{
               TopX = 12,
               TopY = 4,
               BottomX = 18,
               BottomY = 2
            }};

                validateRecord.WellsList = wellsList;
                #endregion

                #region Act
                validateRecord.SetWellType();
                #endregion

                #region Assert
                actual = wellsList.FirstOrDefault().WellType;
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