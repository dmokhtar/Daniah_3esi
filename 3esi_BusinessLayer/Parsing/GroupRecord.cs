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
        public String RecordType;

        public string Name { get; set; }

        public int LocationX;

        public int LocationY;

        public int Radius;       
    }
}
