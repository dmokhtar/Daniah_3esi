using System.Collections.Generic;
using Esi_BusinessLayer.Abstraction;
using Esi_BusinessLayer.Validate.Abstraction;
using System;

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

        public void IsInsideCircle(GroupDetails group, WellDetails well)
        {
            double powerX = Math.Pow(well.TopX - group.LocationX, 2);
            double powerY = Math.Pow(well.TopY - group.LocationY, 2);
            double distance = Math.Sqrt(powerX + powerY);

            if (distance <= group.Radius) //well inside group
            {
                group.WellDetailsList.Add(well);
            }
        }
    }
}