using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoLinhas
{
    public partial class Form1 : Form
    {
        private String erro;
        public static Bitmap bmp, bmpCor;
        private ArrayList array;
        private Poligono atual;
        int x1, x2, y1, y2, total, wid, hei;
        private Color cor;
        private bool pintar;
        private ET et;

        public Form1()
        {
            InitializeComponent();

            bmp = new Bitmap(tela.Width, tela.Height);
            limpaBMP(bmp, Color.White);
            tela.Image = bmp;

            bmpCor = new Bitmap(cores.Width, cores.Height);
            limpaBMP(bmpCor, Color.Black);
            cores.Image = bmpCor;

            wid = tela.Width;
            hei = tela.Height;

            total = 0;
            array = new ArrayList();
            cor = Color.Black;
            pintar = false;

            btnAdd.Enabled = btnExcluir.Enabled = true;
            btnConf.Enabled = btnCancel.Enabled = false;
            txtNome.Enabled = false;
            panel2.Enabled = false;
        }

        private void limpaBMP(Bitmap bmp, Color cor)
        {
            
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                    bmp.SetPixel(i, j, cor);
            }
        }

        public void desenhar()
        {
            Equacoes err;
            if (rbLinha.Checked)
            {
                err = new Equacoes(x1, y1, x2, y2);

                if (rbERR.Checked)
                    err.desenharERR(bmp);
                else if (rbMD.Checked)
                    err.desenharDDA(bmp);
                else if (rbPM.Checked)
                    err.desenharRetasRapidas(bmp);
                else
                {
                    txtErro.Text = "Selecione uma das linhas";
                    return;
                }
            }
            else
            {
                int raio = x2 - x1, raio2 = y2 - y1;

                if (raio < 0)
                    raio = raio * (-1);
                if (raio2 < 0)
                    raio2 *= -1;

                if (raio2 > raio)
                    raio = raio2;

                err = new Equacoes(raio, x1, y1);


                if (rbEquaExpli.Checked)
                    err.desenharEquacExplicita(bmp);
                else if (rbPMC.Checked)
                    err.desenharPontoMedio(bmp);
                else if (rbElipse.Checked)
                {
                    raio = Math.Abs(x2 - x1);
                    raio2 = Math.Abs(y2 - y1);

                    int cx = (x1 + x2) / 2;
                    int cy = (y1 + y2) / 2;
                    err = new Equacoes(raio, raio2, cx, cy);
                    err.desenharElipse(bmp);
                }
                else
                {
                    txtErro.Text = "Selecione uma das Circunferências";
                    return;
                }
            }
            txtErro.Text = "";
            tela.Image = bmp;
        }

        private void novoPoligono(object sender, MouseEventArgs e)
        {
            btnAdd.Enabled = btnExcluir.Enabled = false;
            btnConf.Enabled = btnCancel.Enabled = true;
            txtNome.Enabled = true;
            Poligono p = new Poligono(total++);
            atual = p;
        }

        private void confirmar(object sender, MouseEventArgs e)
        {
            btnAdd.Enabled = btnExcluir.Enabled = true;
            btnConf.Enabled = btnCancel.Enabled = false;

            desenharPoligono(atual);
            if (!txtNome.Text.Equals(""))
                atual.setNome(txtNome.Text);

            array.Add(atual);
            txtNome.Text = "";
            cbPolig.Items.Add(atual.getNome());
            cbPolig1.Items.Add(atual.getNome());
            cbPollig3.Items.Add(atual.getNome());

            tela.Image = bmp;
            txtNome.Enabled = false;
            txtCoord.Text = atual.todasCoordenadas();
        }

        private void desenharPoligono(Poligono p)
        {
            int qtd = p.getQtd();
            Lista prim, fim;
            for (int i = 0; i < qtd; i++)
            {
                prim = p.getPontos(i);
                if (i + 1 == qtd)
                    fim = p.getPontos(0);
                else
                    fim = p.getPontos(i + 1);
                Equacoes eq = new Equacoes(prim.getX(), prim.getY(), fim.getX(), fim.getY());
                eq.desenharRetasRapidas(bmp);
            }
        }

        private void desenharTransformacao(Poligono p)
        {
            //limpaBMP();
            int qtd = p.getQtd();
            Lista prim, fim;
            for (int i = 0; i < qtd; i++)
            {
                prim = p.getPontosAtuais(i);
                if (i + 1 == qtd)
                    fim = p.getPontosAtuais(0);
                else
                    fim = p.getPontosAtuais(i + 1);
                Equacoes eq = new Equacoes(prim.getX(), prim.getY(), fim.getX(), fim.getY());
                eq.desenharRetasRapidas(bmp);
            }
        }

        private void cbPolig1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String nome = cbPolig1.Text;
            foreach (Poligono item in array)
            {
                if (item.getNome().Equals(nome))
                {
                    atual = item;
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                cor = colorDialog1.Color;
                limpaBMP(bmpCor, cor);
                cores.Image = bmpCor;
            }
        }

        private void tbBalde_Click(object sender, EventArgs e)
        {
            pintar = !pintar;
        }

        private void excluir(object sender, EventArgs e)
        {
            String nome = cbPolig.Text;
            Poligono p;
            if (cbPolig.SelectedItem == null)
            {
                txtErro.Text = "Selecione um poligono";
                cbPolig.Focus();
                return;
            }

            foreach (Poligono item in array)
            {
                if (item.getNome().Equals(cbPolig.Text))
                {
                    array.Remove(item);
                    break;
                }
            }

            limpaBMP(bmp, Color.White);
            cbPolig.Items.Clear();
            cbPolig1.Items.Clear();
            cbPollig3.Items.Clear();
            foreach (Poligono item in array)
            {
                desenharPoligono(item);
                cbPolig.Items.Add(item.getNome());
                cbPolig1.Items.Add(item.getNome());
                cbPollig3.Items.Add(item.getNome());
            }

            txtErro.Text = "";
            txtCoord.Text = "";
            txtNome.Text = "";
            txtNome.Enabled = false;
        }

        private void btnDesenhar_Click(object sender, EventArgs e)
        {
            if (cbPolig1.SelectedItem == null)
            {
                txtErro.Text = "Selecione um poligono";
                cbPolig1.Focus();
                return;
            }
            
            if (rbTrans.Checked)
            {
                try
                {
                    atual.translacao(double.Parse(tbTransX.Text), double.Parse(tbTransY.Text));
                }
                catch (Exception ex) { txtErro.Text = "Digite números no campo de translação"; tbTransX.Focus(); }
            }
            else
            if (rbRot.Checked)
            {
                try
                {
                    atual.rotacao(int.Parse(tbRot.Text));
                }
                catch (Exception ex) { txtErro.Text = "Digite números no campo de rotação"; tbRot.Focus(); }
            }
            else
            if(rbEscala.Checked)
            {
                try
                {
                    Double numero = Double.Parse(tbEscala.Text.Replace('.', ','));
                    if (numero >= 0)
                        atual.escala(numero);
                    else
                    {
                        txtErro.Text = "Digite números postivos no campo de escala";
                        tbEscala.Focus();
                    }
                }
                catch (Exception ex) { txtErro.Text = "Digite números no campo de escala"; tbEscala.Focus(); }
            }
            else
            if (rbCisalhamento.Checked)
            {
                bool erro = false;
                if (!tbCisX.Text.Trim().Equals(""))
                {
                    try
                    {
                        atual.cisalhamentoX(double.Parse(tbCisX.Text.Trim()));
                    }
                    catch (Exception ex) { erro = true; txtErro.Text = "Digite números no campo de CisalhamentoX"; tbCisX.Focus(); }
                }

                if (!tbCisY.Text.Trim().Equals("") && !erro)
                {
                    try
                    {
                        atual.cisalhamentoY(double.Parse(tbCisY.Text.Trim()));
                    }
                    catch (Exception ex) { erro = true; txtErro.Text = "Digite números no campo de CisalhamentoY"; tbCisY.Focus(); }
                }
                    
            }
            else
            if(rbEspelhamento.Checked)
            {
                if (cbVertical.Checked)
                    atual.espelhamentoY();
                    
                if (cbHorizontal.Checked)
                    atual.espelhamentoX();
            }

            limpaBMP(bmp, Color.White);
            atual.mudaAtuais();
            desenharTodos();
            txtErro.Text = "";
            tela.Image = bmp;
        }

        private void desenharTodos()
        {
            foreach (Poligono item in array)
                desenharTransformacao(item);
            //adicionar linhas e circulos
        }

        

        private void mudarPoligono(object sender, EventArgs e)
        {
            String nome = cbPolig.Text;
            foreach (Poligono item in array)
            {
                if (item.getNome().Equals(nome))
                {
                    txtCoord.Text = item.todasCoordenadas();
                    break;
                }
            }
        }

        private void cancelar(object sender, EventArgs e)
        {
            txtNome.Text = "";
            btnAdd.Enabled = btnExcluir.Enabled = true;
            btnConf.Enabled = btnCancel.Enabled = false;
            atual = null;
            txtNome.Enabled = false;
        }

        private void btnLimpar_Click()
        {
            limpaBMP(bmp, Color.White);
            tela.Image = bmp;
            array = new ArrayList();
            txtCoord.Text = "";
            txtErro.Text = "";
            cbPolig.Items.Clear();
            cbPolig1.Items.Clear();
            cbPollig3.Items.Clear();
            cbPolig.SelectedItem = "";
            cbPolig1.SelectedItem = "";
            cbPollig3.SelectedItem = "";
        }

        private void rbLinha_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLinha.Checked)
            {
                panel1.Enabled = true;
                panel2.Enabled = false;
            }
        }

        private void rbCirc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCirc.Checked)
            {
                panel1.Enabled = false;
                panel2.Enabled = true;
            }
        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void Cores_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            btnLimpar_Click();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {

        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void cbPollig3_SelectedIndexChanged(object sender, EventArgs e)
        {
            String nome = cbPollig3.Text;
            foreach (Poligono item in array)
            {
                if (item.getNome().Equals(nome))
                {
                    atual = item;
                    break;
                }
            }
        }

        private void coordInicial(object sender, MouseEventArgs e)
        {
            x1 = e.X;
            y1 = e.Y;

            if (tabControl1.SelectedTab == tabPolig && atual != null)
            {
                atual.addPontos(x1, y1);
            }
            if (pintar && tabControl1.SelectedTab == tabCor)
            {
                baldeDeTinta();
                //desenharTodos();
            }
        }

        private void coordFinal(object sender, MouseEventArgs e)
        {
            x2 = e.X;
            y2 = e.Y;

            if (tabControl1.SelectedTab == tabPri)
                desenhar();
        }

        private void baldeDeTinta()
        {
            Stack<Lista> pilha = new Stack<Lista>();
            Color pixel = bmp.GetPixel(x1, y1), aux;
            Lista ponto;

            pilha.Push(new Lista(x1, y1));
            while (pilha.Count != 0)
            {
                ponto = pilha.Pop();
                bmp.SetPixel(ponto.getX(), ponto.getY(), cor);
                if (ponto.getX() < bmp.Width - 1 && ponto.getX() > 0)
                {
                    if (bmp.GetPixel(ponto.getX() - 1, ponto.getY()) == pixel)
                        pilha.Push(new Lista(ponto.getX() - 1, ponto.getY()));
                    if (bmp.GetPixel(ponto.getX() + 1, ponto.getY()) == pixel)
                        pilha.Push(new Lista(ponto.getX() + 1, ponto.getY()));
                }

                if (ponto.getY() < bmp.Height - 1 && ponto.getY() > 0)
                {
                    if (bmp.GetPixel(ponto.getX(), ponto.getY() + 1) == pixel)
                        pilha.Push(new Lista(ponto.getX(), ponto.getY() + 1));
                    if (bmp.GetPixel(ponto.getX(), ponto.getY() - 1) == pixel)
                        pilha.Push(new Lista(ponto.getX(), ponto.getY() - 1));
                }
            }
            tela.Image = bmp;
        }
        private void btnPreencher_Click(object sender, EventArgs e)
        {
            if (cbPollig3.SelectedItem == null)
            {
                txtErro.Text = "Selecione um poligono";
                cbPollig3.Focus();
                return;
            }
            txtErro.Text = "";

            CabET cab = new CabET();
            criaET(cab);

            AET aet = new AET();
            ET aux = cab.getInicio();
            int y = aux.getNum();

            aet.copia_para_AET(aux.getInicio());
            aux = aux.getNext();
            while (aet.getInicio() != null)
            {
                aet.removerYMax(y);
                if (aet.getInicio() != null)
                {
                    aet.ordenarXMin();
                    aet.desenhar(bmp, y, cor);
                    aet.atualizarXMin();
                    y++;

                    if (aux != null && y == aux.getNum())
                    {
                        aet.copia_para_AET(aux.getInicio());
                        aux = aux.getNext();
                    }
                }
            }
            tela.Image = bmp;
        }

        private void criaPontos()
        {
            atual = new Poligono(1);
            atual.zeraAtual();
            /*atual.setQtd(6);
            atual.getTodosPontosAtuais().adicionar(2, 3);
            atual.getTodosPontosAtuais().adicionar(7, 1);
            atual.getTodosPontosAtuais().adicionar(13, 5);
            atual.getTodosPontosAtuais().adicionar(13, 11);
            atual.getTodosPontosAtuais().adicionar(7, 7);
            atual.getTodosPontosAtuais().adicionar(2, 9);

            atual.setQtd(3);
            atual.getTodosPontosAtuais().adicionar(8, 10);
            atual.getTodosPontosAtuais().adicionar(4, 2);
            atual.getTodosPontosAtuais().adicionar(12, 2);*/
        }

        private void criaET(CabET cab)
        {
            et = new ET();
            //criaPontos();
            Lista prim, seg;
            int Ymax, Xmin, Xmax, Ymin;
            double Incx;


            for(int i = 0; i < atual.getQtd(); i++)
            {
                prim = atual.getPontosAtuais(i);
                if (i + 1 == atual.getQtd())
                    seg = atual.getPontosAtuais(0);
                else
                    seg = atual.getPontosAtuais(i + 1);

                if (prim.getY() > seg.getY())
                {
                    Ymax = prim.getY();
                    Xmax = prim.getX();
                    Xmin = seg.getX();
                    Ymin = seg.getY();
                }
                else
                {
                    Xmax = seg.getX();
                    Ymax = seg.getY();
                    Xmin = prim.getX();
                    Ymin = prim.getY();
                }

                if (((double)Ymax - (double)Ymin) == 0)
                    Incx = 0;
                else
                    Incx = ((double)Xmax - (double)Xmin)/((double)Ymax - (double)Ymin);

                cab.insere(Ymax, Xmin, Incx, Ymin);
            }
        }
    }
}
