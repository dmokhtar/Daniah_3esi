using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esi_BusinessLayer.Rules
{
    public class GroupDetails : GroupRecord
    {
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
