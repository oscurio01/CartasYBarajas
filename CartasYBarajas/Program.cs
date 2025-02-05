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
        static bool Salir = false;
        static int cartasEnUnMazo = 7;
        static Carta UltimaCarta;
        static Baraja Baraja;
        static Mazo Jugador1;
        static Mazo Jugador2;
        static void Main(string[] args)
        {
            Baraja = new Baraja();
            Jugador1 = new Mazo();
            Jugador2 = new Mazo();

            Mazo mazoDeSalida;

            IniciarMazos();

            while (!Salir)
            {
                JugarPartida();
            }

            mazoDeSalida = (EsTurnoJugador1 ? Jugador1 : Jugador2);
            if (mazoDeSalida.Cartas.Count == 0)
                Console.WriteLine($"Has ganado jugador {(EsTurnoJugador1 ? "1" : "2")}");

        }

        static void IniciarMazos()
        {
            Carta carta;

            for (int i = 0; i <= cartasEnUnMazo; i++)
            {
                Jugador1.AñadirCartas(Baraja.Robar(TipoDeRobo.PrimeroEnLaBaraja));
                Jugador2.AñadirCartas(Baraja.Robar(TipoDeRobo.PrimeroEnLaBaraja));
            }

            do
            {
                carta = Baraja.Robar(TipoDeRobo.PrimeroEnLaBaraja);
                if (carta.Colores == Color.negro || !int.TryParse(carta.Tipo, out _))
                    Baraja.ColocarCarta(carta);
                else
                    UltimaCarta = carta;
            }
            while (Baraja.Cartas[0].Colores == Color.negro || !int.TryParse(carta.Tipo, out _));
        }

        static int LeerUnNumeroCorrecto(int maximo, int minimo = 0)
        {
            int numeroCorrecto;

            while (true)
            {
                Console.Write("> ");
                if (int.TryParse(Console.ReadLine(), out numeroCorrecto) && numeroCorrecto >= minimo && numeroCorrecto <= maximo)
                    return numeroCorrecto;
                else
                    Console.WriteLine("Numero no valido");
            }
        }

        static void JugarPartida()
        {
            int eleccion = 0;
            Mazo mazoDeSalida;

            Console.Clear();
            Console.WriteLine($"Turno del jugador {(EsTurnoJugador1 ? "1" : "2")}");
            UltimaCarta.ImprimeCarta("La ultima carta es :");

            Console.WriteLine(@"
====================
1. Barajar Cartas
2. Robar Cartas
3. Mostrar Mazo
4. Jugar Carta
====================");

            eleccion = LeerUnNumeroCorrecto(24, 1);

            switch (eleccion)
            {
                case 1:
                    BarajarCartas();
                    break;
                case 2:
                    RobarCarta();
                    break;
                case 3:
                    MostrarMazo();
                    break;
                case 4:
                    JugarCarta();
                    break;
                case 24:
                    mazoDeSalida = (EsTurnoJugador1 ? Jugador1 : Jugador2);
                    int cartasTotales = mazoDeSalida.Cartas.Count;

                    for (int i = 0; i < cartasTotales; i++)
                    {
                        mazoDeSalida.SacarCarta(0);
                    }

                    Salir = true;
                    break;
            }

            Console.WriteLine("Pulsa cualquier boton para continuar...");
            Console.ReadLine();
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
            eleccion = LeerUnNumeroCorrecto(3, 1);

            switch (eleccion)
            {
                case 1:
                    carta = Baraja.Robar((TipoDeRobo)eleccion);
                    break;
                case 2:
                    Console.WriteLine("Dime que posicion en numeros:");
                    Baraja.MostrarCartas();
                    
                    posicionDeCarta = LeerUnNumeroCorrecto(Baraja.TamañoDeLaBaraja(), 1);

                    carta = Baraja.Robar((TipoDeRobo)eleccion,posicionDeCarta-1);

                    break;
                case 3:
                    carta = Baraja.Robar((TipoDeRobo)eleccion);
                    break;
            }

            if (EsTurnoJugador1)
                Jugador1.AñadirCartas(carta);
            else
                Jugador2.AñadirCartas(carta);

            carta.ImprimeCarta("Has robado la carta ");
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

            Mazo mazoActual = (EsTurnoJugador1 ? Jugador1 : Jugador2);

            List<Carta> cartas = mazoActual.Cartas;

            // Comprueba que tiene una carta compatible con la carta en la mesa
            foreach (Carta c in cartas)
            {
                esCartaEspecial = !int.TryParse(c.Tipo, out _);
                if ((c.Colores == UltimaCarta.Colores || c.Tipo == UltimaCarta.Tipo) || 
                    (esCartaEspecial && c.Colores == UltimaCarta.Colores)|| c.Colores == Color.negro)
                    sePuedeJugar = true;
            }
            // Si no tiene carta te hecha para atras y te obliga a robar
            if(!sePuedeJugar)
            {
                UltimaCarta.ImprimeCarta("No tienes ninguna carta que sea igual que ");
                Console.WriteLine();
                return;
            }

            MostrarMazo();
            Console.WriteLine("Dime que carta vas a usar:");

            UsaUnaCartaDelMazo();

            if (mazoActual.Cartas.Count == 0)
            {
                Salir = true;
                return;
            }


            // Cambia el turno
            EsTurnoJugador1 = !EsTurnoJugador1;
        }

        static void UsaUnaCartaDelMazo()
        {
            bool esCartaEspecial = true;
            int eleccion = 0;

            Mazo mazoActual = (EsTurnoJugador1 ? Jugador1 : Jugador2);
            Carta cartaActual;

            int maximo = mazoActual.Cartas.Count();

            // Hasta que la carta que usas en el mazo no sea igual en algo o especial no puedes dejar esa carta
            do
            {
                eleccion = LeerUnNumeroCorrecto(maximo, 1);

                cartaActual = mazoActual.Cartas[eleccion - 1];

                // Comprueba si la carta que usas es especial
                esCartaEspecial = !int.TryParse(cartaActual.Tipo, out _);

                if ((cartaActual.Colores == UltimaCarta.Colores || cartaActual.Tipo == UltimaCarta.Tipo) && !esCartaEspecial)
                {
                    UltimaCarta = cartaActual;
                    mazoActual.SacarCarta(eleccion - 1);
                    break;
                }
                else if (esCartaEspecial && (cartaActual.Colores == UltimaCarta.Colores || cartaActual.Colores == Color.negro))
                {
                    // Genero el efecto de la carta
                    EfectosDeCartasEspeciales(ref cartaActual);
                    // la carta ya sea que se haya modificado algo o no vuelve al mazo actual
                    mazoActual.Cartas[eleccion - 1] = cartaActual;

                    UltimaCarta = mazoActual.Cartas[eleccion - 1];
                    mazoActual.SacarCarta(eleccion - 1);
                    break;
                }
                else
                {
                    Console.WriteLine("No tiene ni el mismo color ni el mismo numero, selecciona otra cosa");
                }

            } while (true);
        }

        static void EfectosDeCartasEspeciales(ref Carta carta)
        {
            Mazo mazoRival = (EsTurnoJugador1 ? Jugador2 : Jugador1);

            switch (carta.Tipo)
            {
                case "masDos":
                    for (int i = 0; i < 3; i++)
                    {
                        mazoRival.AñadirCartas(Baraja.Robar(TipoDeRobo.PrimeroEnLaBaraja));
                    }
                    break;
                case "masCuatro":
                    MasCuatro(ref mazoRival, ref carta);
                    break;
                case "cambioDeColor":
                    CambioDeColor(ref carta);
                    break;
            }
        }

        static void MasCuatro(ref Mazo mazoRival, ref Carta carta)
        {
            int eleccion = 0;

            for (int i = 0; i < 4; i++)
            {
                mazoRival.AñadirCartas(Baraja.Robar(TipoDeRobo.PrimeroEnLaBaraja));
            }
            Console.WriteLine("A que color quieres ponerlo? ");
            Console.WriteLine("1.Rojo, 2.Amarillo, 3.Verde. 4.Azul");
            eleccion = LeerUnNumeroCorrecto(4, 1);

            switch (eleccion)
            {
                case 1:
                    carta.Colores = Color.rojo;
                    break;
                case 2:
                    carta.Colores = Color.amarillo;
                    break;
                case 3:
                    carta.Colores = Color.verde;
                    break;
                case 4:
                    carta.Colores = Color.azul;
                    break;
            }
        }

        static void CambioDeColor(ref Carta carta)
        {
            int eleccion = 0;
            Console.WriteLine("A que color quieres ponerlo? ");
            Console.WriteLine("1.Rojo, 2.Amarillo, 3.Verde. 4.Azul");
            eleccion = LeerUnNumeroCorrecto(4, 1);

            switch (eleccion)
            {
                case 1:
                    carta.Colores = Color.rojo;
                    break;
                case 2:
                    carta.Colores = Color.amarillo;
                    break;
                case 3:
                    carta.Colores = Color.verde;
                    break;
                case 4:
                    carta.Colores = Color.azul;
                    break;
            }
        }
    }
}
