using System;
using FileHelpers;
using Esi_BusinessLayer.Abstraction;

namespace Esi_BusinessLayer
{
    [DelimitedRecord(",")]
    class WellRecord : IRecord
    {   
        public String WellName;

        public int TopX;

        public int TopY;
                
        public int BottomX;
                
        public int BottomY;

        public WellRecord(String wellName, int topX, int topY, int bottomX, int bottomY)
        {
            this.WellName = wellName;
            this.TopX = topX;
            this.TopY = topY;
            this.BottomX = bottomX;
            this.BottomY = bottomY;
        }
    }
}
