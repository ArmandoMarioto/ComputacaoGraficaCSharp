using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoLinhas
{
    class Equacoes
    {
        private int cx1, cx2, cy1, cy2;
        private double deltaX, deltaY, m;
        private int raio, heig, widt;

        public Equacoes(int raio, int x, int y)
        {
            this.raio = raio;
            cx1 = x;
            cy1 = y;
        }

        public Equacoes(int cx1, int cy1, int cx2, int cy2)
        {
            this.cx1 = cx1;
            this.cx2 = cx2;
            this.cy1 = cy1;
            this.cy2 = cy2;
            deltaX = cx2 - cx1;
            deltaY = cy2 - cy1;
            m = deltaY / deltaX;
        }

        public void desenharEquacExplicita(Bitmap bitmap)
        {
            int y;
            int cx = cx1, cy = cy1;
            heig = bitmap.Height;
            widt = bitmap.Width;

            for(int x = 0; x < raio; x++, cx++)
            {
                y = (int)Math.Sqrt(Math.Pow(raio, 2) - Math.Pow(x, 2));
                pontosCircunferencia(x, y, bitmap);
            }
        }

        void ElipsePoints(int x, int y, Bitmap bitmap)
        {
            if (x + cx2 > 0 && x + cx2 < widt && y + cy2 > 0 && y + cy2 < heig)
                bitmap.SetPixel(x + cx2, y + cy2, Color.Black);

            if ((x * -1) + cx2 > 0 && (x * -1) + cx2 < widt && y + cy2 > 0 && y + cy2 < heig)
                bitmap.SetPixel((x * -1) + cx2, y + cy2, Color.Black);

            if (x + cx2 > 0 && x + cx2 < widt && (y * -1) + cy2 > 0 && (y * -1) + cy2 < heig)
                bitmap.SetPixel(x + cx2, (y * -1) + cy2, Color.Black);

            if ((x * -1) + cx2 > 0 && (x * -1) + cx2 < widt && (y * -1) + cy2 > 0 && (y * -1) + cy2 < heig)
                bitmap.SetPixel((x * -1) + cx2, (y * -1) + cy2, Color.Black);
        }

        public void desenharElipse(Bitmap bitmap)
        {
            int a = cx1, b = cy1;
            int x, y;
            double d1, d2;

            heig = bitmap.Height;
            widt = bitmap.Width;


            x = 0;
            y = b;
            d1 = b * b - a * a * b + a * a / 4.0;
            ElipsePoints(x, y, bitmap);
            while (a * a * (y - 0.5) > b * b * (x + 1))
            {
                if (d1 < 0)
                {
                    d1 = d1 + b * b * (2 * x + 3);
                    x++;
                }
                else
                {
                    d1 = d1 + b * b * (2 * x + 3) + a * a * (-2 * y + 2);
                    x++;
                    y--;
                }
                ElipsePoints(x, y, bitmap);
            }

            d2 = b * b * (x + 0.5) * (x + 0.5) + a * a * (y - 1) * (y - 1) - a * a * b * b;
            while (y > 0)
            {
                if (d2 < 0)
                {
                    d2 = d2 + b * b * (2 * x + 2) + a * a * (-2 * y + 3);
                    x++;
                    y--;
                }
                else
                {
                    d2 = d2 + a * a * (-2 * y + 3);
                    y--;
                }
                ElipsePoints(x, y, bitmap);
            }
        }
        public void desenharPontoMedio(Bitmap bitmap)
        {
            int x = 0, y = raio;
            double d = 1 - raio;

            heig = bitmap.Height;
            widt = bitmap.Width;

            pontosCircunferencia(x, y, bitmap);
            while(y > x)
            {
                if (d < 0)
                    d += 2 * x + 3;
                else
                {
                    d += 2 * (x - y) + 5;
                    y--;
                }
                x++;
                pontosCircunferencia(x, y, bitmap);
            }
        }

        private void pontosCircunferencia(int x, int y, Bitmap bitmap)
        {
            
            if(x+cx1 >= 0 && x+cx1 < widt && y + cy1 >= 0 && y + cy1 < heig) 
                bitmap.SetPixel(x + cx1, y + cy1, Color.Black);             //(x, y)

            if(y + cx1 >= 0 && y + cx1 < widt && x + cy1 >= 0 && x + cy1 < heig)
                bitmap.SetPixel(y + cx1, x + cy1, Color.Black);             //(y, x)
            
            if ((y * -1) + cx1 >= 0 && (y * -1) + cx1 < widt && x + cy1 >= 0 && x + cy1 < heig)
                bitmap.SetPixel((y * -1) + cx1, x + cy1, Color.Black);      //(-y, x)

            if ((x * -1) + cx1 >= 0 && (x * -1) + cx1 < widt && y + cy1 >= 0 && y + cy1 < heig)
                bitmap.SetPixel((x * -1) + cx1, y + cy1, Color.Black);      //(-x, y)

            if (x + cx1 >= 0 && x + cx1 < widt && (y * -1) + cy1 >= 0 && (y * -1) + cy1 < heig)
                bitmap.SetPixel(x + cx1, (y*-1) + cy1, Color.Black);        //(x, -y)

            if (y + cx1 >= 0 && y + cx1 < widt && (x * -1) + cy1 >= 0 && (x * -1) + cy1 < heig)
                bitmap.SetPixel(y + cx1, (x * -1) + cy1, Color.Black);      //(y, -x)

            if ((x * -1) + cx1 >= 0 && (x * -1) + cx1 < widt && (y * -1) + cy1 >= 0 && (y * -1) + cy1 < heig)
                bitmap.SetPixel((x * -1) + cx1, (y * -1) + cy1, Color.Black);//(-x, -y)

            if ((y * -1) + cx1 >= 0 && (y * -1) + cx1 < widt && (x * -1) + cy1 >= 0 && (x * -1) + cy1 < heig)
                bitmap.SetPixel((y * -1) + cx1, (x * -1) + cy1, Color.Black);//(-y, -x)
                
        }

        public void desenharERR(Bitmap bitmap)
        {
            int signal;
            if (Math.Abs(deltaX) > Math.Abs(deltaY))
            {
                double y;
                signal = Math.Sign(deltaX);
                for (int x = cx1; x != cx2; x += signal)
                {
                    y = cy1 + m * (x - cx1);
                    bitmap.SetPixel(x, (int)Math.Round(y), Color.FromArgb(0, 0, 0));
                }
            }
            else
            {
                double x;
                signal = Math.Sign(deltaY);
                for (int y = cy1; y != cy2; y += signal)
                {
                    x = cx1 + (y - cy1) / m;
                    bitmap.SetPixel((int)Math.Round(x), y, Color.FromArgb(0, 0, 0));
                }
            }
        }

        public void desenharDDA(Bitmap bitmap)
        {
            int Length, I;
            double X, Y, Xinc, Yinc, cont;

            Length = Math.Abs(cx2 - cx1);

            if (Math.Abs(cy2 - cy1) > Length)
                Length = Math.Abs(cy2 - cy1);

            Xinc = (double)(cx2 - cx1) / Length;
            Yinc = (double)(cy2 - cy1) / Length;
            
            X = cx1;
            Y = cy1;
            cont = 0;

            while (cont < Length)
            {
                if((int)Math.Round(X) > 0 && (int)Math.Round(X) < bitmap.Width && (int)Math.Round(Y) > 0 && (int)Math.Round(Y) < bitmap.Height)
                    bitmap.SetPixel((int)Math.Round(X), (int)Math.Round(Y), Color.FromArgb(0, 0, 0));
                X = X + Xinc;
                Y = Y + Yinc;
                cont ++;
            }
            
        }

        void bresenham1(int x1, int y1, int x2, int y2, Bitmap bmp)
        {
            int declive = 1;
            int dx, dy, incE, incNE, d, x, y;
            dx = x2 - x1;
            dy = y2 - y1;

            if(Math.Abs(dx) > Math.Abs(dy))
            {
                if (x2 < x1)
                {
                    bresenham1(x2, y2, x1, y1, bmp);
                    return;
                }

                if(y2 < y1)
                {
                    dy = -dy;
                    declive = -1;
                }
                incE = 2 * dy;
                incNE = 2 * dy - 2 * dx;
                d = 2 * dy - dx;
                y = y1;
                for (x = x1; x <= x2; x++)
                {
                    if(x > 0 && x < bmp.Width && y > 0 && y < bmp.Height)
                        bmp.SetPixel(x, y, Color.Black);
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
                    bresenham1(x2, y2, x1, y1, bmp);
                    return;
                }

                if (x2 < x1)
                {
                    dx = -dx;
                    declive = -1;
                }
                incE = 2 * dx;
                incNE = 2 * dx - 2 * dy;
                d = 2 * dx - dy;
                x = x1;

                for (y = y1; y <= y2; y++)
                {
                    if (x > 0 && x < bmp.Width && y > 0 && y < bmp.Height)
                        bmp.SetPixel(x, y, Color.Black);
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


        public void desenharRetasRapidas(Bitmap bitmap)
        {
            bresenham1(cx1, cy1, cx2, cy2, bitmap);
        }

        public void bresenhaCalc(Bitmap bitmap, Boolean movX)
        {
            if(!movX)
            {
                int aux = cx1;
                cx1 = cy1;
                cy1 = aux;

                aux = cx2;
                cx2 = cy2;
                cy2 = aux;
            }

            if (cx2 < cx1)
            {
                int aux = cx1;
                cx1 = cx2;
                cx2 = aux;

                aux = cy1;
                cy1 = cy2;
                cy2 = aux;
            }

            if (cy2 < cy1)
            {
                cy1 = -cy1;
                cy2 = -cy2;
            }

            int g = -1;

            int dx = cx2 - cx1;
            int dy = cy2 - cy1;

            int incE = 2 * dy;
            int incNE = 2 * (dy - dx);

            int d = incE - dx;
            int y = cy1;

            for (int x = cx1; x < cx2; x++)
            {

                if (y < 0)
                    g = -y;
                else
                    g = y;

              
                    bitmap.SetPixel(x, g, Color.Black);
               

                if (d > 0)
                {
                    d += incNE;
                    y++;
                }
                else
                {
                    d += incE;
                }
            }
        }

    }
}
