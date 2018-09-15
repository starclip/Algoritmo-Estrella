using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoLista
{
    class AEstrella
    {
        private int rows;
        private int cols;
        private int size;
        private TimeSpan tiempoAlgoritmo;
        private Tablero campoJuego;
        private Heap listaBuscados;
        private List<Ficha> listaInsertados;
        private List<Ficha> listaCompletados;
        private int distanciaRecorrida;

        public AEstrella(int m, int n, Tablero campoJuego)
        {
            this.campoJuego = campoJuego; // Tablero
            this.listaBuscados = new Heap();
            this.listaCompletados = new List<Ficha>();
            this.listaInsertados = new List<Ficha>();
            this.size = m * n;
            this.rows = m;
            this.cols = n;
            this.distanciaRecorrida = 0;
        }

        /* Revise si ya había hecho ese movimiento previamente. */
        public Boolean compararPrevios(Ficha posActual, Ficha nuevoHijo)
        {
            Ficha temp = posActual.getPrevious();
            while(temp != null)
            {
                if (temp.compararFicha(nuevoHijo))
                {
                    return true;
                }
                temp = temp.getPrevious();
            }
            return false;
        }

        /* Compare si llegue a la meta. */
        public Boolean compararMeta(int x, int y)
        {
            Ficha meta = this.campoJuego.getMeta();
            Ficha fichaTemp = this.campoJuego.getFicha(x, y);
            // Si es la meta.
            if (fichaTemp.compararFicha(meta))
            {
                return true;
            }
            return false;
        }

        /* Mueve la ficha dependiendo de las posiciones X y Y. */
        public void moverFicha(int posX, int posY, Ficha posActual, Blanco nuevaFicha)
        {
            // Si es diagonal me muevo en diagonal. En caso contrario me muevo en línea Recta.
            if (posX == posActual.getX() || posY == posActual.getY())
            {
                nuevaFicha.advanceNormal(campoJuego.getCostoLateral());
            }
            else
            {
                nuevaFicha.advanceDiagonal(campoJuego.getCostoDiagonal());
            }
        }

        /* Compare si es un movimiento válido. */
        public Boolean movimientoValido(Ficha posActual, int x, int y)
        {
            Ficha[] enemigos = this.campoJuego.getEnemigos();
            Ficha fichaTemp = this.campoJuego.getFicha(x, y);
            // Si es un enemigo.
            if (fichaTemp.compararMuchasFichas(enemigos))
            {
                return false; // No es válido
            }

            if (fichaTemp.compararMuchasFichas(listaInsertados.ToArray()))
            {
                return false; // No es válido.
            }

            // Si ya lo había insertado previamente.
            if (compararPrevios(posActual, fichaTemp))
            {
                return false; // No es válido.
            }

            return true; // Movimiento válido.
        }

        /* *******************************************************
         * Obtengo todos los posibles movimientos de una ficha.
         * Se recibe la ficha a la que desea saber los posibles movimientos.
         * Solo se permiten movimientos laterales (arriba, abajo, derecha e izquierda).
         * Agrega a la lista de buscados.
         *********************************************************/
        public void obtenerAliadosLaterales(Ficha posActual)
        {
            Ficha meta = this.campoJuego.getMeta();
            int x = posActual.getX();
            int y = posActual.getY();
            int[,] positions = new int[4, 2] { 
                { x-1, y}, 
                { x+1, y }, 
                { x, y-1 }, 
                { x, y+1 } };
               
            // Realice cuatro veces por las diagonales de las fichas.
            for(int pos = 0; pos < 4; pos++)
            {
                int posX = positions[pos, 0];
                int posY = positions[pos, 1];

                // Si no se sale del tablero y además no se evalua la misma ficha.
                if ((posX >= 0 && posX < this.rows) && (posY >= 0 && posY < this.cols) &&
                    (posActual.getX() != posX || posActual.getY() != posY))
                {
                    // Compare tablero[i, j];
                    if (movimientoValido(posActual, posX, posY))
                    {
                        Blanco nuevaFicha = new Blanco(posX, posY, posActual);
                        nuevaFicha.setCostoManhattan(meta);
                        // Si es diagonal me muevo en diagonal. En caso contrario me muevo en línea Recta.
                        moverFicha(posX, posY, posActual, nuevaFicha);
                        listaBuscados.insert(nuevaFicha); // Se inserta la nueva ficha.
                    }
                }
            }
        }

        /* *******************************************************
         * Obtengo todos los posibles movimientos de una ficha.
         * Se recibe la ficha a la que desea saber los posibles movimientos.
         * Se permiten movimientos laterales y diagonales.
         * Agrega a la lista de buscados.
         *********************************************************/
        public void obtenerAliadosDiagonal(Ficha posActual)
        {
            Ficha meta = this.campoJuego.getMeta();
            int x = posActual.getX() - 1;
            int y = posActual.getY() - 1;

            for(int posX = x; posX < (x + 3); posX++)
            {
                for(int posY = y; posY < (y + 3); posY++)
                {
                    // Si no se sale del tablero y además no se evalua la misma ficha.
                    if ((posX >= 0 && posX < this.rows) && (posY >= 0 && posY < this.cols) && 
                        (posActual.getX() != posX || posActual.getY() != posY))
                    {
                        // Si es un movimiento válido (No es un enemigo y no lo había hecho previamente).
                        if (movimientoValido(posActual, posX, posY))
                        {
                            Blanco nuevaFicha = new Blanco(posX, posY, posActual);
                            nuevaFicha.setCostoManhattan(meta);
                            // Si es diagonal me muevo en diagonal. En caso contrario me muevo en línea Recta.
                            moverFicha(posX, posY, posActual, nuevaFicha);
                            listaBuscados.insert(nuevaFicha); // Se inserta la nueva ficha.
                        }
                    }
                }
            }
        }

        /* Inserto los valores de la ruta óptima. */
        public void seleccionarRutaOptima(Ficha ficha)
        {
            while(ficha != null)
            {
                this.listaCompletados.Insert(0, ficha);
                ficha = ficha.getPrevious();
            }
        }

        /* Obtener tiempo de duración del algoritmo. */
        public TimeSpan getTime()
        {
            return this.tiempoAlgoritmo;
        }

        /* Obtengo la ruta con los mejores resultados. */
        public List<Ficha> obtenerMejorRuta()
        {
            return this.listaCompletados;
        }

        /* Obtengo el número de evaluaciones pendientes que realizó el algoritmo. */
        public int obtengaPendientes()
        {
            return listaBuscados.getCantidad();
        }

        /* Obtengo el número de evaluaciones que realizó el algoritmo. */
        public int obtengaEvaluados()
        {
            return listaInsertados.Count;
        }

        /* Obtengo el número de cambios de un mismo valor que realizó el algoritmo. */
        public int getCambios()
        {
            return this.listaBuscados.getCambios();
        }

        /* Obtengo el costo total del algoritmo. */
        public int obtenerDistanciaTotal()
        {
            return this.distanciaRecorrida;
        }

        /* Realiza el algoritmo Estrella. */
        public Boolean accion(Boolean Diagonales)
        {
            Ficha jugador = this.campoJuego.getJugador();
            listaInsertados.Insert(0, jugador);

            DateTime tiempoInicial = DateTime.Now;
            /* Compare si llegue a la meta. O sea ¿Inicio == Final? */
            while(compararMeta(jugador.getX(), jugador.getY()) == false){
                
                if (Diagonales)   // Se permiten diagonales
                {
                    obtenerAliadosDiagonal(jugador);
                }else{            // No se permiten diagonales
                    obtenerAliadosLaterales(jugador);
                }

                jugador = listaBuscados.getFicha(); // Retorna el posible movimiento con el costo más bajo.
                listaInsertados.Insert(0, jugador);

                // Si no hay rutas óptimas.
                if (jugador == null)
                {
                    return false;
                }
            }
            DateTime tiempoFinal = DateTime.Now;

            tiempoAlgoritmo = new TimeSpan(tiempoFinal.Ticks - tiempoInicial.Ticks);

            this.distanciaRecorrida = jugador.getCostoTotal();
            seleccionarRutaOptima(jugador); // Inicializo la ruta óptima con la meta hasta el punto inicial.

            return true;
        }

    }
}
