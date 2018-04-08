using System.Collections.Generic;
using Esi_BusinessLayer.Abstraction;
using Esi_BusinessLayer.Parsing;

namespace _3esi.Tests.Rules.Abstraction
{
    public class AbstractValidateTest
    {
        protected List<IRecord> RecordsList => new List<IRecord>
        {
            new WellRecord
            {
                Name = "SomeName",
                BottomX = 10,
                BottomY = 20,
                RecordType = "Well",
                TopX = 10,
                TopY = 10
            },
            new WellRecord
            {
                Name = "SomeName1",
                BottomX = 10,
                BottomY = 20,
                RecordType = "Well",
                TopX = 10,
                TopY = 10
            },
            new GroupRecord
            {
                Name = "SomeName",
                LocationX = 10,
                LocationY= 20,
                RecordType = "Well",
                Radius = 10
            },
            new GroupRecord
            {
                Name = "SomeName",
                LocationX = 10,
                LocationY= 20,
                RecordType = "Well",
                Radius = 10
            }
        };

        protected List<GroupRecord> IntersectingGroupsList => new List<GroupRecord>
        {   
            new GroupRecord
            {
                Name = "Group A",
                LocationX = 1,
                LocationY= 5,
                RecordType = "Group",
                Radius = 5
            },
            new GroupRecord
            {
                Name = "Group B",
                LocationX = 1,
                LocationY= 16,
                RecordType = "Group",
                Radius = 2
            },
            new GroupRecord
            {
                Name = "Group C",
                LocationX = 10,
                LocationY= 20,
                RecordType = "Group",
                Radius = 5
            },
            new GroupRecord
            {
                Name = "Group D",
                LocationX = 10,
                LocationY= 20,
                RecordType = "Group",
                Radius = 10
            }
        };
    }
}