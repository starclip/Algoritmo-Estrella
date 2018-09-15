using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoLista
{
    class Meta : Ficha
    {
        public Meta(int x, int y): base(x, y)
        {
            this.costoTotal = 0; // La meta no ofrece ninguna ventaja.
        }

        /* Obtenga el costo total que se obtiene de la meta. */
        public override int getCostoTotal()
        {
            return this.costoTotal;
        }
    }
}
