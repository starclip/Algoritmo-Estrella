using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoLista
{
    abstract class Ficha
    {
        protected int x, y;
        protected int costoTotal;
        protected Ficha previous;

        public Ficha(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /* Asigne el valor previo. */
        public void setPrevious(Ficha previous)
        {
            this.previous = previous;
        }

        /* Obtenga el valor previo. */
        public Ficha getPrevious()
        {
            return this.previous;
        }

        /* Obtenga la coordenada X */
        public int getX()
        {
            return x;
        }

        /* Obtenga la coordenada Y */
        public int getY()
        {
            return y;
        }

        /* Compare si las coordenadas entre las fichas son equivalentes. */
        public Boolean compararFicha(Ficha f)
        {
            if (f == null)
            {
                return false;
            }

            if (this.x == f.getX() && this.y == f.getY())
            {
                return true;
            }
            return false;
        }

        /* Compare las coordenadas de la ficha con los enemigos. */
        public Boolean compararMuchasFichas(Ficha[] fichas)
        {
            // Por cada enemigo o bloque en el campo.
            foreach(Ficha littleFicha in fichas)
            {
                if (littleFicha == null)
                {
                    return false; // No está en una casilla de un enemigo.
                }

                if (this.x == littleFicha.getX() && this.y == littleFicha.getY())
                {
                    return true; // Es un enemigo.
                }
            }

            return false; // No está en una casilla de un enemigo.
        }

        /* Dependiendo del tipo de ficha ... retorna un costo total. */
        public abstract int getCostoTotal();
    }
}
