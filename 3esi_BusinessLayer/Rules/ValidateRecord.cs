using System.Collections.Generic;
using Esi_BusinessLayer.Abstraction;
using Esi_BusinessLayer.Validate.Abstraction;

namespace Esi_BusinessLayer.Rules
{
    public class ValidateRecord : IValidateRecord
    {

        public void ValidateBusinessRules()
        {

        }

        public void Dispose()
        {
            Records?.Clear();
        }

        public List<IRecord> Records { get; set; }
    }
}