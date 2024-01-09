/*
    Esercizio in classe, Matteo Angiolillo 3H, 2023-10-31
    Realizzare un programma, utilizzando dei metodi esterni al main, che permette di convertire un int da base 10 alla base scelta, oppure un numero dalla base scelta alla base 10

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BaseConv
{
    internal class Program
    {
        static void BuildConsoleFrame()
        {
            Console.Title = "Conversioni di Base | Matteo Angiolillo 3H | 2023-10-31 ";

            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("                          Matteo Angiolillo 3H, 2023-10-31 ");
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("Conversioni di un intero a una base scelta, e Conversione di Char/Int di base scelta a intero");
            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }


        static string Reverse(string strIn) // funzione che reversa la stringa
        {
            string reversedString = "";
            char[] chars = strIn.ToCharArray();
            Array.Reverse(chars);

            return new string(chars);
        }
        static char IntToChar(int digit) // funzione che ci permette tramite l'ascii di prendere un intero e trasformarlo in carattere
        {
            char resultCh;

            if (digit < 10) resultCh = (char)(digit + 48); // se la cifra è da 0 a 9, si ritornano i caratteri ascii corrispondenti ai numeri

            else resultCh = (char)(digit + 55); // se il valore è da 10 a 36, ci ritorna le lettere dell'alfabeto
                        
            return resultCh;
        }

        static int CharToInt(char digit) // funzione che ci permette tramite l'ascii di prendere un carattere e trasformarlo in intero
        {
            int resultInt;

            if (digit < '9') resultInt = (char)(digit - 48);

            else resultInt = (char)(digit - 55);
            
            return resultInt;
        }

        static string IntToBase(int valoreInput, int baseInput) // funzione che converte un valore intero in un'altro in un'altra base a scelta nostra
         {
            int resto;
            string result = "";
            
            while (valoreInput != 0) // conversione
            {
                resto = valoreInput % baseInput;                 

                result += IntToChar(resto);

                valoreInput /= baseInput;                
            }            
            return Reverse(result); // reversa tutto grazie alla funzione scritta sopra Reverse()
        }
        // La funzione BaseToInt trasforma un carattere o un int dalla base che gli diciamo noi, e ce lo trasforma in base10
        static int BaseToInt(string valoreInput, int baseInput) // devo prendere ogni posizione della stringa di input reversata, e moltiplicarla per la base^[i] 
        {
            int risultato = 0;
            int risultatoTemp = 0;

            valoreInput = Reverse(valoreInput);

            for (int i = 0; i < valoreInput.Length; i++) // conversione
            {                
                risultatoTemp = CharToInt(valoreInput[i]) * (int)Math.Pow(baseInput, i);

                risultato += risultatoTemp;
            }

            return risultato;
        }

        static void Main(string[] args)
        {
            string interoABase = IntToBase(10, 16); 
            int BaseAIntero = BaseToInt("Z", 36);

            BuildConsoleFrame(); // stampa console

            // Output metodi

            Console.WriteLine($"Numero da intero a base ( Input da parametri ): {interoABase}");
            Console.WriteLine($"Numero da base scelta a base 10 ( Input da parametri ): {BaseAIntero}");

            Console.ReadKey();
        }
    }
}
