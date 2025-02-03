using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartasYBarajas
{
    // Cada carta consta de un numero y un color
    // Pueden existir cartas especiales que hacen diferentes efectos, como cambiar color
    public enum TipoCarta { numero, masDos, masCuatro, cambioDeColor }
    public enum Color { rojo, amarillo, azul, verde, negro}

    public class Carta
    {
        private string tipo;
        private Color color;

        public string Tipo { get { return tipo; } set { tipo = value; } } // puede ser un numero o una carta especial
        public Color Colores { get { return color; } set { color = value; } }

        public Carta()
        {

        }

        public Carta(string tipo, Color color) 
        {
            this.tipo = tipo;
            this.color = color;
        }

        public void ImprimeCarta(string texto = "")
        {
            // Guardamos el color original de la consola
            ConsoleColor originalColor = Console.ForegroundColor;

            // Establecemos el color solo para la parte deseada
            ConsoleColor colorTexto = ConsoleColor.White; // Color normal para la parte sin resaltar
            ConsoleColor colorResaltado = originalColor;

            switch (color)
            {
                case Color.azul:
                    colorResaltado = ConsoleColor.Blue;
                    break;
                case Color.verde:
                    colorResaltado = ConsoleColor.Green;
                    break;
                case Color.rojo:
                    colorResaltado = ConsoleColor.Red;
                    break;
                case Color.amarillo:
                    colorResaltado = ConsoleColor.Yellow;
                    break;
            }

            // Imprimimos la parte normal
            Console.ForegroundColor = colorTexto;
            Console.Write(texto);

            // Imprimimos la parte coloreada
            Console.ForegroundColor = colorResaltado;
            //Console.Write($"{color}");

            Console.Write($"Tipo: {tipo} ");

            // Restauramos el color normal y seguimos imprimiendo
            Console.ForegroundColor = colorTexto;

            // Restauramos el color original de la consola
            Console.ForegroundColor = originalColor;
        }


    }
}
