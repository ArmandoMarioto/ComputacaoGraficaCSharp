using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabalhoCG.Entities;
using TrabalhoCG.ProcessamentoImagem;

namespace TrabalhoCG
{ 
    public partial class Form1 : Form
    {
        private List<Modelo3D> objetos3D;
        private Bitmap ImagemCenario;
        private Modelo3D atual;
        private Point posLeftClick { get; set; }
        private Point posRightClick { get; set; }
        private Boolean mouseLeftClick { get; set; }
        private Boolean mouseRightClick { get; set; }
        private Boolean movimentaFonteLuz = false;

        private List<ET>[] EdgeTable { get; set; }

        private List<ET> Aet { get; set; }

        private Dictionary<int, Vertice> verticesAtuaisBackup { get; set; }

        private int dif = 0;
        private int max = 2;
        private Ponto3D vetorNormalSol { get; set; }

        private double[,] matrizAcumuladaAnterior { get; set; }


        public Form1()
        {
            InitializeComponent();
            objetos3D = new List<Modelo3D>();
            ImagemCenario = new Bitmap(pbCenario.Width, pbCenario.Height);
            atual = new Modelo3D();
            mouseLeftClick = false;
            mouseRightClick = false;
            pbCenario.MouseWheel += Mouse_Escala;
            matrizAcumuladaAnterior = new double[4, 4];
            tBD.Enabled = false;
            verticesAtuaisBackup = new Dictionary<int, Vertice>();
            Aet = new List<ET>();
            this.KeyPreview = true;

        }

        private void Mouse_Escala(object sender, MouseEventArgs e)
        {
            var escala = e.Delta > 0 ? 1.1 : 0.8;
            var centro = CalculaCentro(atual);
            Transladar(-centro.X, -centro.Y, -centro.Z);
            Escalar(escala, escala, escala);
            Transladar(centro.X, centro.Y, centro.Z);
            ImagemCenario = new Bitmap(pbCenario.Width, pbCenario.Height);
            DesenhaModelo3D(Color.White);
            pbCenario.Image = ImagemCenario;
        }

        private double[,] MatrizIdentidade()
        {
            double[,] m = new double[4, 4];
            for (int i = 0; i < m.GetLength(0); i++)
                m[i, i] = 1;
            return m;
        }

        private double[,] MatrizMultiplica(double[,] m1, double[,] m2)
        {
            double[,] aux = new double[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < 4; k++)
                        aux[i, j] += m1[i, k] * m2[k, j];
            return aux;
        }

        public void ExtrairInformacoesObjeto3D(Modelo3D objeto)
        {
            StreamReader rdr = new StreamReader(objeto.arquivo, Encoding.UTF8);
            List<Vertice> verticesAux = new List<Vertice>();
            List<Face> facesAux = new List<Face>();

            for (int i = 0; !rdr.EndOfStream; i++)
            {
                var linha = rdr.ReadLine();
                if (!String.IsNullOrEmpty(linha))
                {
                    if (linha[0] == 'v' && linha[1] == ' ')
                    {
                        linha = linha.Substring(2).Trim();
                        var infos = linha.Split(' ');

                        var vertice = new Vertice();
                        vertice.index = i + 1;
                        vertice.x = Convert.ToDouble(infos[0].Trim().Replace(".", ","));
                        vertice.y = Convert.ToDouble(infos[1].Trim().Replace(".", ","));
                        vertice.z = Convert.ToDouble(infos[2].Trim().Replace(".", ","));
                        verticesAux.Add(vertice);
                        objeto.verticesOriginais.Add(i + 1, new Vertice(vertice.index, vertice.x + (pbCenario.Width / 2), vertice.y + (pbCenario.Height / 2), vertice.z));
                        objeto.verticesAtuais.Add(i + 1, new Vertice(vertice.index, vertice.x + (pbCenario.Width / 2), vertice.y + (pbCenario.Height / 2), vertice.z));
                    }

                    if (linha[0] == 'f')
                    {
                        linha = linha.Substring(1).Trim();
                        var infos = linha.Split(' ');

                        var face = new Face();
                        face.index = i + 1;
                        for (int j = 0; j < infos.Count(); j++)
                        {
                            var index = infos[j].Replace("//", "-").Split('-');
                            var indexVertice = Convert.ToInt32(index[0]);
                            var vertice = verticesAux.Where(v => v.index == indexVertice).FirstOrDefault();
                            if (vertice != null && j == 0)
                            {
                                face.v1 = indexVertice;
                            }
                            else
                            if (vertice != null && j == 1)
                            {
                                face.v2 = indexVertice;
                            }
                            else if (vertice != null && j == 2)
                            {
                                face.v3 = indexVertice;
                            }
                        }
                        facesAux.Add(face);
                    }
                }
            }
            objeto.faces = facesAux;
            objetos3D.Add(objeto);
        }



