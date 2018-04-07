using System.Collections.Generic;
using System.Linq;
using Esi_BusinessLayer.Abstraction;
using Esi_BusinessLayer.Validate.Abstraction;

namespace Esi_BusinessLayer.Rules
{
    public class ValidateRecord : IValidateRecord
    {
        private List<IRecord> failedRecords; 

        public void ValidateBusinessRules()
        {
            failedRecords = new List<IRecord>();
        }

        public void ValidateNameUniqness()
        {
            failedRecords = Records.GroupBy(record => record.Name).Where(item => item.Count() > 1).SelectMany(item => item).ToList();
            Records = Records.Except(failedRecords).ToList();
        }

        public void Dispose()
        {
            Records?.Clear();
        }

        public List<IRecord> Records { get; set; }
    }
}