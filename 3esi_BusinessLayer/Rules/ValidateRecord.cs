using System.Collections.Generic;
using System.Linq;
using Esi_BusinessLayer.Abstraction;
using Esi_BusinessLayer.Validate.Abstraction;
using System;
using Esi_BusinessLayer.Common;
using Esi_BusinessLayer.Parsing;

namespace Esi_BusinessLayer.Rules
{
    public class ValidateRecord : IValidateRecord
    {
        private List<IRecord> failedRecords;
        public List<IRecord> Records { get; set; }

        public void ValidateBusinessRules()
        {
            failedRecords = new List<IRecord>();
        }

        public void ValidateNameUniqness()
        {
            failedRecords = Records.GroupBy(record => record.Name).Where(item => item.Count() > 1).SelectMany(item => item).ToList();
            Records = Records.Except(failedRecords).ToList();
        }

        public void Dispose()
        {
            Records?.Clear();
        }

        public void IsGroupChild(GroupDetails group, WellDetails well)
        {
            double distance = CalculateDistance(group.LocationX, group.LocationY, well.TopX, well.TopY);
           
            if (distance <= group.Radius) //well inside group
            {
                group.WellDetailsList.Add(well);
            }
        }

        public void SetWellType(WellDetails well)
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

        public double CalculateDistance(int x1, int y1, int x2, int y2)
        {
            double powerX = Math.Pow(x2 - x1, 2);
            double powerY = Math.Pow(y2 - y1, 2);
            return Math.Sqrt(powerX + powerY);
        }

        public List<IRecord> AreGroupsIntersecting(List<IRecord> records)
        {
            List<IRecord> IntersectingFailedRecords = new List<IRecord>();
            List<IRecord> groupDetailsList = records.Where(record => record.RecordType == "Group").ToList();
            for (int i = 0; i < groupDetailsList.Count - 1; i++)
            {
                GroupRecord group1 = ((GroupRecord)groupDetailsList[i]);

                for (int j = i+1; j < groupDetailsList.Count; j++)
                {
                    GroupRecord group2 = ((GroupRecord)groupDetailsList[j]);
                    if (IntersectingFailedRecords.Count == 0 || !IntersectingFailedRecords.Contains(group2))
                    {
                        double distance = CalculateDistance(group1.LocationX, group1.LocationY, group2.LocationX, group2.LocationY);

                        if (distance > group1.Radius + group2.Radius)
                        {
                            //not intersecting, leave group
                        }
                        else if ((distance <= group1.Radius + group2.Radius) || distance < Math.Abs(group1.Radius - group2.Radius)
                                    || (distance == 0 && group1.Radius == group2.Radius))
                        {
                            IntersectingFailedRecords.Add(group2);
                        }
                        else
                            IntersectingFailedRecords.Add(group2);
                    }
                }                
            }

            records = records.Except(IntersectingFailedRecords).ToList();
            failedRecords = new List<IRecord>();
            failedRecords.AddRange(IntersectingFailedRecords);
            return records;
        }

    }
}