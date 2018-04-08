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
        public List<IRecord> failedRecords { get; set; }
        private Dictionary<IRecord, List<WellRecord>> groupsDictionary;

        public List<IRecord> Records { get; set; }

        public ValidateRecord()
        {
            failedRecords = new List<IRecord>();
            groupsDictionary = new Dictionary<IRecord, List<WellRecord>>();
        }

        public void ValidateNameUniqness()
        {
            failedRecords = Records.GroupBy(record => record.Name).Select(item => item.First()).ToList();
            Records = Records.Except(failedRecords).ToList();
        }

        public List<GroupRecord>  ValidateGroupLocationUniqness(List<GroupRecord> groupRecords)
        {
            List<GroupRecord> failedGroupRecords = groupRecords.Distinct(new GroupLocationComparator()).ToList();
            failedRecords.AddRange(failedGroupRecords);
            return groupRecords.Except(failedGroupRecords).ToList();
        }

        public List<WellRecord> ValidateWellLocationUniqness(List<WellRecord> wellRecords)
        {
            List<WellRecord> failedWellRecords = wellRecords.Distinct(new WellLocationComparator()).ToList();
            failedRecords.AddRange(failedWellRecords);
            return wellRecords.Except(failedWellRecords).ToList();
        }


        public void Dispose()
        {
            Records?.Clear();
        }

        public Dictionary<IRecord, List<WellRecord>> SetGroupsChildren(List<GroupRecord> groupsList, List<WellRecord> wellsList)
        {
            groupsList.ForEach(record => groupsDictionary.Add(record, new List<WellRecord  >()));
            AddWellsToGroups(wellsList);
            return groupsDictionary;
        }

        private void AddWellsToGroups(List<WellRecord> wellRecords)
        {
            List<WellRecord> deletedWells = new List<WellRecord>();
            foreach (KeyValuePair<IRecord, List<WellRecord>> group in groupsDictionary)
            {
                foreach (var well in wellRecords)
                {
                    if (IsWellGroupChild((GroupRecord)group.Key, (WellRecord)well))
                    {
                        groupsDictionary[group.Key].Add((WellRecord)well);
                        deletedWells.Add(well);
                    }
                }
                wellRecords = wellRecords.Except(deletedWells).ToList();
            }
        }

        public void GetGroupsWellsLists(List<IRecord> records, List<GroupRecord> groupsList, List<WellRecord> wellsList)
        {
            records.ForEach(record =>
            {
                if (record.GetType() == typeof(GroupRecord)) groupsList.Add((GroupRecord)record);
                else wellsList.Add((WellRecord)record);
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
        public void SetWellType(List<WellRecord> wellsList)
        {
            foreach (var well in wellsList)
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

        public double CalculateDistance(int x1, int y1, int x2, int y2)
        {
            double powerX = Math.Pow(x2 - x1, 2);
            double powerY = Math.Pow(y2 - y1, 2);
            return Math.Sqrt(powerX + powerY);
        }

        public List<GroupRecord> RemoveGroupsIntersections(List<GroupRecord> groupDetailsList)
        {
            List<IRecord> intersectingFailedRecords = new List<IRecord>();

            for (int i = 0; i < groupDetailsList.Count - 1; i++)
            {
                GroupRecord group1 = groupDetailsList[i];
                for (int j = i+1; j < groupDetailsList.Count; j++)
                {
                    GroupRecord group2 = groupDetailsList[j];
                    if (intersectingFailedRecords.Count == 0 || !intersectingFailedRecords.Contains(group2))
                    {
                        double distance = CalculateDistance(group1.LocationX, group1.LocationY, group2.LocationX, group2.LocationY);

                        if (distance > group1.Radius + group2.Radius)
                        {
                            //not intersecting, leave group
                        }
                        else if ((distance <= group1.Radius + group2.Radius) || distance < Math.Abs(group1.Radius - group2.Radius)
                                    || (distance == 0 && group1.Radius == group2.Radius))
                        {
                            intersectingFailedRecords.Add(group2);
                        }
                        else
                            intersectingFailedRecords.Add(group2);
                    }
                }                
            }

            failedRecords.AddRange(intersectingFailedRecords);
            List<GroupRecord> groups = intersectingFailedRecords.Cast<GroupRecord>().ToList();
            return groupDetailsList.Except(groups).ToList();
        }

    }
}