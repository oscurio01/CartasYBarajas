using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartasYBarajas
{
    // Una baraja consta de 12 cartas aleatorias las cuales van bajando de numero segun de van usando,
    // Solo se pueden usar si o el color coincide o el numero coincide
    // Deberia añadir las cartas especiales las cuales cambian de color o te hacen un +2 y +4 
    public class Baraja
    {
        public List<Carta> Cartas { get; set; }

        public Baraja() 
        { 
            Cartas = new List<Carta>();
            GenerarBaraja();
        }

        void GenerarBaraja()
        {
            for (int j = 0; j < 3; j++)
            {
                foreach (Color color in Enum.GetValues(typeof(Color)))
                {
                    if (color == Color.negro)
                        continue;

                    for (int i = 0; i < 10; i++)
                    {
                        Cartas.Add(new Carta(i.ToString(), color));
                    }

                    // Cartas especiales de color
                    Cartas.Add(new Carta(TipoCarta.cambioDeDireccion.ToString(), color));
                    Cartas.Add(new Carta(TipoCarta.salto.ToString(), color));
                    Cartas.Add(new Carta(TipoCarta.masDos.ToString(), color));
                }

                // Cartas especiales unicas
                Cartas.Add(new Carta(TipoCarta.masCuatro.ToString(), Color.negro));
                Cartas.Add(new Carta( TipoCarta.cambioDeColor.ToString(), Color.negro));
            }

            Barajar();
        }

        // Barajar
        public void Barajar()
        {
            Random random = new Random();
            Cartas = Cartas.OrderBy(c => random.Next()).ToList();
        }

        public int TamañoDeLaBaraja()
        {
            return Cartas.Count;
        }

        // Robar
        public Carta Robar(int eleccion, int numero = 0)
        {
            if (Cartas.Count == 0)
                return null;

            else if (eleccion == 3)
            {
                Random rand = new Random();
                numero = rand.Next();
            }
            Carta carta = Cartas[numero];
            Cartas.RemoveAt(numero);
            return carta;

        }

        // Metodo Mostrar cartas
        public void MostrarCartas()
        {
            foreach (Carta carta in Cartas)
            {
                Console.Write($"{carta.ToString()},");
            }
        }

        public void ColocarCarta(Carta carta)
        {
            Cartas.Add(carta);
        }

    }
}
