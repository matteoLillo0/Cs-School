/*
    Matteo Angiolillo 3°H 2023-10-20
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMonteCarlo
{
    internal class Program
    {       

        static void Main(string[] args)

        {
            double x, y, risultatoFinale, lato; // coordinate del punto, vanno generate casualmente.
            long puntiTotali;
            long puntiDentro = 0;

            Console.Write("Inserire lato: ");

            lato = Convert.ToDouble(Console.ReadLine());

            Console.Write("Inserire punti totali: ");

            puntiTotali = Convert.ToInt64(Console.ReadLine());
            
            Random rnd = new Random();

            for (long i = 0; i < puntiTotali; i++) // ciclo che genera coordinate per ogni punto
            {                

                x = rnd.NextDouble() * lato; // .NextDouble() genera sempre un random tra 0.0 e 1.0, per cui se vogliamo un random tra 0 e il nostro lato dobbiamo moltiplicarlo per L
                y = rnd.NextDouble() * lato;

                if (x * x + y * y <= lato * lato) // calcoliamo al distanza e la aggiungiamo alla statistica dei punti
                
                {
                    puntiDentro++; // stima dell'area

                }
                
            }

            risultatoFinale = puntiDentro * 4.0 / puntiTotali; // la probabilità secondo la quale i punti generati sono caduti dentro al quarto di circonferenza

            Console.WriteLine("Il risultato finale: " + risultatoFinale);

            Console.ReadLine();

        }
    }
}