        public void DesenhaVista(int vista, Bitmap imagem)
        {

            if (cBFaceOculta.Checked)
            {
                atual.CalculaNormalFace();

                for (int i = 0; i < atual.faces.Count; i++)
                {
                    var v1 = atual.faces[i].v1;
                    var v2 = atual.faces[i].v2;
                    var v3 = atual.faces[i].v3;
                    if (atual.faces[i].normalFace.Z >= 0)
                    {
                        var vertice1 = atual.verticesAtuais[v1];
                        var vertice2 = atual.verticesAtuais[v2];
                        var vertice3 = atual.verticesAtuais[v3];
                        Ponto3D ponto1 = new Ponto3D((int)vertice1.x, (int)vertice1.y, (int)vertice1.z);
                        Ponto3D ponto2 = new Ponto3D((int)vertice2.x, (int)vertice2.y, (int)vertice2.z);
                        Ponto3D ponto3 = new Ponto3D((int)vertice3.x, (int)vertice3.y, (int)vertice3.z);

                        if (vista == 2)
                        {
                            ponto1 = new Ponto3D(ponto1.Z + (pBLateral.Width / 2), ponto1.Y, ponto1.X);
                            ponto2 = new Ponto3D(ponto2.Z + (pBLateral.Width / 2), ponto2.Y, ponto2.X);
                            ponto3 = new Ponto3D(ponto3.Z + (pBLateral.Width / 2), ponto3.Y, ponto3.X);
                        }
                        else
                            if (vista == 3)
                        {
                            ponto1 = new Ponto3D(ponto1.X, ponto1.Z + (pBSuperior.Height / 2), ponto1.Y);
                            ponto2 = new Ponto3D(ponto2.X, ponto2.Z + (pBSuperior.Height / 2), ponto2.Y);
                            ponto3 = new Ponto3D(ponto3.X, ponto3.Z + (pBSuperior.Height / 2), ponto3.Y);
                        }

                        pontoMedioVistas(ponto1, ponto2, Color.White, imagem);
                        pontoMedioVistas(ponto2, ponto3, Color.White, imagem);
                        pontoMedioVistas(ponto3, ponto1, Color.White, imagem);
                    }
                }
            }
            else
            {

                for (int i = 0; i < atual.faces.Count; i++)
                {
                    var v1 = atual.faces[i].v1;
                    var v2 = atual.faces[i].v2;
                    var v3 = atual.faces[i].v3;
                    var vertice1 = atual.verticesAtuais[v1];
                    var vertice2 = atual.verticesAtuais[v2];
                    var vertice3 = atual.verticesAtuais[v3];
                    Ponto3D ponto1 = new Ponto3D((int)vertice1.x, (int)vertice1.y, (int)vertice1.z);
                    Ponto3D ponto2 = new Ponto3D((int)vertice2.x, (int)vertice2.y, (int)vertice2.z);
                    Ponto3D ponto3 = new Ponto3D((int)vertice3.x, (int)vertice3.y, (int)vertice3.z);

                    if (vista == 2)
                    {
                        ponto1 = new Ponto3D(ponto1.Z + (pBLateral.Width / 2), ponto1.Y, ponto1.X);
                        ponto2 = new Ponto3D(ponto2.Z + (pBLateral.Width / 2), ponto2.Y, ponto2.X);
                        ponto3 = new Ponto3D(ponto3.Z + (pBLateral.Width / 2), ponto3.Y, ponto3.X);
                    }
                    else
                            if (vista == 3)
                    {
                        ponto1 = new Ponto3D(ponto1.X, ponto1.Z + (pBSuperior.Height / 2), ponto1.Y);
                        ponto2 = new Ponto3D(ponto2.X, ponto2.Z + (pBSuperior.Height / 2), ponto2.Y);
                        ponto3 = new Ponto3D(ponto3.X, ponto3.Z + (pBSuperior.Height / 2), ponto3.Y);
                    }

                    pontoMedioVistas(ponto1, ponto2, Color.White, imagem);
                    pontoMedioVistas(ponto2, ponto3, Color.White, imagem);
                    pontoMedioVistas(ponto3, ponto1, Color.White, imagem);
                }
            }
        }

        public void DesenhaModelo3D(Color cor)
        {
            if (rBCabinet.Checked)
            {
                Cabinet();
                atual.matrizAcumulada = matrizAcumuladaAnterior;
            }
            else
            if (rBCavaleira.Checked)
            {
                Cavaleira();
                atual.matrizAcumulada = matrizAcumuladaAnterior;
            }
            else
            if (rBPerspectiva.Checked)
            {
                var d = 0;
                if (!String.IsNullOrEmpty(tBD.Text))
                {
                    d = Convert.ToInt32(tBD.Text);
                }
                else
                {
                    d = 1;
                }
                Perspectiva(d);
                atual.matrizAcumulada = matrizAcumuladaAnterior;

            }

            if (cBFaceOculta.Checked)
            {
                atual.CalculaNormalFace();

                for (int i = 0; i < atual.faces.Count; i++)
                {
                    var v1 = atual.faces[i].v1;
                    var v2 = atual.faces[i].v2;
                    var v3 = atual.faces[i].v3;
                    if (atual.faces[i].normalFace.Z >= 0)
                    {
                        var vertice1 = atual.verticesAtuais[v1];
                        var vertice2 = atual.verticesAtuais[v2];
                        var vertice3 = atual.verticesAtuais[v3];
                        Ponto3D ponto1 = new Ponto3D((int)vertice1.x, (int)vertice1.y, (int)vertice1.z);
                        Ponto3D ponto2 = new Ponto3D((int)vertice2.x, (int)vertice2.y, (int)vertice2.z);
                        Ponto3D ponto3 = new Ponto3D((int)vertice3.x, (int)vertice3.y, (int)vertice3.z);

                        pontoMedio(ponto1, ponto2, cor);
                        pontoMedio(ponto2, ponto3, cor);
                        pontoMedio(ponto3, ponto1, cor);
                    }
                }
            }
            else
            {

                for (int i = 0; i < atual.faces.Count; i++)
                {
                    var v1 = atual.faces[i].v1;
                    var v2 = atual.faces[i].v2;
                    var v3 = atual.faces[i].v3;
                    var vertice1 = atual.verticesAtuais[v1];
                    var vertice2 = atual.verticesAtuais[v2];
                    var vertice3 = atual.verticesAtuais[v3];
                    Ponto3D ponto1 = new Ponto3D((int)vertice1.x, (int)vertice1.y, (int)vertice1.z);
                    Ponto3D ponto2 = new Ponto3D((int)vertice2.x, (int)vertice2.y, (int)vertice2.z);
                    Ponto3D ponto3 = new Ponto3D((int)vertice3.x, (int)vertice3.y, (int)vertice3.z);

                    pontoMedio(ponto1, ponto2, cor);
                    pontoMedio(ponto2, ponto3, cor);
                    pontoMedio(ponto3, ponto1, cor);
                }
            }

            if (rBFlat.Checked)
            {

                var zBuffer = InicializaZbuffer();
                atual.CalculaNormalFace();
                ImagemCenario = new Bitmap(pbCenario.Width, pbCenario.Height);

                for (int f = 0; f < atual.faces.Count; f++)
                {
                    var face = atual.faces[f];

                    if (face.normalFace.Z >= 0)
                    {
                        Preenchimento(face, zBuffer);
                    }
                }
            }

        }

