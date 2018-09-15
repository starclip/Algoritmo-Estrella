using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoLista
{
    class Tablero
    {
        private int filas;
        private int columnas;
        private int costoDiagonal;
        private int costoLateral;
        private int numeroFichas;
        private Meta meta;
        private Jugador jugador;
        private Enemigo[] enemigos;
        private List<Ficha> rutaOptima;
        private Ficha[,] tablero;

        /* Constructor tablero. Se construye a base de N filas y N columnas. */
        public Tablero(int filas, int columnas, int distancia)
        {
            this.filas = filas;
            this.columnas = columnas;
            tablero = new Ficha[filas, columnas];
            this.numeroFichas = filas * columnas;
            this.rutaOptima = new List<Ficha>();
            this.costoLateral = distancia;
            this.costoDiagonal = (int)Math.Sqrt(Math.Pow(distancia, 2) + Math.Pow(distancia, 2));
        }


        /* *******************************************************
         * Se selecciona la meta a seguir.
         * No se deben haber generado enemigos.
         *********************************************************/
        public void seleccionarMeta()
        {
            Ficha fichaTemp = obtenerficha();
            // Si la ficha de la meta no está en el mismo lugar que el jugador.
            while (fichaTemp.compararFicha(this.jugador))
            {
                fichaTemp = obtenerficha();
            }
            this.meta = new Meta(fichaTemp.getX(), fichaTemp.getY());
        }

        /* Obtenga el costo lateral total. */
        public int getCostoLateral()
        {
            return this.costoLateral;
        }

        /* Obtenga el costo lateral diagonal. */
        public int getCostoDiagonal()
        {
            return this.costoDiagonal;
        }

        /* *******************************************************
         * Se selecciona el jugador por primera vez.
         * No se deben haber generado enemigos.
         *********************************************************/
        public void seleccionarInicio()
        {
            Ficha fichaTemp = obtenerficha();
            // Si la ficha del jugador no está en el mismo lugar que la meta.
            while (fichaTemp.compararFicha(this.meta))
            {
                fichaTemp = obtenerficha();
            }
            this.jugador = new Jugador("Temp", fichaTemp.getX(), fichaTemp.getY());
        }

        /* *******************************************************
         * Se seleccionan las posiciones para asignar los enemigos.
         * Se deben haber seleccionado previamente la meta y el inicio.
         *********************************************************/
        public void seleccionarEnemigos(int numeroEnemigos)
        {
            this.enemigos = new Enemigo[numeroEnemigos];

            // Creo N número de enemigos.
            for (int i = 0; i < numeroEnemigos; i++)
            {
                Ficha fichaTemp = obtenerficha();
                // La ficha no puede ser la meta o el jugador.
                while (fichaTemp.compararFicha(this.meta) || fichaTemp.compararFicha(this.jugador)
                    || fichaTemp.compararMuchasFichas(this.enemigos))
                {
                    fichaTemp = obtenerficha();
                }
                this.enemigos[i] = new Enemigo(fichaTemp.getX(), fichaTemp.getY()); // Agregué el nuevo enemigo.
            }
        }

        /* Se crea el tablero. */
        public void crearTablero()
        {
            for (int i = 0; i < this.filas; i++)
            {
                for (int j = 0; j < this.columnas; j++)
                {
                    tablero[i, j] = new Blanco(i, j);
                }
            }
        }

        /* Obtengo una ficha del tablero. */
        public Ficha getFicha(int x, int y)
        {
            return this.tablero[x, y];
        }

        /* Obtener la meta */
        public Ficha getMeta()
        {
            return this.meta;
        }

        /* Obtener el jugador */
        public Ficha getJugador()
        {
            return this.jugador;
        }

        /* Obtener los enemigos */
        public Ficha[] getEnemigos()
        {
            return this.enemigos;
        }

        /* Configure la ruta más óptima para el tablero. */
        public void setRuta(List<Ficha> ruta)
        {
            this.rutaOptima = ruta;
        }

        /* Obtengo una ficha al azar del tablero. */
        public Ficha obtenerficha()
        {
            Random r = new Random();
            int x = r.Next(0, this.filas);
            int y = r.Next(0, this.columnas);
            Ficha fichaSeleccionada = tablero[x, y];
            return fichaSeleccionada;
        }

        /* Imprimir el tablero de juego. */

        public void printTablero()
        {
            Console.WriteLine(": Tablero : ");
            for (int i = 0; i < this.filas; i++)
            {
                for (int j = 0; j < this.columnas; j++)
                {
                    Console.Out.Write(" - ");
                    if (tablero[i, j].compararFicha(this.jugador))
                    {
                        Console.Out.Write("J");
                    }
                    else if (tablero[i, j].compararFicha(this.meta))
                    {
                        Console.Out.Write("M");
                    }
                    else if (tablero[i, j].compararMuchasFichas(this.enemigos))
                    {
                        Console.Out.Write("E");
                    }
                    else if (tablero[i, j].compararMuchasFichas(this.rutaOptima.ToArray()))
                    {
                        Console.Out.Write("*");
                    }
                    else
                    {
                        Console.Out.Write(tablero[i, j].getCostoTotal());
                    }
                }
                Console.Out.WriteLine();
            }
            Console.Out.WriteLine();
        }

    }
}
