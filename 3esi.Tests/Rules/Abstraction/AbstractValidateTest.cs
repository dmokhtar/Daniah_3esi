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
    }
}