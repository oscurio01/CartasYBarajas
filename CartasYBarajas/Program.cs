using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartasYBarajas
{
    internal class Program
    {
        static bool EsTurnoJugador1 = true;
        static bool Salir = true;
        static Carta UltimaCarta;
        static Baraja Baraja;
        static Mazo Jugador1;
        static Mazo Jugador2;
        static void Main(string[] args)
        {
            int eleccion = 0;

            Baraja = new Baraja();
            Jugador1 = new Mazo();
            Jugador2 = new Mazo();

            IniciarMazos();

            while (Salir)
            {

                Console.WriteLine("Pulsa cualquier boton para continuar...");
                Console.ReadLine();

                Console.Clear();
                Console.WriteLine($"La ultima carta es :{UltimaCarta.ToString()}");

                Console.WriteLine(@"
====================
1. Barajar Cartas
2. Robar Cartas
3. Mostrar Baraja
4. Mostrar Mazo
5. Jugar Carta
0. Salir
====================");

                eleccion = EsUnNumeroCorrecto(eleccion, 5, 0);

                switch (eleccion)
                {
                    case 1:
                        BarajarCartas();
                        break;
                    case 2:
                        RobarCarta();
                        break;
                    case 3:
                        MostrarBaraja();
                        break;
                    case 4:
                        MostrarMazo();
                        break;
                    case 5:
                        JugarCarta();
                        break;
                    case 0:
                        Salir = false;
                        break;
                }

            }
        }

        static void IniciarMazos()
        {
            Carta carta;

            for (int i = 0; i < 13; i++)
            {
                Jugador1.AñadirCartas(Baraja.Robar(1));
                Jugador2.AñadirCartas(Baraja.Robar(1));
            }

            do
            {
                carta = Baraja.Robar(1);
                if (carta.Colores == Color.negro)
                    Baraja.ColocarCarta(carta);
                else
                    UltimaCarta = carta;
            }
            while (Baraja.Cartas[0].Colores == Color.negro);
        }

        static int EsUnNumeroCorrecto(int numeroCorrecto, int limites, int minimo = 0)
        {
            while (true)
            {
                Console.Write("> ");
                if (int.TryParse(Console.ReadLine(), out numeroCorrecto) && numeroCorrecto > minimo && numeroCorrecto <= limites)
                    return numeroCorrecto;
                else
                    Console.WriteLine("Numero no valido");
            }
        }

        static void BarajarCartas() 
        {
            Baraja.Barajar();
            Console.WriteLine("Se han barajado las cartas");
        }

        static void RobarCarta() 
        {
            int eleccion = 0;
            int posicionDeCarta = 0;
            Carta carta = new Carta();

            Console.WriteLine("Dime que metodo de robar quieres:");
            Console.WriteLine(@"
======================================
1.Robar la primera carta de la baraja
2.Robar en la posicion que te diga
3.Robar al azar
=====================================");
            eleccion = EsUnNumeroCorrecto(eleccion, 3, 1);

            switch (eleccion)
            {
                case 1:
                    carta = Baraja.Robar(eleccion);
                    break;
                case 2:
                    Console.WriteLine("Dime que posicion:");
                    Baraja.MostrarCartas();

                    posicionDeCarta = EsUnNumeroCorrecto(posicionDeCarta, Baraja.TamañoDeLaBaraja());

                    carta = Baraja.Robar(eleccion,posicionDeCarta);

                    break;
                case 3:
                    carta = Baraja.Robar(eleccion);
                    break;
            }

            if (EsTurnoJugador1)
                Jugador1.AñadirCartas(carta);
            else
                Jugador2.AñadirCartas(carta);

            Console.WriteLine($"Has robado la carta {carta.ToString()}");
        }

        static void MostrarBaraja() 
        {
            Console.WriteLine("Estas son las cartas en la baraja: ");
            Baraja.MostrarCartas();
        }

        static void MostrarMazo() 
        {
            Console.WriteLine($"Jugador {(EsTurnoJugador1? "1" : "2")} esta es tu mano");
            if (EsTurnoJugador1)
                Jugador1.MostrarMano();
            else
                Jugador2.MostrarMano();

            
        }

        static void JugarCarta() 
        {
            bool sePuedeJugar = false;
            bool esCartaEspecial = true;
            int limites = (EsTurnoJugador1 ? Jugador1.Cartas.Count() : Jugador2.Cartas.Count());
            int eleccion = 0;
            List<Carta> carta = (EsTurnoJugador1 ? Jugador1.Cartas : Jugador2.Cartas);

            foreach (Carta c in carta)
            {
                if ((c.Colores == UltimaCarta.Colores || c.Tipo == UltimaCarta.Tipo) || esCartaEspecial)
                    sePuedeJugar = true;
            }
            if(!sePuedeJugar)
            {
                Console.WriteLine($"No tienes ninguna carta que sea igual que {UltimaCarta}");
                return;
            }

            MostrarMazo();
            Console.WriteLine("Dime que carta vas a usar:");

            do
            {
                eleccion = EsUnNumeroCorrecto(eleccion, limites);



            } while (!esCartaEspecial);



        }
    }
}
