using System.Collections.Generic;
using System.Linq;
using Esi_BusinessLayer.Abstraction;
using Esi_BusinessLayer.Validate.Abstraction;
using System;
using Esi_BusinessLayer.Common;
using Esi_BusinessLayer.Parsing;
using System.Collections;

namespace Esi_BusinessLayer.Rules
{
    public class ValidateRecord : IValidateRecord
    {
        #region Properties
        public List<IRecord> Records { get; set; }
        public List<GroupRecord> GroupsList { get; set; }
        public List<WellRecord> WellsList { get; set; }
        public Dictionary<IRecord, List<WellRecord>> GroupsDictionary { get; set; }
        public List<IRecord> FailedRecords { get; set; }
        #endregion
        
        public ValidateRecord()
        {
            FailedRecords = new List<IRecord>();
            GroupsDictionary = new Dictionary<IRecord, List<WellRecord>>();
        }

        public void ValidateNameUniqness()
        {
            List<IRecord> localRecordsList = Records.GroupBy(record => record.Name).Select(item => item.First()).ToList();
            FailedRecords.AddRange(Records.Except(localRecordsList));
            Records = localRecordsList;
        }

        public void ValidateGroupLocationUniqness()
        {
            if (GroupsList != null && GroupsList.Count > 0)
            {
                List<GroupRecord> localGroupRecords = GroupsList.Distinct(new GroupLocationComparator()).ToList();
                FailedRecords.AddRange(GroupsList.Except(localGroupRecords).ToList());
                GroupsList = localGroupRecords;
            }
        }

        public void ValidateWellLocationUniqness()
        {
            if (WellsList != null && WellsList.Count > 0)
            {
                List<WellRecord> localWellRecords = WellsList.Distinct(new WellLocationComparator()).ToList();
                FailedRecords.AddRange(WellsList.Except(localWellRecords).ToList());
                WellsList = localWellRecords;
            }
        }

        public void Dispose()
        {
            Records?.Clear();
        }

        public Dictionary<IRecord, List<WellRecord>> SetGroupsChildren()
        {
            if (GroupsList != null && GroupsList.Count > 0)
            {
                GroupsList.ForEach(record => GroupsDictionary.Add(record, new List<WellRecord>()));
                AddWellsToGroups();
            }
            return GroupsDictionary;
        }

        private void AddWellsToGroups()
        {
            if (WellsList != null && WellsList.Count > 0)
            {   
                foreach (KeyValuePair<IRecord, List<WellRecord>> group in GroupsDictionary)
                {
                    List<WellRecord> deletedWells = new List<WellRecord>();
                    foreach (var well in WellsList)
                    {
                        if (IsWellGroupChild((GroupRecord)group.Key, (WellRecord)well))
                        {
                            GroupsDictionary[group.Key].Add((WellRecord)well);
                            deletedWells.Add(well);
                        }
                    }

                    WellsList = WellsList.Except(deletedWells).ToList();
                }
            }
        }

        public void GetGroupsWellsLists()
        {
            Records?.ForEach(record =>
            {
                if (record.GetType() == typeof(GroupRecord)) GroupsList.Add((GroupRecord)record);
                else WellsList.Add((WellRecord)record);
            });
        }

        public bool IsWellGroupChild(GroupRecord group, WellRecord well)
        {
            double distance = CalculateDistance(group.LocationX, group.LocationY, well.TopX, well.TopY);

            if (distance <= group.Radius) //well inside group
            {
                return true;
            }

            return false;
        }
        public void SetWellType()
        {
            if (WellsList != null && WellsList.Count > 0)
            {
                foreach (var well in WellsList)
                {
                    double distance = CalculateDistance(well.TopX, well.TopY, well.BottomX, well.BottomY);

                    if (distance >= 0 && distance < 1)
                    {
                        well.WellType = WellType.Vertical.ToDescription();
                    }
                    else if (distance >= 1 && distance < 5)
                    {
                        well.WellType = WellType.Slanted.ToDescription();
                    }
                    else if (distance >= 5)
                    {
                        well.WellType = WellType.Horizontal.ToDescription();
                    }
                }
            }
        }

        public double CalculateDistance(int x1, int y1, int x2, int y2)
        {
            double powerX = Math.Pow(x2 - x1, 2);
            double powerY = Math.Pow(y2 - y1, 2);
            return Math.Sqrt(powerX + powerY);
        }

        public void RemoveGroupsIntersections()
        {
            if (GroupsList != null && GroupsList.Count > 0)
            {
                List<IRecord> intersectingFailedRecords = new List<IRecord>();

                for (int i = 0; i < GroupsList.Count - 1; i++)
                {
                    GroupRecord group1 = GroupsList[i];
                    for (int j = i + 1; j < GroupsList.Count; j++)
                    {
                        GroupRecord group2 = GroupsList[j];
                        if (intersectingFailedRecords.Count == 0 || !intersectingFailedRecords.Contains(group2))
                        {
                            double distance = CalculateDistance(group1.LocationX, group1.LocationY, group2.LocationX,
                                group2.LocationY);

                            if (distance > group1.Radius + group2.Radius)
                            {
                                //not intersecting, leave group
                            }
                            else if ((distance <= group1.Radius + group2.Radius) || distance <
                                                                                 Math.Abs(group1.Radius - group2.Radius)
                                                                                 || (distance == 0 &&
                                                                                     group1.Radius == group2.Radius))
                            {
                                intersectingFailedRecords.Add(group2);
                            }
                            else
                                intersectingFailedRecords.Add(group2);
                        }
                    }
                }

                FailedRecords.AddRange(intersectingFailedRecords);
                List<GroupRecord> groups = intersectingFailedRecords.Cast<GroupRecord>().ToList();
                GroupsList = GroupsList.Except(groups).ToList();
            }
        }

        public void ValidateCsv()
        {
            //Remove groups wells with duplicate names
            ValidateNameUniqness();

            //Get Wells Groups 2 Lists
            GroupsList = new List<GroupRecord>();
            WellsList = new List<WellRecord>();
            GetGroupsWellsLists();

            //Remove groups with duplicate locations
            ValidateGroupLocationUniqness();

            //Remove groups intersecting
            RemoveGroupsIntersections();

            //Remove wells with duplicate locations
            ValidateWellLocationUniqness();

        }
    }
}