using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoLinhas
{
    class ET
    {
        private Caixinha inicio;
        private ET next;
        private int num;
        public ET()
        {
            inicio = null;
            next = null;
            num = -1;
        }

        public void setNum(int x)
        {
            num = x;
        }

        public ET(int y, ET p)
        {
            num = y;
            next = p;
            inicio = null;
        }

        public ET getNext()
        {
            return next;
        }

        public void setNext(ET et)
        {
            next = et;
        }

        public int getNum()
        {
            return num;
        }

        public void setInicio(Caixinha c)
        {
            c.setProx(inicio);
            inicio = c;
        }

        public Caixinha getInicio()
        {
            return inicio;
        }
    }
}
