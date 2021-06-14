using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoCG
{ 
    public class ET
    {
        public ET()
        {

        }
        public ET(double Ymax, double Xmin, double IncX, double Zmin, double IncZ)
        {
            this.Ymax = Ymax;
            this.Xmin = Xmin;
            this.IncX = IncX;
            this.Zmin = Zmin;
            this.IncZ = IncZ;
        }

        public double Ymax { get; set; }
        public double Xmin { get; set; }
        public double IncX { get; set; }
        public double Zmin { get; set; }
        public double IncZ { get; set; }
    }
}
