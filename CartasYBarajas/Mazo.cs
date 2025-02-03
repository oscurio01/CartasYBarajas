using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartasYBarajas
{
    internal class Mazo
    {
        public List<Carta> Cartas { get; set; }

        public Mazo()
        {
            Cartas = new List<Carta>();
        }

        public void AñadirCartas(Carta carta)
        {
            Cartas.Add(carta);
        }

        public void MostrarMano()
        {
            Console.WriteLine("Tus cartas son:");
            foreach (Carta carta in Cartas)
            {
                Console.WriteLine(carta.ToString());
            }
        }

        public Carta SacarCarta(int numero)
        {
            if (Cartas.Count == 0)
                return null;

            Carta carta = Cartas[numero];
            Cartas.RemoveAt(numero);
            return carta;
        }

    }
}
