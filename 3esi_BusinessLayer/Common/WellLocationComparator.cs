using System.Collections.Generic;
using Esi_BusinessLayer.Parsing;

namespace Esi_BusinessLayer.Common
{
    public class WellLocationComparator : IEqualityComparer<WellRecord>
    {
        public bool Equals(WellRecord compareWellRecord, WellRecord compareWellRecordTo)
        {
            if (ReferenceEquals(compareWellRecord, compareWellRecordTo)) return true;
            if (ReferenceEquals(compareWellRecord, null) || ReferenceEquals(compareWellRecordTo, null)) return false;

            return compareWellRecord.BottomX.Equals(compareWellRecordTo.BottomX) &&
                   compareWellRecord.BottomY.Equals(compareWellRecordTo.BottomY) &&
                   compareWellRecord.TopX.Equals(compareWellRecordTo.TopX) &&
                   compareWellRecord.TopY.Equals(compareWellRecordTo.TopY);
        }

        public int GetHashCode(WellRecord obj)
        {
            return obj.TopX.GetHashCode() ^ obj.TopY.GetHashCode() ^ obj.BottomX.GetHashCode() ^ obj.BottomY.GetHashCode();
        }
    }
}