        public void pontoMedio(Ponto3D inicio, Ponto3D fim, Color cor)
        {
            int x1 = (int)inicio.X;
            int x2 = (int)fim.X;
            int y1 = (int)inicio.Y;
            int y2 = (int)fim.Y;

            int declive;
            int dx, dy, incE, incNE, d, x, y;
            dx = (int)(fim.X - inicio.X);
            dy = (int)(fim.Y - inicio.Y);

            if (Math.Abs(dx) > Math.Abs(dy))
            {

                if (x2 < x1)
                {
                    pontoMedio(fim, inicio, cor);
                    return;
                }

                //Constante de Bresenham
                declive = Math.Sign(dy);

                if (dy < 0)
                {
                    dy = -dy;
                }

                incE = 2 * dy;
                incNE = 2 * dy - 2 * dx;
                d = 2 * dy - dx;
                y = (int)(inicio.Y);

                for (x = (int)inicio.X; x <= fim.X; x++)
                {
                    if (x < pbCenario.Width && x >= 0 && y >= 0 && y < pbCenario.Height)
                    {
                        ImagemCenario.SetPixel(x, y, cor);
                    }
                    if (d <= 0)
                    {
                        d += incE;
                    }
                    else
                    {
                        d += incNE;
                        y += declive;
                    }
                }
            }
            else
            {
                if (y2 < y1)
                {
                    pontoMedio(fim, inicio, cor);
                    return;
                }

                //Constante de Bresenham
                declive = Math.Sign(dx);

                if (dx < 0)
                {
                    dx = -dx;
                }

                incE = 2 * dx;
                incNE = 2 * dx - 2 * dy;
                d = 2 * dx - dy;
                x = (int)inicio.X;

                for (y = (int)inicio.Y; y <= fim.Y; y++)
                {
                    if (x < pbCenario.Width && x >= 0 && y >= 0 && y < pbCenario.Height)
                    {
                        ImagemCenario.SetPixel(x, y, cor);
                    }

                    if (d <= 0)
                    {
                        d += incE;
                    }
                    else
                    {
                        d += incNE;
                        x += declive;
                    }
                }
            }
        }

        public void pontoMedioVistas(Ponto3D inicio, Ponto3D fim, Color cor, Bitmap imagem)
        {
            int x1 = (int)inicio.X;
            int x2 = (int)fim.X;
            int y1 = (int)inicio.Y;
            int y2 = (int)fim.Y;

            int declive;
            int dx, dy, incE, incNE, d, x, y;
            dx = (int)(fim.X - inicio.X);
            dy = (int)(fim.Y - inicio.Y);

            if (Math.Abs(dx) > Math.Abs(dy))
            {

                if (x2 < x1)
                {
                    pontoMedio(fim, inicio, cor);
                    return;
                }

                //Constante de Bresenham
                declive = Math.Sign(dy);

                if (dy < 0)
                {
                    dy = -dy;
                }

                incE = 2 * dy;
                incNE = 2 * dy - 2 * dx;
                d = 2 * dy - dx;
                y = (int)(inicio.Y);

                for (x = (int)inicio.X; x <= fim.X; x++)
                {
                    if (x < imagem.Width && x >= 0 && y >= 0 && y < imagem.Height)
                    {
                        imagem.SetPixel(x, y, cor);
                    }
                    if (d <= 0)
                    {
                        d += incE;
                    }
                    else
                    {
                        d += incNE;
                        y += declive;
                    }
                }
            }
            else
            {
                if (y2 < y1)
                {
                    pontoMedio(fim, inicio, cor);
                    return;
                }

                //Constante de Bresenham
                declive = Math.Sign(dx);

                if (dx < 0)
                {
                    dx = -dx;
                }

                incE = 2 * dx;
                incNE = 2 * dx - 2 * dy;
                d = 2 * dx - dy;
                x = (int)inicio.X;

                for (y = (int)inicio.Y; y <= fim.Y; y++)
                {
                    if (x < imagem.Width && x >= 0 && y >= 0 && y < imagem.Height)
                    {
                        imagem.SetPixel(x, y, cor);
                    }

                    if (d <= 0)
                    {
                        d += incE;
                    }
                    else
                    {
                        d += incNE;
                        x += declive;
                    }
                }
            }
        }

        private Vertice CentroModelo()
        {
            var X = 0.0;
            var Y = 0.0;
            var Z = 0.0;
            foreach (var vertice in atual.verticesAtuais)
            {
                X += vertice.Value.x;
                Y += vertice.Value.y;
                Z += vertice.Value.z;
            }
            var count = atual.verticesAtuais.Count;
            Console.WriteLine(String.Format("CentroInterno: Vertice: ({3}, {4}) X: {0}, Y: {1}, Z: {2}", X / count, Y / count, Z / count, atual.verticesAtuais[1].x, atual.verticesAtuais[1].y));
            var ve = new Vertice(X / count, Y / count, Z / count);
            return ve;
        }

