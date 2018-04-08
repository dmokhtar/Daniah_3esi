using System;

namespace Esi_BusinessLayer.Abstraction
{
    public interface IRecord
    {
        String RecordType { get; set; }

        string Name { get; set; }
    }
}
