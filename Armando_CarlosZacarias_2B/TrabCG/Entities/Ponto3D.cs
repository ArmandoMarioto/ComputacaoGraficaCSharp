using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoCG.Entities
{
    public class Ponto3D
    {
        public double X { get; set; } 
        public double Y { get; set; }
        public double Z { get; set; }

        public Ponto3D()
        {
            X = Y = Z = 0;
        }

        public Ponto3D(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public Ponto3D(Vertice vertice)
        {
            this.X = vertice.x;
            this.Y = vertice.y;
            this.Z = vertice.z;
        }
    }
}
