using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoLinhas
{
    class Poligono
    {
        private int pos, qtd;
        double medX, medY;
        private ListaEncadeada pontosAtuais, pontosOriginais;
        private String nome;
        private double[ , ] MA = { {1, 0, 0}, { 0, 1, 0 }, { 0, 0, 1 } };

        public Poligono(int pos)
        {
            this.nome = "Poligono " + pos;
            qtd = 0;
            this.pos = pos;
            pontosAtuais = new ListaEncadeada(null);
            pontosOriginais = new ListaEncadeada(null);
        }

        public void translacao(double x, double y)
        {
            double[,] transf = new double[,] { { 1, 0, x }, { 0, 1, y }, { 0, 0, 1 } };
            multMatriz(transf);
        }

        public void escala(double px)
        {
            pontoMedio();
            translacao(-medX, -medY);
            double[,] transf = new double[,] { { px, 0, 0}, { 0, px, 0}, { 0, 0, 1} };
            multMatriz(transf);
            translacao(medX, medY);
        }

        public void rotacao(double grau)
        {
            pontoMedio();
            translacao(-medX,-medY);
            double angle = Math.PI * grau / 180.0;
            double[,] transf = new double[,] { { Math.Cos(angle), -Math.Sin(angle), 0 }, 
                                     { Math.Sin(angle),  Math.Cos(angle), 0 },
                                     { 0, 0, 1 } };
            multMatriz(transf);
            translacao(medX, medY);
        }

        public void cisalhamentoX(double a)
        {
            pontoMedio();
            translacao(-medX, -medY);
            double[,] transf = new double[,] { { 1, 0, 0 }, { a, 1, 0 }, { 0, 0, 1 } };
            multMatriz(transf);
            translacao(medX, medY);
        }
        public void cisalhamentoY(double b)
        {
            pontoMedio();
            translacao(-medX, -medY);
            double[,] transf = new double[,] { { 1, b, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            multMatriz(transf);
            translacao(medX, medY);
        }

        public void espelhamentoX()
        {
            pontoMedio();
            translacao(-medX, -medY);
            double[,] transf = new double[,] { { 1, 0, 0 }, { 0, -1, 0 }, { 0, 0, 1 } };
            multMatriz(transf);
            translacao(medX, medY);
        }
        public void espelhamentoY()
        {
            pontoMedio();
            translacao(-medX, -medY);
            double[,] transf = new double[,] { { -1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            multMatriz(transf);
            translacao(medX, medY);
        }
        public void mudaAtuais()
        {
            double[,] pontos;
            Lista l = pontosOriginais.getInicio();
            zeraAtual();

            for (int i = 0; i < qtd; i++)
            {
                pontos = new double[,] { { l.getX() }, { l.getY() }, { 1 } };
                pontos = multMatrizPonto(pontos);
                l = l.getProx();
                pontosAtuais.adicionar((int)pontos[0, 0], (int)pontos[1, 0]);
            }
        }

        private double[,] multMatrizPonto(double[,] mat)
        {
            double[,] retorno = new double[3, 1];

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 1; j++)
                    for (int k = 0; k < 3; k++)
                        retorno[i, j] += MA[i, k] * mat[k, j];
            
            return retorno;
        }

        private void multMatriz(double[,] transf)
        {
            double[,] final = new double[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    for (int k = 0; k < 3; k++)
                        final[i, j] += transf[i,k] * MA[k, j];
            
            MA = final;
        }

        public void zeraAtual()
        {
            pontosAtuais = new ListaEncadeada(null);
        }

        public void pontoMedio()
        {
            int maiorX = -1, maiorY = -1, menX = int.MaxValue, menY = int.MaxValue;
            Lista lista = pontosAtuais.getInicio();

            for (int i = 0; i < qtd; i++)
            {
                if (lista.getX() > maiorX)
                    maiorX = lista.getX();

                if (lista.getX() < menX)
                    menX = lista.getX();

                if (lista.getY() > maiorY)
                    maiorY = lista.getY();

                if (lista.getY() < menY)
                    menY = lista.getY();
                lista = lista.getProx();
            }

            medX = (maiorX + menX) / 2.0;
            medY = (maiorY + menY) / 2.0;
        }

        public double[,] getAcumulada()
        {
            return MA;
        }

        public void setAcumulada(double[,] ma)
        {
            MA = ma;
        }

        public void addAtual(int x, int y)
        {
            pontosAtuais.adicionar(x, y);
            
        }

        public ListaEncadeada getTodosPontos()
        {
            return pontosOriginais;
        }

        public void setQtd(int x)
        {
            qtd = x;
        }

        public ListaEncadeada getTodosPontosAtuais()
        {
            return pontosAtuais;
        }

        public ListaEncadeada getAtuais()
        {
            return pontosAtuais;
        }

        public int getPos()
        {
            return pos;
        }

        public void setNome(String nome)
        {
            this.nome = nome;
        }

        public String getNome()
        {
            return nome;
        }

        public int getQtd()
        {
            return qtd;
        }

        public void addPontos(int x, int y)
        {
            qtd++;
            pontosOriginais.adicionar(x, y);
            pontosAtuais.adicionar(x, y);
        }

        public Lista getPontos(int i)
        {
            return pontosOriginais.getNo(i);
        }

        public Lista getPontosAtuais(int i)
        {
            return pontosAtuais.getNo(i);
        }

        public String todasCoordenadas()
        {
            int i = 1;
            String coordenadas = "Pontos do poligono: "+nome+ "\r\n";

            Lista caixinha = pontosOriginais.getInicio();
            while(caixinha != null)
            {
                coordenadas += "x" + i + " = " + caixinha.getX() + "  y" + i + " = " + caixinha.getY()+ "\r\n";
                caixinha = caixinha.getProx();
                i++;
            }
            
            return coordenadas;
        }
    }
}
