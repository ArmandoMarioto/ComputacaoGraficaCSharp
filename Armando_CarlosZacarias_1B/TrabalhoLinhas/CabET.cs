using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoLinhas
{
    class CabET
    {
        private ET inicio;
        public CabET()
        {
            inicio = null;
        }

        public ET getInicio()
        {
            return inicio;
        }

        public void setInicio(ET et)
        {
            inicio = et;
        }

        public void insere(int y, int x, double inc, int ymin)
        {
            ET et = new ET(ymin, null);
            Caixinha c = new Caixinha(y, x, inc);
            et.setInicio(c);

            if (inicio == null)
            {
                inicio = et;
            }
            else
            {
                ET aux = inicio, ant = null;
                while(aux != null && ymin > aux.getNum())
                {
                    ant = aux;
                    aux = aux.getNext();
                }

                if(aux != null)
                {
                    if(aux.getNum() == ymin)
                    {
                        c.setProx(aux.getInicio());
                        aux.getInicio().setAnt(c);
                        aux.setInicio(c);
                    }
                    else
                    {
                        et.setNext(aux);
                        if(ant != null)
                            ant.setNext(et);
                        else
                            inicio = et;
                    }
                }
                else
                {
                    et.setNext(aux);
                    ant.setNext(et);
                }
            }
        }
    }

    
}
