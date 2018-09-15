using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoLista
{
    class Heap
    {
        private List<Blanco> listaBuscados;
        private int cambiosRealizados;

        public Heap()
        {
            this.listaBuscados = new List<Blanco>();
            cambiosRealizados = 0;
        }

        /* Inserta una ficha dependiendo de su costo (menores primero). */
        private void insertElement(Blanco ficha)
        {
            for (int i = 0; i < this.listaBuscados.Count; i++)
            {
                Blanco fichaActual = this.listaBuscados[i];
                // Si la nueva ficha tiene un costo menor.
                if (fichaActual.getCostoCompleto() >= ficha.getCostoCompleto())
                {
                    this.listaBuscados.Insert(i, ficha);
                    return;
                }
            }

            this.listaBuscados.Add(ficha); // Agreguela al final.
        }

        /* Inseta un elemento, primero revise si ya se insertó en la lista de búsqueda previamente. */
        public void insert(Blanco ficha)
        {
            for (int i = 0; i < this.listaBuscados.Count; i++)
            {
                Blanco fichaTemp = listaBuscados[i];
                // Si las dos fichas tienen la misma posición.
                if (ficha.compararFicha(fichaTemp)){
                    // Si su costo es menor a la previa.
                    if (ficha.getCostoCompleto() < fichaTemp.getCostoCompleto())
                    {
                        cambiosRealizados++;
                        listaBuscados.Remove(fichaTemp);
                        this.insertElement(ficha); // Inserte si su costo es menor.
                    }
                    return;
                }
            }
            this.insertElement(ficha); // Inserte si no tiene coincidencias.
        }

        public int getCambios()
        {
            return this.cambiosRealizados;
        }

        /* Obtenga la ficha en su posición 0. */
        public Blanco getFicha()
        {
            if (this.listaBuscados.Count > 0)
            {
                Blanco ficha = this.listaBuscados[0];
                this.listaBuscados.Remove(ficha);
                return ficha;
            }
            return null;
        }

        /* Obtenga el número de evaluaciones que realizó el algoritmo. */
        public int getCantidad()
        {
            return this.listaBuscados.Count;
        }

        /* Imprima la lista de búsqueda. */
        public void print()
        {
            Console.Out.WriteLine("Lista de buscados: ");
            foreach (Blanco f in this.listaBuscados)
            {
                Console.Out.Write(" ( " + f.getX() + "," + f.getY() + " ) = " + f.getCostoTotal());
            }
            Console.Out.WriteLine();
        }
    }
}
