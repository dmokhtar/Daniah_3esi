using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using Esi_BusinessLayer.Abstraction;

namespace Esi_BusinessLayer
{
    [DelimitedRecord(",")]
    class GroupRecord : IRecord
    {
        public String GroupName;

        public int LocationX;

        public int LocationY;

        public int Radius;

        public GroupRecord(String groupName, int locationX, int locationY, int radius)
        {
            this.GroupName = groupName;
            this.LocationX = locationX;
            this.LocationY = locationY;
            this.Radius = radius;
        }
    }
}
