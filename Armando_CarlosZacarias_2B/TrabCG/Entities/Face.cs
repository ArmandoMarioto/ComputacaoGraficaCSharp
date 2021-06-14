using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace TrabalhoCG.Entities
{
    public class Face
    {
        public int index { get; set; }
        public int v1 { get; set; }
        public int v2 { get; set; }
        public int v3 { get; set; }
        public Ponto3D normalFace { get; set; }


        //public List<Vertice> vertices { get; set; }
        //public List<Vertice> verticesAtuais { get; set; }
        //public List<Vertice> verticesOriginais { get; set; }
        //public List<Vertice> vetoresNormais { get; set; }

        public Face()
        {
            normalFace = new Ponto3D(0, 0, 0);
        }
      
    }
}
