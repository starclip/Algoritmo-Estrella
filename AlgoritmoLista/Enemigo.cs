using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoLista
{
    class Enemigo : Ficha
    {
        public Enemigo(int x, int y) : base(x, y)
        {
            this.costoTotal = -1; // La ficha enemigo tiene un costo de -1;
        }

        public override int getCostoTotal()
        {
            return costoTotal;
        }
    }
}
