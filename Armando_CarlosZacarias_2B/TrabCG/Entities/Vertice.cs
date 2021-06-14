using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoCG.Entities
{
    public class Vertice
    {
        public Vertice()
        { 

        }
        public Vertice(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vertice(int index, double x, double y, double z)
        {
            this.index = index;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public int index { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }
}
