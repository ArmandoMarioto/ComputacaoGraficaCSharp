using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoLinhas
{
    class Caixinha
    {
        private double Ymax, Xmin;
        private double Incx;
        private Caixinha prox, ant;

        public Caixinha(double y, double x, double inc)
        {
            Ymax = y;
            Xmin = x;
            Incx = inc;
            prox = null;
            ant = null;
        }

        public void setAnt(Caixinha c)
        {
            ant = c;
        }

        public Caixinha getAnt()
        {
            return ant;
        }

        public void setProx(Caixinha c)
        {
            prox = c;
        }

        public Caixinha getProx()
        {
            return prox;
        }

        public double getYmax()
        {
            return Ymax;
        }

        public void setYmax(double y)
        {
            Ymax = y;
        }

        public double getXmin()
        {
            return Xmin;
        }

        public void setXmin(double x)
        {
            Xmin = x;
        }
        public double getIncx()
        {
            return Incx;
        }
        public void setIncx(double inc)
        {
            Incx = inc;
        }
    }
}
