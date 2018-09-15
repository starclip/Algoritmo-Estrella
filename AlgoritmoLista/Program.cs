using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoLista
{
    class Program
    {
        static void Main(string[] args)
        {
            int m = 8;
            int n = 8;
            int distancia = 10;
            int enemy = 16;
            Boolean state;
            Boolean hayDiagonales = true;
            AEstrella algoritmoEstrella;
            Tablero tablero;
            // Asigno el punto Final. Y el punto Inicial.

            tablero = new Tablero(m, n, distancia);
            tablero.crearTablero(); // inicializo el tablero.
            tablero.seleccionarMeta(); // se selecciona la meta al azar.
            tablero.seleccionarInicio(); // Se selecciona el inicio al azar.
            tablero.seleccionarEnemigos(enemy); // Se seleccionan los enemigos al azar.
            tablero.printTablero();

            algoritmoEstrella = new AEstrella(m, n, tablero);
            state = algoritmoEstrella.accion(hayDiagonales); // Se realiza el algoritmo de estrella.

            if (state)
            {
                tablero.setRuta(algoritmoEstrella.obtenerMejorRuta());
                tablero.printTablero();
                Console.WriteLine("Se encontró la siguiente ruta óptima");
                Console.WriteLine("Número de pendientes: " + algoritmoEstrella.obtengaPendientes());
                Console.WriteLine("Número de evaluados: " + algoritmoEstrella.obtengaEvaluados());
                Console.WriteLine("Tiempo de duración: " + algoritmoEstrella.getTime().ToString());
                Console.WriteLine("Número de cambios: " + algoritmoEstrella.getCambios());
                Console.WriteLine("Total de distancia recorrida: " + algoritmoEstrella.obtenerDistanciaTotal());
            }
            else
            {
                Console.WriteLine("No se pudo encontrar la ruta óptima.");
            }
            Console.ReadLine();
        }
    }
}
