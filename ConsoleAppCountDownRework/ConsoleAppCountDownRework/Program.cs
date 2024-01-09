/*
    Esercizio in classe, rework vecchia consegna conto alla rovescia, Matteo Angiolillo 3H, 2023-11-21
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Mime;

namespace ContoAllaRovesciaNuovo
{
    internal class Program
    {
        #region Numeri Segmenti
        // di seguito tutti gli array di stringe contenenti i nostri numeri formati dai segmenti, gestiti come array globali
        static string[] cifra_0 = {
            " ▓▓▓▓▓▓ ",
            "▓      ▓",
            "▓      ▓",
            "▓      ▓",
            "▓      ▓",
            "▓      ▓",
            " ▓▓▓▓▓▓ ",
        };

        static string[] cifra_1 = {
            "       ▓",
            "       ▓",
            "       ▓",
            "        ",
            "       ▓",
            "       ▓",
            "       ▓",
        };

        static string[] cifra_2 = {
            " ▓▓▓▓▓▓ ",
            "       ▓",
            "       ▓",
            " ▓▓▓▓▓▓ ",
            "▓       ",
            "▓       ",
            " ▓▓▓▓▓▓ ",
        };
        static string[] cifra_3 = {
            " ▓▓▓▓▓▓ ",
            "       ▓",
            "       ▓",
            " ▓▓▓▓▓▓ ",
            "       ▓",
            "       ▓",
            " ▓▓▓▓▓▓ ",
        };
        static string[] cifra_4 = {
            "        ",
            "▓      ▓",
            "▓      ▓",
            " ▓▓▓▓▓▓ ",
            "       ▓",
            "       ▓",
            "        ",
        };
        static string[] cifra_5 = {
            " ▓▓▓▓▓▓ ",
            "▓          ",
            "▓        ",
            " ▓▓▓▓▓▓ ",
            "       ▓",
            "       ▓",
            " ▓▓▓▓▓▓ ",
        };
        static string[] cifra_6 = {
            " ▓▓▓▓▓▓ ",
            "▓       ",
            "▓       ",
            " ▓▓▓▓▓▓ ",
            "▓      ▓",
            "▓      ▓",
            " ▓▓▓▓▓▓ ",
        };
        static string[] cifra_7 = {
            " ▓▓▓▓▓▓ ",
            "       ▓",
            "       ▓",
            "        ",
            "       ▓",
            "       ▓",
            "        ",
        };
        static string[] cifra_8 = {
            " ▓▓▓▓▓▓ ",
            "▓      ▓",
            "▓      ▓",
            " ▓▓▓▓▓▓ ",
            "▓      ▓",
            "▓      ▓",
            " ▓▓▓▓▓▓ ",
        };
        static string[] cifra_9 = {
            " ▓▓▓▓▓▓ ",
            "▓      ▓",
            "▓      ▓",
            " ▓▓▓▓▓▓ ",
            "       ▓",
            "       ▓",
            " ▓▓▓▓▓▓ ",
        };
        #endregion

        static void StampaCifra(string[] cifra, int riga, int col) //stampa la cifra data
        {

            for (int i = 0; i < cifra.Length; i++)
            {
                Console.SetCursorPosition(col, riga + i);
                Console.WriteLine(cifra[i]);
            }
        }

        static int getInt(string messaggio) // Legge intero 
        {
            while (true) // Controllo Input
            {
                Console.Write(messaggio);
                if (int.TryParse(Console.ReadLine(), out int number)) return number;
                else Console.WriteLine("Input non valido."); 
            }
        }
        static void StampaNumero(int numero, int riga) // stampa numero desiderato
        {
            int col = 0;
            string cifre = numero.ToString();
            for (int i = 0; i < cifre.Length; i++) // Cicla tutte le cifre del numero
            {
                switch (cifre[i])
                {
                    case '0':
                        StampaCifra(cifra_0, riga, col);
                        break;
                    case '1':
                        StampaCifra(cifra_1, riga, col);
                        break;
                    case '2':
                        StampaCifra(cifra_2, riga, col);
                        break;
                    case '3':
                        StampaCifra(cifra_3, riga, col);
                        break;
                    case '4':
                        StampaCifra(cifra_4, riga, col);
                        break;
                    case '5':
                        StampaCifra(cifra_5, riga, col);
                        break;
                    case '6':
                        StampaCifra(cifra_6, riga, col);
                        break;
                    case '7':
                        StampaCifra(cifra_7, riga, col);
                        break;
                    case '8':
                        StampaCifra(cifra_8, riga, col);
                        break;
                    case '9':
                        StampaCifra(cifra_9, riga, col);
                        break;
                }
                col += 9; //spostamento a destra
            }
        }


        static void Main(string[] args)
        {

            Console.Title = " Rework Conto alla rovescia, Matteo Angiolillo 3°H, 2023-11-21 ";
            Console.WriteLine(" Matteo Angiolillo 3H 2023-11-21");
            #region Countdown
            /*
            int riga = 10;
            int col = 10;

            StampaCifra(cifra_9, riga, col); // esecuzioni della funzione del countdown
            StampaCifra(cifra_8, riga, col);
            StampaCifra(cifra_7, riga, col);
            StampaCifra(cifra_6, riga, col);
            StampaCifra(cifra_5, riga, col);
            StampaCifra(cifra_4, riga, col);
            StampaCifra(cifra_3, riga, col);
            StampaCifra(cifra_2, riga, col);
            StampaCifra(cifra_2, riga, col);
            StampaCifra(cifra_1, riga, col);
            */
            #endregion

            int number = getInt("inserisci il numero da rappresentare: ");

            (int, int) position = Console.GetCursorPosition(); //ottengo la posizione di partenza
            StampaNumero(number, position.Item2 + 1);



            #region Fine Stampa
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Stampa Finita...");
            Console.ReadKey();
            #endregion
        }
    }
}
