using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoLinhas
{
    class ListaEncadeada
    {
        private Lista inicio;
        private int qtd;

        public ListaEncadeada(Lista l)
        {
            qtd = 0; 
            inicio = l;
        }

        public Lista getInicio()
        {
            return inicio;
        }

        public int getQtd()
        {
            return qtd;
        }

        public Lista getNo(int i)
        {
            int x = 0;
            Lista aux = inicio;

            while(aux != null && x < i)
            {
                aux = aux.getProx();
                x++;
            }

            if (x == i)
                return aux;
            return null;
        }

        public void adicionar(int x, int y)
        {
            qtd++;
            Lista l = new Lista(x, y);
            if (inicio == null)
                inicio = l;
            else
            {
                Lista aux = inicio;
                while (aux.getProx() != null)
                    aux = aux.getProx();

                aux.setProx(l);
            }
        }
    }
}
