using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EntryPoint
{
    class Vertice
    {
        public Vector2 Vector1 { get; set; }
        public double Distance { get; set; }
        public Vertice PreviousVertice { get; set; }


        public Vertice(Vector2 vector1)
        {
            this.Vector1 = vector1;
        }
    }
}
