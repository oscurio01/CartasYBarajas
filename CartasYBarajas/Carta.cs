using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartasYBarajas
{
    // Cada carta consta de un numero y un color
    // Pueden existir cartas especiales que hacen diferentes efectos, como cambiar color
    public enum TipoCarta { numero, salto, cambioDeDireccion, masDos, masCuatro, cambioDeColor }
    public enum Color { rojo, amarillo, azul, verde, negro}

    public class Carta
    {
        private string tipo;
        private Color color;

        public string Tipo { get { return tipo; } set { tipo = value; } } // puede ser un numero o una carta especial
        public Color Colores { get { return color; } }

        public Carta()
        {

        }

        public Carta(string tipo, Color color) 
        {
            this.tipo = tipo;
            this.color = color;
        }

        public override string ToString()
        {
            string tipo = this.tipo;
            if (color == Color.azul) 
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            if (color == Color.verde)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            if (color == Color.rojo)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if(color == Color.amarillo)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            return $" {color} - {tipo} {Console.ForegroundColor = ConsoleColor.Black}";
        }

        
    }
}
