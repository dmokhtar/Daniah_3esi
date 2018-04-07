using System;
using FileHelpers;
using Esi_BusinessLayer.Abstraction;

namespace Esi_BusinessLayer
{
    [DelimitedRecord(",")]
    class WellRecord : IRecord
    {
        public String RecordType { get; set; }

        public string Name { get; set; }

        public int TopX { get; set; }

        public int TopY { get; set; }

        public int BottomX { get; set; }

        public int BottomY { get; set; }

    }
}
