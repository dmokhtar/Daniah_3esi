using System;
using Esi_BusinessLayer.Abstraction;
using FileHelpers;

namespace Esi_BusinessLayer.Parsing
{
    [DelimitedRecord(",")]
    public class WellRecord : IRecord
    {
        public String RecordType { get; set; }

        public string Name { get; set; }

        public int TopX { get; set; }

        public int TopY { get; set; }

        public int BottomX { get; set; }

        public int BottomY { get; set; }

        [FieldHidden]
        public String WellType { get; set; }

    }
}
