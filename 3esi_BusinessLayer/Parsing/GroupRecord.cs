using System;
using Esi_BusinessLayer.Abstraction;
using FileHelpers;

namespace Esi_BusinessLayer.Parsing
{
    [DelimitedRecord(",")]
    public class GroupRecord : IRecord
    {
        public String RecordType { get; set; }

        public string Name { get; set; }

        public int LocationX { get; set; }

        public int LocationY { get; set; }

        public double Radius { get; set; }
    }
}
