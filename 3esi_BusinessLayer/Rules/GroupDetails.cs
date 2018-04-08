using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Esi_BusinessLayer.Parsing;

namespace Esi_BusinessLayer.Rules
{
    public class GroupDetails : GroupRecord
    {
        public GroupDetails()
        {
            WellDetailsList = new List<WellDetails>();
        }
        public List<WellDetails> WellDetailsList { get; set; }

        double Area { get; set; }

        public double CalculateArea()
        {
            double radius = Radius;
            double pi = Math.PI;
            return Area = pi * (radius * radius);
        }
    }
}
