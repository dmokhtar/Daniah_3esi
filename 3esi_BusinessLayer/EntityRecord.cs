using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
using Esi_BusinessLayer.Abstraction;

namespace Esi_BusinessLayer
{
    [DelimitedRecord(",")]
    class EntityRecord : IRecord
    {
        public String RecordType;

        public String Name;

        public int IntegerValue1;

        public int IntegerValue2;

        public int IntegerValue3;

        [FieldNullValue(typeof(int), "0")]
        [FieldOptional]
        public int IntegerValue4;                
    }
}