        private void pbCenario_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseLeftClick = true;
                posLeftClick = new Point(e.X, e.Y);
                var centro = CalculaCentro(atual);
                var dx = posLeftClick.X - centro.X;
                var dy = posLeftClick.Y - centro.Y;
                Transladar(dx, dy, 0);
                ImagemCenario = new Bitmap(pbCenario.Width, pbCenario.Height);
                DesenhaModelo3D(Color.White);
                pbCenario.Image = ImagemCenario;
            }
            if (e.Button == MouseButtons.Right)
            {
                mouseRightClick = true;
                posRightClick = new Point(e.X, e.Y);
            }
        }
        private Ponto3D CalculaCentro(Modelo3D atual)
        {
            var i = 0;
            var mediaX = 0.0;
            var mediaY = 0.0;
            var mediaZ = 0.0;
            for (; i < atual.verticesAtuais.Count; i++)
            {
                mediaX += atual.verticesAtuais.ElementAt(i).Value.x;
                mediaY += atual.verticesAtuais.ElementAt(i).Value.y;
                mediaZ += atual.verticesAtuais.ElementAt(i).Value.z;
            }
            mediaX /= i;
            mediaY /= i;
            mediaZ /= i;

            return new Ponto3D(mediaX, mediaY, mediaZ);
        }

        private void Transladar(double tx, double ty, double tz)
        {
            var T = new double[4, 4];
            T = MatrizIdentidade();
            T[0, 3] = tx;
            T[1, 3] = ty;
            T[2, 3] = tz;

            Console.WriteLine(String.Format("Transladar: X: {0}, Y: {1}, Z: {2}", tx, ty, tz));
            atual.matrizAcumulada = MatrizMultiplica(T, atual.matrizAcumulada);

            atual.verticesAtuais = ObterNovosVertices();
        }

        private void Escalar(double fx, double fy, double fz)
        {
            var E = new double[4, 4];
            E = MatrizIdentidade();
            E[0, 0] = fx;
            E[1, 1] = fy;
            E[2, 2] = fz;

            atual.matrizAcumulada = MatrizMultiplica(E, atual.matrizAcumulada);

            atual.verticesAtuais = ObterNovosVertices();

        }

        private void RotacionarX(double t)
        {
            var R = new double[4, 4];
            R = MatrizIdentidade();
            R[1, 1] = Math.Cos(t);
            R[1, 2] = -Math.Sin(t);
            R[2, 1] = Math.Sin(t);
            R[2, 2] = Math.Cos(t);

            atual.matrizAcumulada = MatrizMultiplica(R, atual.matrizAcumulada);

            atual.verticesAtuais = ObterNovosVertices();
        }

        private void RotacionarY(double t)
        {
            var R = new double[4, 4];
            R = MatrizIdentidade();
            R[0, 0] = Math.Cos(t);
            R[0, 2] = Math.Sin(t);
            R[2, 0] = -Math.Sin(t);
            R[2, 2] = Math.Cos(t);

            atual.matrizAcumulada = MatrizMultiplica(R, atual.matrizAcumulada);

            atual.verticesAtuais = ObterNovosVertices();
        }

        private void RotacionarZ(double t)
        {
            var R = new double[4, 4];
            R = MatrizIdentidade();
            R[0, 0] = Math.Cos(t);
            R[0, 1] = -Math.Sin(t);
            R[1, 0] = Math.Sin(t);
            R[1, 1] = Math.Cos(t);

            atual.matrizAcumulada = MatrizMultiplica(R, atual.matrizAcumulada);

            atual.verticesAtuais = ObterNovosVertices();

        }

        private Dictionary<int, Vertice> ObterNovosVertices()
        {
            var xAux = 0.0;
            var yAux = 0.0;
            var zAux = 0.0;

            var listAux = new Dictionary<int, Vertice>();
            foreach (var vertice in atual.verticesOriginais)
            {

                var matrizPontos = new double[4] { vertice.Value.x, vertice.Value.y, vertice.Value.z, 1 };
                xAux = 0;
                yAux = 0;
                zAux = 0;
                for (int i = 0; i < 4; i++)
                {
                    var x = 0;
                    for (int j = 0; j < 4; j++, x++)
                    {
                        if (i == 0)
                        {
                            xAux += atual.matrizAcumulada[i, j] * matrizPontos[x];
                        }

                        if (i == 1)
                        {
                            yAux += atual.matrizAcumulada[i, j] * matrizPontos[x];
                        }

                        if (i == 2)
                        {
                            zAux += atual.matrizAcumulada[i, j] * matrizPontos[x];
                        }
                    }
                }
                listAux.Add(vertice.Value.index, new Vertice(vertice.Value.index, xAux, yAux, zAux));
            }
            return listAux;
        }
        private void pbCenario_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseRightClick)
            {


                if (dif % max == 0)
                {
                    var centro = CalculaCentro(atual);
                    Transladar(-centro.X, -centro.Y, -centro.Z);

                    if (rBRotacaoXY.Checked)
                    {
                        double dx = e.X - posRightClick.X;
                        double dy = posRightClick.Y - e.Y;
                        RotacionarX(dy / 5000);
                        RotacionarY(dx / 5000);
                    }


                    if (rBRotacaoZ.Checked)
                    {
                        double dx = e.X - posRightClick.X;
                        double dy = e.Y - posRightClick.Y;
                        double dz = dy + dx;
                        RotacionarZ(dz / 5000);
                    }

                    Transladar(centro.X, centro.Y, centro.Z);

                    ImagemCenario = new Bitmap(pbCenario.Width, pbCenario.Height);
                    DesenhaModelo3D(Color.White);
                    pbCenario.Image = ImagemCenario;
                    dif = 1;

                }
                else
                {
                    dif++;
                }
            }

            if (rBMoverFonteLuz.Checked)
            {
               
                var centroTela = new Point(pbCenario.Width / 2, pbCenario.Height / 2);
                var vetor = new Point(MousePosition.X - centroTela.X, MousePosition.Y - centroTela.Y);
                var norma = Math.Sqrt(Math.Pow(vetor.X, 2) + Math.Pow(vetor.Y, 2));
                vetorNormalSol = new Ponto3D(vetor.X / norma, vetor.Y / norma, 1);

                if (rBFlat.Checked)
                {

                    var zBuffer = InicializaZbuffer();
                    atual.CalculaNormalFace();
                    ImagemCenario = new Bitmap(pbCenario.Width, pbCenario.Height);

                    for (int f = 0; f < atual.faces.Count; f++)
                    {
                        var face = atual.faces[f];

                        if (face.normalFace.Z >= 0)
                        {
                            Preenchimento(face, zBuffer);
                            //ScanLine3D(PbCorAtual.BackColor, zBuffer, face);
                        }
                    }

                    pbCenario.Image = ImagemCenario;
                }

            }
           
        }

        private void pbCenario_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseRightClick)
            {
                mouseRightClick = false;
            }
        }

        private void bGerarVistas_Click(object sender, EventArgs e)
        {
            //Frontal
            VistaViewPort(pbCenario.Width, pbCenario.Height, pBFrontal.Width, pBFrontal.Height);
            var frontal = new Bitmap(pBFrontal.Width, pBFrontal.Height);
            DesenhaVista(1, frontal);
            pBFrontal.Image = frontal;
            atual.verticesAtuais = verticesAtuaisBackup;

            //Lateral
            VistaViewPort(pbCenario.Width, pbCenario.Height, pBLateral.Width, pBLateral.Height);
            var lateral = new Bitmap(pBLateral.Width, pBLateral.Height);
            DesenhaVista(2, lateral);
            pBLateral.Image = lateral;
            atual.verticesAtuais = verticesAtuaisBackup;


            //Superior
            VistaViewPort(pbCenario.Width, pbCenario.Height, pBSuperior.Width, pBSuperior.Height);
            var superior = new Bitmap(pBSuperior.Width, pBSuperior.Height);
            DesenhaVista(3, superior);
            pBSuperior.Image = superior;
            atual.verticesAtuais = verticesAtuaisBackup;
        }

        private void rBParalela_CheckedChanged(object sender, EventArgs e)
        {
            if (rBParalela.Checked)
            {
                bGerarVistas.Enabled = true;
            }
            else
            {
                bGerarVistas.Enabled = false;
            }
        }

        private void Cabinet()
        {
            matrizAcumuladaAnterior = atual.matrizAcumulada;
            var matCabinet = new double[4, 4];
            matCabinet[0, 0] = 1;
            matCabinet[1, 1] = 1;
            matCabinet[0, 2] = Math.Cos(45 * (Math.PI / 180)) / 2;
            matCabinet[1, 2] = Math.Sin(45 * (Math.PI / 180)) / 2;
            matCabinet[3, 3] = 1;

            atual.matrizAcumulada = MatrizMultiplica(matCabinet, atual.matrizAcumulada);

            atual.verticesAtuais = ObterNovosVertices();
        }

        private void Cavaleira()
        {
            matrizAcumuladaAnterior = atual.matrizAcumulada;
            var matCavaleira = new double[4, 4];
            matCavaleira[0, 0] = 1;
            matCavaleira[1, 1] = 1;
            matCavaleira[0, 2] = Math.Cos(45 * (Math.PI / 180));
            matCavaleira[1, 2] = Math.Sin(45 * (Math.PI / 180));
            matCavaleira[3, 3] = 1;

            atual.matrizAcumulada = MatrizMultiplica(matCavaleira, atual.matrizAcumulada);

            atual.verticesAtuais = ObterNovosVertices();
        }

        private void Perspectiva(int d)
        {
            matrizAcumuladaAnterior = atual.matrizAcumulada;
            var listaAux = new Dictionary<int, Vertice>();
            for (int i = 0; i < atual.verticesAtuais.Count; i++)
            {
                var vx = (atual.verticesAtuais.ElementAt(i).Value.x * d) / (atual.verticesAtuais.ElementAt(i).Value.z + d);
                var vy = (atual.verticesAtuais.ElementAt(i).Value.y * d) / (atual.verticesAtuais.ElementAt(i).Value.z + d);
                var vz = (atual.verticesAtuais.ElementAt(i).Value.z * d) / (atual.verticesAtuais.ElementAt(i).Value.z + d);
                var novoVertice = new Vertice(atual.verticesAtuais.ElementAt(i).Value.index, vx, vy, vz);
                listaAux.Add(atual.verticesAtuais.ElementAt(i).Key, novoVertice);
            }

            atual.verticesAtuais = listaAux;
        }

        private void VistaViewPort(int width1, int height1, int width2, int height2)
        {
            verticesAtuaisBackup = atual.verticesAtuais;
            matrizAcumuladaAnterior = atual.matrizAcumulada;
            var listaAux = new Dictionary<int, Vertice>();
            for (int i = 0; i < atual.verticesAtuais.Count; i++)
            {
                var vx = (atual.verticesAtuais.ElementAt(i).Value.x * ((double)width2 / (double)width1));
                var vy = (atual.verticesAtuais.ElementAt(i).Value.y * ((double)height2 / (double)height1));
                var vz = (atual.verticesAtuais.ElementAt(i).Value.z * ((double)height2 / (double)height1));
                var novoVertice = new Vertice(atual.verticesAtuais.ElementAt(i).Value.index, vx, vy, vz);
                listaAux.Add(atual.verticesAtuais.ElementAt(i).Key, novoVertice);
            }

            atual.verticesAtuais = listaAux;
        }

        private void rBPerspectiva_CheckedChanged(object sender, EventArgs e)
        {
            if (rBPerspectiva.Checked)
            {
                tBD.Enabled = true;
            }
            else
            {
                tBD.Enabled = false;
            }
        }

        private void bAtualizar_Click(object sender, EventArgs e)
        {
            ImagemCenario = new Bitmap(pbCenario.Width, pbCenario.Height);
            DesenhaModelo3D(Color.White);
            pbCenario.Image = ImagemCenario;
        }

        private void rBCavaleira_CheckedChanged(object sender, EventArgs e)
        {
            ImagemCenario = new Bitmap(pbCenario.Width, pbCenario.Height);
            DesenhaModelo3D(Color.White);
            pbCenario.Image = ImagemCenario;
        }

        private void rBCabinet_CheckedChanged(object sender, EventArgs e)
        {
            ImagemCenario = new Bitmap(pbCenario.Width, pbCenario.Height);
            DesenhaModelo3D(Color.White);
            pbCenario.Image = ImagemCenario;
        }

        private void PbPaleta_MouseClick(object sender, MouseEventArgs e)
        {
            var corSelecionado = ((Bitmap)PbPaleta.Image).GetPixel(e.X, e.Y);
            PbCorAtual.BackColor = corSelecionado;
            //DesenhaModelo3D(PbCorAtual.BackColor);
        }
        public void ScanLine3D(Color cor, double[,] zBuffer, Face face)
        {
            var max = new Ponto3D(0, 0, 0);
            var min = new Ponto3D(0, 0, 0);

            EdgeTable = new List<ET>[pbCenario.Height];
            for (int i = 0; i < pbCenario.Height; i++)
            {
                EdgeTable[i] = new List<ET>();
            }
            var y = 0;


            var vertice1 = atual.verticesAtuais[face.v1];
            var vertice2 = atual.verticesAtuais[face.v2];
            var vertice3 = atual.verticesAtuais[face.v3];

            var poligonoTriangular = new List<Vertice>();
            poligonoTriangular.Add(vertice1);
            poligonoTriangular.Add(vertice2);
            poligonoTriangular.Add(vertice3);


            for (int i = 0; i < poligonoTriangular.Count - 1; i++)
            {
                var ponto1 = poligonoTriangular[i];
                var ponto2 = poligonoTriangular[i + 1];


                if (ponto1.y < ponto2.y)
                {
                    min = new Ponto3D(ponto1);
                    max = new Ponto3D(ponto2);
                }
                else
                {
                    min = new Ponto3D(ponto2);
                    max = new Ponto3D(ponto1);
                }
                var dx = 0.0;
                var dz = 0.0;
                if ((max.Y - min.Y) != 0)
                {
                    dx = (max.X - min.X) / (double)(max.Y - min.Y);
                    dz = (max.Z - min.Z) / (double)(max.Y - min.Y);
                }

                var et = new ET(max.Y, min.X, dx, min.Z, dz);
                EdgeTable[(int)min.Y].Add(et);
            }

            var p1 = new Vertice();
            p1 = vertice3;
            var p2 = new Vertice();
            p2 = vertice1;

            var ma = new Ponto3D(0, 0, 0);
            var mi = new Ponto3D(0, 0, 0);

            if (p1.y < p2.y)
            {
                mi = new Ponto3D(p1);
                ma = new Ponto3D(p2);
            }
            else
            {
                mi = new Ponto3D(p2);
                ma = new Ponto3D(p1);
            }
            var dxA = 0.0;
            var dzA = 0.0;
            if ((ma.Y - mi.Y) != 0)
            {
                dxA = (ma.X - mi.X) / (double)(ma.Y - mi.Y);
                dzA = (ma.Z - mi.Z) / (double)(ma.Y - mi.Y);
            }
            var etA = new ET(ma.Y, mi.X, dxA, mi.Z, dzA);
            EdgeTable[(int)mi.Y].Add(etA);

            Aet = new List<ET>();
            for (y = 0; y < pbCenario.Height; y++)
            {
                Aet.AddRange(EdgeTable[y]);
                Aet.RemoveAll(fa => fa.Ymax == y);
                Aet = Aet.OrderBy(fa => fa.Xmin).ToList();
                for (int j = 0; j < Aet.Count - 1; j += 2)
                {
                    double z = Aet[j].Zmin;

                    var incZ = (Aet[j + 1].Zmin - Aet[j].Zmin) / (Aet[j + 1].Xmin - Aet[j + 1].Xmin - Aet[j].Xmin);
                    for (var x = Aet[j].Xmin; x < Aet[j + 1].Xmin; x++)
                    {
                        if (z > zBuffer[(int)x, y])
                        {
                            ImagemCenario.SetPixel((int)x, y, cor);
                            zBuffer[(int)x, y] = z;
                        }
                        z += incZ;
                    }
                }
            }
            for (int i = 0; i < Aet.Count; i++)
            {
                var ponto = Aet[i];
                ponto.Xmin += ponto.IncX;
                ponto.Zmin += ponto.IncZ;
            }
        }

        private double[,] InicializaZbuffer()
        {
            var zBuffer = new double[pbCenario.Width, pbCenario.Height];
            for (int i = 0; i < pbCenario.Width; i++)
            {
                for (int j = 0; j < pbCenario.Height; j++)
                {
                    zBuffer[i, j] = -9999999;
                }
            }
            return zBuffer;
        }
        private void bAplicarTonalizacao_Click(object sender, EventArgs e)
        {
            var centroTela = new Point(pbCenario.Width / 2, pbCenario.Height / 2);
            var vetor = new Point(MousePosition.X - centroTela.X, MousePosition.Y - centroTela.Y);
            var norma = Math.Sqrt(Math.Pow(vetor.X, 2) + Math.Pow(vetor.Y, 2));
            vetorNormalSol = new Ponto3D(vetor.X / norma, vetor.Y / norma, 1); //Passamos 1 pois nao sabemio o z da luz

            if (rBFlat.Checked)
            {

                var zBuffer = InicializaZbuffer();
                atual.CalculaNormalFace();
                ImagemCenario = new Bitmap(pbCenario.Width, pbCenario.Height);

                for (int f = 0; f < atual.faces.Count; f++)
                {
                    var face = atual.faces[f];

                    if (face.normalFace.Z >= 0)
                    {
                        Preenchimento(face, zBuffer);
                        //ScanLine3D(PbCorAtual.BackColor, zBuffer, face);
                    }
                }

                pbCenario.Image = ImagemCenario;
            }
        }

        void Preenchimento(Face Poligono, double[,] ZBuffer)
        {
            var Max = new Vertice();
            var Min = new Vertice();
            ET no;

            List<ET>[] ET = new List<ET>[pbCenario.Height];
            for (int i = 0; i < ET.Length; i++)
                ET[i] = new List<ET>();

            var vertice1 = atual.verticesAtuais[Poligono.v1];
            var vertice2 = atual.verticesAtuais[Poligono.v2];
            var vertice3 = atual.verticesAtuais[Poligono.v3];

            var poligonoTriangular = new List<Vertice>();
            poligonoTriangular.Add(vertice1);
            poligonoTriangular.Add(vertice2);
            poligonoTriangular.Add(vertice3);

            //pega as aresta
            for (int i = 0; i < poligonoTriangular.Count - 1; i++)
            {
                Max = poligonoTriangular[i];
                Min = poligonoTriangular[i + 1];
                if (Min.y > Max.y)
                {
                    Max = poligonoTriangular[i + 1];
                    Min = poligonoTriangular[i];
                }								//  deltaX/deltaY
                no = new ET((int)Max.y, Min.x, (Max.x - Min.x) / (double)(Max.y - Min.y), Min.z, (Max.z - Min.z) / (double)(Max.y - Min.y));
                if (Min.y <= pbCenario.Height && Min.y >= 0)
                {
                    ET[(int)Min.y].Add(no);
                }
                else
                {
                    ET[pbCenario.Height - 1].Add(no);
                }
            }
            Max = poligonoTriangular[poligonoTriangular.Count - 1];
            Min = poligonoTriangular[0];
            if (Min.y > Max.y)
            {
                Max = poligonoTriangular[0];
                Min = poligonoTriangular[poligonoTriangular.Count - 1];
            }
            no = new ET((int)Max.y, Min.x, (Max.x - Min.x) / (double)(Max.y - Min.y), Min.z, (Max.z - Min.z) / (double)(Max.y - Min.y));
            if (Min.y <= pbCenario.Height && Min.y >= 0)
            {
                ET[(int)Min.y].Add(no);
            }
            else
            {
                ET[pbCenario.Height - 1].Add(no);
            }
            List<ET> AET = new List<ET>();

            for (int y = 0; y < pbCenario.Height; y++)
            {
                //adiciona na AET
                if (ET[y].Count != 0)
                    for (int i = 0; i < ET[y].Count; i++)
                        AET.Add(ET[y].ElementAt(i));

                //remove da AET os q Ymax for igual a y
                for (int i = 0; i < AET.Count; i++)
                    if (AET.ElementAt(i).Ymax == y)
                    {
                        AET.RemoveAt(i);
                        i--;
                    }

                //ordena em Xmin
                for (int i = 0; i < AET.Count - 1; i++)
                    for (int j = i + 1; j < AET.Count; j++)
                        if (AET[i].Xmin > AET[j].Xmin)
                        {
                            ET aux = new ET();
                            aux = AET[j];
                            AET[j] = AET[i];
                            AET[i] = aux;
                        }


                for (int i = 0; i < AET.Count - 1; i += 2)
                {
                    double Z = AET.ElementAt(i).Zmin;
                 
                    double incZ = (AET.ElementAt(i + 1).Zmin - AET.ElementAt(i).Zmin) / (AET.ElementAt(i + 1).Xmin - AET.ElementAt(i).Xmin);
                    for (double x = AET.ElementAt(i).Xmin; x < AET.ElementAt(i + 1).Xmin && x >= 0 && x <= pbCenario.Width; x++)
                    {
                        if (Z > ZBuffer[(int)x, y])
                        {
                            if (cBHabilitarIluminacao.Checked)
                            {
                                var n = Convert.ToDouble(nUPDEspecularidade.Value);
                                var Kdif = new Ponto3D(0, 0, 0);
                                var Kesp = new Ponto3D(0, 0, 0);
                                var Kamb = new Ponto3D(0, 0, 0);

                                if (cBDifusa.Checked)
                                {
                                    Kdif = new Ponto3D(Convert.ToDouble(nUDKdR.Value), Convert.ToDouble(nUDKdG.Value), Convert.ToDouble(nUDKdB.Value));
                                }

                                if (cBEspecular.Checked)
                                {
                                    Kesp = new Ponto3D(Convert.ToDouble(nUDKeR.Value), Convert.ToDouble(nUDKeG.Value), Convert.ToDouble(nUDKeB.Value));
                                }
                                

                                if (cBAmbiente.Checked)
                                {
                                    Kamb = new Ponto3D(Convert.ToDouble(nUDKaR.Value), Convert.ToDouble(nUDKaG.Value), Convert.ToDouble(nUDKaB.Value));
                                }
                                

                                var Ldif = new Ponto3D(Convert.ToDouble(nUDLdR.Value), Convert.ToDouble(nUDLdG.Value), Convert.ToDouble(nUDLdB.Value));
                                var Lesp = new Ponto3D(Convert.ToDouble(nUDLeR.Value), Convert.ToDouble(nUDLeG.Value), Convert.ToDouble(nUDLeB.Value));
                                var Lamb = new Ponto3D(Convert.ToDouble(nUDLaR.Value), Convert.ToDouble(nUDLaG.Value), Convert.ToDouble(nUDLaB.Value));

                                ImagemCenario.SetPixel((int)x, y, Flat(n, Poligono.normalFace, Kdif, Kesp, Kamb, Ldif, Lesp, Lamb));
                            }
                            else
                            {
                                ////img.SetPixel((int)x, y, Phong(N, ptbDifusa.BackColor, Color.White, ptbAmbiente.BackColor, ptbDifusa.BackColor, ptbEspectral.BackColor));
                                //else
                                ImagemCenario.SetPixel((int)x, y, PbCorAtual.BackColor);
                            }
                            ZBuffer[(int)x, y] = Z;
                        }
                        Z += incZ;
                    }
                }

                //incrementa a AET Xmin+=incX
                for (int i = 0; i < AET.Count; i++)
                {
                    AET[i].Xmin += AET[i].IncX;
                    AET[i].Zmin += AET[i].IncZ;
                }
            }
        }
        public double ProdutoEscalar(Ponto3D vetor1, Ponto3D vetor2)
        {
            return vetor2.X * vetor1.X + vetor2.Y * vetor1.Y + vetor2.Z * vetor1.Z;
        }

        public Color Flat(double N, Ponto3D NormFace, Ponto3D Kdif, Ponto3D Kesp, Ponto3D Kamb, Ponto3D Ldif, Ponto3D Lesp, Ponto3D Lamb)
        {
            var Ka = new Ponto3D();
            var Kd = new Ponto3D(); //Cor do objeto
            var Ks = new Ponto3D(); //Cor do brilho
            var Ia = new Ponto3D(); //Luz ambiente
            var Id = new Ponto3D();//Luz difusa
            var Is = new Ponto3D(); //Luz espectral
            Ka = Kamb;
            Kd = Kdif;
            Ks = Kesp;
            Ia = Lamb;
            Id = Ldif;
            Is = Lesp;

            double NL = ProdutoEscalar(vetorNormalSol, NormFace);
            if(NL > 0)
            {
                Id = new Ponto3D(Id.X * Kd.X * NL, Id.Y * Kd.Y * NL, Id.Z * Kd.Z * NL);
            }
            else
            {
                Id = new Ponto3D();
            }

            //Calcula LAmbiente
            Ia = new Ponto3D(Ia.X * Ka.X, Ia.Y * Ka.Y, Ia.Z * Ka.Z); //X = R, Y = G, Z = B

            //Calcula Iespecular
            Ponto3D V = new Ponto3D(0, 0, 1);
            //R=2N<N,L>-L
            Ponto3D R = new Ponto3D(2 * NormFace.X * NL - vetorNormalSol.X, 2 * NormFace.Y * NL - vetorNormalSol.Y, 2 * NormFace.Z * NL - vetorNormalSol.Z);

            //vetor R normalizado R/|R|
            R = new Ponto3D(R.X / Math.Sqrt(Math.Pow(R.X, 2) + Math.Pow(R.Y, 2) + Math.Pow(R.Z, 2)), R.Y / Math.Sqrt(Math.Pow(R.X, 2) + Math.Pow(R.Y, 2) + Math.Pow(R.Z, 2)), R.Z / Math.Sqrt(Math.Pow(R.X, 2) + Math.Pow(R.Y, 2) + Math.Pow(R.Z, 2)));

            double VR = ProdutoEscalar(R, V);

            if (VR > 0)
            {
                VR = Math.Pow(VR, Convert.ToDouble(N));
                Is = new Ponto3D(Ks.X * Id.X * VR, Ks.Y * Id.Y * VR, Ks.Z * Id.Z * VR);
            }
            else
                Is = new Ponto3D();

            Ponto3D Flat = new Ponto3D(Ia.X + Id.X + Is.X, Ia.Y + Id.Y + Is.Y, Ia.Z + Id.Z + Is.Z);

            if (Flat.X > 1)
            {
                Flat.X = 1;
            }
            else
                if (Flat.X < 0)
            {
                Flat.X = 0;
            }
            if (Flat.Y > 1)
            {
                Flat.Y = 1;
            }
            else
                if (Flat.Y < 0)
            {
                Flat.Y = 0;
            }
            if (Flat.Z > 1)
            {
                Flat.Z = 1;
            }
            else
                if (Flat.Z < 0)
            {
                Flat.Z = 0;
            }
            return Color.FromArgb((int)(Flat.X * 255), (int)(Flat.Y * 255), (int)(Flat.Z * 255));
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ImagemCenario = new Bitmap(pbCenario.Width, pbCenario.Height);
            pbCenario.Image = ImagemCenario;
            DesenhaModelo3D(Color.White);
            pbCenario.Image = ImagemCenario;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.L)
            {
                movimentaFonteLuz = rBMoverFonteLuz.Checked;
                rBMoverFonteLuz.Checked = !movimentaFonteLuz;
                rBFixar.Checked = movimentaFonteLuz;
            }
        }

        private void pbCenario_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.L)
            {
                movimentaFonteLuz = rBMoverFonteLuz.Checked;
                rBMoverFonteLuz.Checked = !movimentaFonteLuz;
            }
        }

        private void rBMoverFonteLuz_CheckedChanged(object sender, EventArgs e)
        {
            var centroTela = new Point(pbCenario.Width / 2, pbCenario.Height / 2);
            var vetor = new Point(MousePosition.X - centroTela.X, MousePosition.Y - centroTela.Y);
            var norma = Math.Sqrt(Math.Pow(vetor.X, 2) + Math.Pow(vetor.Y, 2));
            vetorNormalSol = new Ponto3D((int)(vetor.X / norma), (int)(vetor.Y / norma), 0);
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            var centroTela = new Point(pbCenario.Width / 2, pbCenario.Height / 2);
            var norma = Math.Sqrt(Math.Pow(MousePosition.X, 2) + Math.Pow(MousePosition.Y, 2));
            vetorNormalSol = new Ponto3D((int)(MousePosition.X / norma), (int)(MousePosition.Y / norma), 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string filename = dlg.FileName;
                    var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    var objeto = new Modelo3D();
                    objeto.nome = dlg.FileName;
                    objeto.arquivo = fileStream;
                    objeto.matrizAcumulada = MatrizIdentidade();
                    ExtrairInformacoesObjeto3D(objeto);
                    atual = objeto;
                    DesenhaModelo3D(Color.White);
                    pbCenario.Image = ImagemCenario;
                }
            }
        }

        private void RBRotacaoZ_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void PbCenario_Click(object sender, EventArgs e)
        {

        }
    }
}
