using System;
using FileHelpers;
using Esi_BusinessLayer.Abstraction;

namespace Esi_BusinessLayer
{
    [DelimitedRecord(",")]
    class WellRecord : IRecord
    {
        public String RecordType;

        public string Name { get; set; }

        public int TopX;

        public int TopY;
                
        public int BottomX;
                
        public int BottomY;
        
    }
}
