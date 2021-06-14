using TrabalhoCG.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoCG.ProcessamentoImagem 
{
    public class Modelo3D
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public FileStream arquivo { get; set; }
        public List<Face> faces { get; set; }
        public double[,] matrizAcumulada { get; set; }
        public Dictionary<int, Vertice> verticesAtuais { get; set; }
        public Dictionary<int, Vertice> verticesOriginais { get; set; }

        public Modelo3D()
        {
            matrizAcumulada = new double[4, 4];
            verticesAtuais = new Dictionary<int, Vertice>();
            verticesOriginais = new Dictionary<int, Vertice>();
        }

        public void CalculaNormalFace()
        {
            foreach (var face in faces)
            {
                var ponto1 = verticesAtuais[face.v1];
                var ponto2 = verticesAtuais[face.v2];
                var ponto3 = verticesAtuais[face.v3];

                var mat = new double[2, 3];
                //AB = B - A
                mat[0, 0] = ponto2.x - ponto1.x;
                mat[0, 1] = ponto2.y - ponto1.y;
                mat[0, 2] = ponto2.z - ponto1.z;

                //AC = C - A
                mat[1, 0] = ponto3.x - ponto1.x;
                mat[1, 1] = ponto3.y - ponto1.y;
                mat[1, 2] = ponto3.z - ponto1.z;

                face.normalFace = new Ponto3D(mat[0, 1] * mat[1, 2] - mat[0, 2] * mat[1, 1], mat[0, 2] * mat[1, 0] - mat[0, 0] * mat[1, 2], mat[0, 0] * mat[1, 1] - mat[0, 1] * mat[1, 0]);
                var normal = Math.Sqrt(Math.Pow(face.normalFace.X, 2) + Math.Pow(face.normalFace.Y, 2) + Math.Pow(face.normalFace.Z, 2));

                face.normalFace.X /= normal;
                face.normalFace.Y /= normal;
                face.normalFace.Z /= normal;
            }
        }
    }
}


