using System;
using System.Collections.Generic;

namespace Proyecto2grupo_2 
{
    class Program
    {
        static char[,] tablero = new char[6, 7]; // Matriz que representa el tablero
        static char jugadorActual = '$'; // Jugador actual (comienza con '$')
        static Stack<int> historialMovimientos = new Stack<int>(); // Pila para almacenar el historial de movimientos
        static Queue<char> colaJugadores = new Queue<char>(); // Cola para alternar entre jugadores

        static void Main(string[] args)
        {
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("1. Jugar");
                Console.WriteLine("2. Salir");
                Console.Write("Ingrese su elección: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Jugar();
                        break;
                    case "2":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, ingrese 1 o 2.");
                        break;
                }
            }
        }

        static void Jugar()
        {
            InicializarTablero();
            InicializarJugadores();

            while (true)
            {
                ImprimirTablero();
                Console.WriteLine($"Turno del jugador {jugadorActual}. Elija una columna (1-7): ");
                int columna = Convert.ToInt32(Console.ReadLine()) - 1;

                if (MovimientoValido(columna))
                {
                    RealizarMovimiento(columna);
                    historialMovimientos.Push(columna); // Agregar el movimiento actual al historial
                    if (VerificarVictoria(jugadorActual))
                    {
                        ImprimirTablero();
                        Console.WriteLine($"¡El jugador {jugadorActual} ha ganado!");
                        break;
                    }
                    if (TableroCompleto())
                    {
                        ImprimirTablero();
                        Console.WriteLine("¡Empate!");
                        break;
                    }
                    CambiarJugador(); // Cambiar al siguiente jugador utilizando la cola
                }
                else
                {
                    Console.WriteLine("Movimiento no válido. Intente de nuevo.");
                }
            }

            Console.WriteLine("Presione cualquier tecla para volver al menú principal.");
            Console.ReadKey();
        }

        static void InicializarTablero()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    tablero[i, j] = ' ';
                }
            }
        }

        static void InicializarJugadores()
        {
            colaJugadores.Enqueue('$');
            colaJugadores.Enqueue('%');
        }

        static void ImprimirTablero()
        {
            Console.Clear();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write("|" + tablero[i, j]);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("---------------");
            Console.WriteLine(" 1 2 3 4 5 6 7 ");
        }

        static bool MovimientoValido(int columna)
        {
            if (columna < 0 || columna >= 7 || tablero[0, columna] != ' ')
            {
                return false;
            }
            return true;
        }

        static void RealizarMovimiento(int columna)
        {
            for (int i = 5; i >= 0; i--)
            {
                if (tablero[i, columna] == ' ')
                {
                    tablero[i, columna] = jugadorActual;
                    break;
                }
            }
        }

        static bool VerificarVictoria(char jugador)
        {
            // Verificar horizontal disminuir j 
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tablero[i, j] == jugador &&
                        tablero[i, j + 1] == jugador &&
                        tablero[i, j + 2] == jugador &&
                        tablero[i, j + 3] == jugador)

                    {
                        return true;
                    }
                }
            }

            // Verificar vertical disminuir i
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (tablero[i, j] == jugador &&
                        tablero[i + 1, j] == jugador &&
                        tablero[i + 2, j] == jugador &&
                        tablero[i + 3, j] == jugador)
                    {
                        return true;
                    }
                }
            }

            // Verificar diagonal ascendente
            for (int i = 3; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tablero[i, j] == jugador &&
                        tablero[i - 1, j + 1] == jugador &&
                        tablero[i - 2, j + 2] == jugador &&
                        tablero[i - 3, j + 3] == jugador)
                    {
                        return true;
                    }
                }
            }

            // Verificar diagonal descendente
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tablero[i, j] == jugador &&
                        tablero[i + 1, j + 1] == jugador &&
                        tablero[i + 2, j + 2] == jugador &&
                        tablero[i + 3, j + 3] == jugador)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static bool TableroCompleto()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (tablero[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static void CambiarJugador()
        {
            jugadorActual = (jugadorActual == '$') ? '%' : '$';

        }
    }
}