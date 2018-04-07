using System;
using System.Collections.Generic;
using Esi_BusinessLayer.Abstraction;

namespace Esi_BusinessLayer.Validate.Abstraction
{
    public interface IValidateRecord : IDisposable
    {
        List<IRecord> Records { get; set; }
    }
}