using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoLista
{
    class Blanco : Ficha
    {
        private int costoManhattan;

        // X1 - Y1 son sus respectivas posiciones.
        public Blanco(int x, int y) : base(x, y)
        {
            costoManhattan = 0;
        }

        /* Constructor para almacenar la ficha previa. */
        public Blanco(int x, int y, Ficha previous) : base(x, y)
        {
            costoManhattan = 0;
            this.previous = previous; // Defino el valor previo.
            this.costoTotal += previous.getCostoTotal();
        }

        /* Se configura el costo de Manhattan basado con respecto al punto final. */
        public void setCostoManhattan(Ficha puntoFinal)
        {
            int cost1 = Math.Abs(puntoFinal.getX() - this.x);
            int cost2 = Math.Abs(puntoFinal.getY() - this.y);
            this.costoManhattan = cost1 + cost2;
        }

        /* Obtener el costo de manhattan. */
        public int getCostoManhattan()
        {
            return this.costoManhattan;
        }

        /* Avance en una diagonal. Aumente el valor de la diagonal. */
        public void advanceDiagonal(int costoDiagonal)
        {
            this.costoTotal += costoDiagonal;
        }

        /* Avance en una lateral. Aumente el valor de la lateral. */
        public void advanceNormal(int costoLateral)
        {
            this.costoTotal += costoLateral;
        }

        /* Obtener el costo total. f(n) = g(n) + h(n) */
        public override int getCostoTotal()
        {
            return this.costoTotal;
        }

        public int getCostoCompleto()
        {
            return this.costoTotal + this.costoManhattan;
        }
    }
}
