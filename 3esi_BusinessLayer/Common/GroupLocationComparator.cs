using Esi_BusinessLayer.Parsing;
using System.Collections.Generic;

namespace Esi_BusinessLayer.Common
{
    class GroupLocationComparator : IEqualityComparer<GroupRecord>
    {   
        public bool Equals(GroupRecord compareGroupRecord, GroupRecord compareGroupRecordTo)
        {
            if (ReferenceEquals(compareGroupRecord, compareGroupRecordTo)) return true;
            if (ReferenceEquals(compareGroupRecord, null) || ReferenceEquals(compareGroupRecordTo, null)) return false;

            return compareGroupRecord.LocationX.Equals(compareGroupRecordTo.LocationX) &&
                   compareGroupRecord.LocationY.Equals(compareGroupRecordTo.LocationY);
        }

        public int GetHashCode(GroupRecord obj)
        {
            return obj.LocationY.GetHashCode() ^ obj.LocationX.GetHashCode();
        }
    }
}
