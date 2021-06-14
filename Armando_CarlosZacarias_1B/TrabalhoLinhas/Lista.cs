using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoLinhas
{
    class Lista
    {
        private Lista prox;
        private int x, y;

        public Lista(int x, int y)
        {
            this.x = x;
            this.y = y;
            prox = null;
        }

        public Lista getProx()
        {
            return prox;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void setProx(Lista lista)
        {
            prox = lista;
        }

        public void setX(int x)
        {
            this.x = x;
        }

        public void setY(int y)
        {
            this.y = y;
        }
    }
}
