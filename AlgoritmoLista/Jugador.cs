using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoLista
{
    class Jugador : Ficha
    {
        String name;
        public Jugador(String name, int x, int y) : base(x, y)
        {
            this.name = name;
            this.costoTotal = 0; // El costo es equivalente al lugar de ubicación del jugador.
        }

        public override int getCostoTotal()
        {
            return costoTotal;
        }
    }
}
