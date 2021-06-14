using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TrabalhoLinhas
{
    class AET
    {
        private Caixinha inicio, fim;
        private int qtde;
        
        public AET()
        {
            fim = null;
            inicio = null;
            qtde = 0;
        }
        

        public Caixinha getInicio()
        {
            return inicio;
        }

        public void copia_para_AET(Caixinha c)
        {
            Caixinha aux = inicio;

            if (aux == null)
                inicio = c;
            else
            {
                while (aux.getProx() != null)
                    aux = aux.getProx();
                aux.setProx(c);
                c.setAnt(aux);
            }
        }

        private Caixinha getAnt(Caixinha c)
        {
            Caixinha aux = inicio;
            while (aux.getProx() != c)
                aux = aux.getProx();
            return aux;
        }

        public void removerYMax(int y)
        {
            Caixinha ant, c = inicio;
            bool removeu = false;
            ant = c;
            while(c != null)
            {
                if (c.getYmax() == y)
                {
                    removeu = true;
                    if (c == inicio)
                    {
                        inicio = inicio.getProx();
                        if(inicio != null)
                            inicio.setAnt(null);
                    }
                    else
                    {
                        ant.setProx(c.getProx());
                        if (c.getProx() != null)
                            c.getProx().setAnt(ant);
                    }
                }

                if (!removeu)
                    ant = c;
                else
                    removeu = false;

                c = c.getProx();
            }
        }

        private int getTL()
        {
            Caixinha c = inicio;
            int i = 0;
            while(c!=null)
            {
                i++;
                fim = c;
                c = c.getProx();
            }
            return i;
        }

        public void ordenarXMin()
        {
            Caixinha j, i;
            int tam = getTL();
            i = fim;
            bool flag = true;
            double x, y, inc;

            if (inicio == null)
                return;

            while(i.getAnt() != null && flag)
            {
                flag = false;
                j = inicio;
                while(j != i)
                {
                    if(j.getXmin() > j.getProx().getXmin())
                    {
                        x = j.getXmin(); y = j.getYmax(); inc = j.getIncx();
                        j.setXmin(j.getProx().getXmin()); j.setYmax(j.getProx().getYmax()); j.setIncx(j.getProx().getIncx());
                        j.getProx().setXmin(x); j.getProx().setYmax(y); j.getProx().setIncx(inc);
                        flag = true;
                    }
                    j = j.getProx();
                }
                i = i.getAnt();
            }
        }
        public void desenhar(Bitmap bmp, int y, Color cor)
        {
            Caixinha c1, c2, aux;
            aux = inicio;

            while (aux != null)
            {
                c1 = aux;
                aux = aux.getProx();
                c2 = aux;
                aux = aux.getProx();

                bmp.SetPixel((int)c1.getXmin(), y, cor);
                for (double x = c1.getXmin(); x < c2.getXmin(); x++)
                    bmp.SetPixel((int)x, y, cor);
            }
        }

        public void atualizarXMin()
        {
            Caixinha aux = inicio;
            while(aux != null)
            {
                aux.setXmin(aux.getXmin() + aux.getIncx());
                aux = aux.getProx();
            }
        }
    }
}
