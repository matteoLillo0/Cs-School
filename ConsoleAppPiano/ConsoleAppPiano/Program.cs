/*
 
    Esercizio in classe, Matteo Angiolillo 3H, 2023-11-21
    Consegna su classroom, in pratica bisogna realizzare, usando gli array, un piano forte che in base al tasto premuto, il pc suona una determinata sequenza

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleAppPiano
{
    internal class Program
    {

        #region Variabili Globali 

        //          array "globali" per avere gli array di frequenze e caratteri utilizzabili da tutti i metodi

        // Nota -->                 DO  DO#   RE   RE#   MI   FA  FA# SOL  SOL# LA   LA#   SI  DO   RE
        static char[] tasti     = { 'a', 'w', 's', 'e', 'd', 'f', 't', 'g', 'y', 'h', 'u', 'j', 'k', 'l' };
        static int[] frequenze = { 262, 277, 294, 311, 330, 349, 370, 392, 415, 440, 466, 494, 523, 587 };

        #endregion

        #region Costruzione Cornice e corrispondenze note | BuildFrame()

        static void BuildFrame()
        {
            Console.Title = "ConsoleAppPiano | Matteo Angiolillo 3H | 2023-11-21";
            Console.WriteLine(" Matteo Angiolillo 3H, 2023-11-21");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Corrispondenza tasti->note:\n " +
                              "a = Do | w = Do# | s = Re | e = Re# | d = Mi | f = Fa | t = Fa# | g = Sol |\n y = Sol# | h = La | u = La# | j = Si | k = Do | l = Re |");
        }

        #endregion

        #region Funzione per suonare Play()

        static void Play()
        {
            while (true) // permette di suonare all'infinito
            {
                char tastoPremuto = Console.ReadKey(true).KeyChar; // legge il char

                for (int i = 0; i<tasti.Length; i++) // controlla se è nell'aray dei tasti premuti
                {
                    if (tasti[i] == tastoPremuto) // se è presente
                    {
                        Console.Beep(frequenze[i], 400); // suona la frequenza corrispondente all'indice
                    }
                }
            }
        }

        #endregion

        #region Main

        static void Main(string[] args)
        {
            BuildFrame();
            Play();
            Console.ReadKey();
        }

        #endregion
    }
}
