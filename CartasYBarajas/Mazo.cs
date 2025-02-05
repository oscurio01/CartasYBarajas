using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartasYBarajas
{
    // Un mazo consta de 7 cartas aleatorias las cuales van bajando de numero segun de van sacando,
    // Solo se pueden usar si o el color coincide o el numero coincide
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
            for (int i = 0; i < Cartas.Count; i++)
            {
                Cartas[i].ImprimeCarta($"{i+1}/");
            }
            Console.WriteLine();
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
