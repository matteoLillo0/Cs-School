/*
 
    Esercizio in classe, Matteo Angiolillo 3H, 2023-11-07

    Realizzare un programma che permette di creare un test di scrittura 
    
    Quindi il programma cosa deve fare ->

        -) Posizionarsi su una stringa già stampata

        -) Scrivere una stringa sopra a quella già stampata e controllare se i caratteri inseriti corrispondono a quelli della stringa
        
        -) in caso corrispondano coloriamo i sigoli caratteri corretti della la stringa sotto di verde e quelli errati di rosso
        
        -) alla fine stampiamo gli errori totali, i caratteri per minuto e il tempo che ci abbiamo messo 
 
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TypingTest
{
    internal class Program
    {

        #region Creazione Cornice FrameCreation();

        static void FrameCreation() // Funzione per la stampa della Cornice
        {
            Console.Title = " Speed Typing Test || Matteo Angiolillo 3H || 2023-11-07";
            Console.WriteLine("\n  -------------- Speed typing test -------------- ");
            Console.WriteLine("          Matteo Angiolillo 3H 2023-11-07        ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n Digita la seguente stringa il più velocemente possibile!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("--------------------------------------------------------");
        }

        #endregion

        #region Inizia il test StartTest(string frase);

        static void StartTest(string frase) // funzione che inizia il test, e prende come parametro la stringa letta da file tramite la funzione GetRandomSentence()
        {
            int errori = 0;

            // creare il timer per il tempo
            Stopwatch sw = new Stopwatch();

            // stampo la frase a schermo
            Console.WriteLine($"\n {frase}");

            Console.SetCursorPosition(1, 7); // mi posiziono sopra alla stringa


            sw.Restart(); // parte il timer

            for (int i = 0; i<frase.Length; i++)
            {
                char key = Console.ReadKey(true).KeyChar; // Leggo il carattere senza stamparlo però

                if (frase[i] == key) // se i caratteri corrispondono...
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(frase[i]);
                }
                else
                {
                    errori++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(frase[i]);
                }
            }

            // finito l'inserimento della stringa stoppo il timer e cambio il colore della Console per stampare i risultati

            sw.Stop();
            Console.ForegroundColor = ConsoleColor.White;

            // stampa statistiche del test

            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine("\n ----------------------------------");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n Tempo trascorso: {sw.Elapsed}");
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n Caratteri per minuto: {(int)(frase.Length / ((double)sw.ElapsedMilliseconds / 60000.0))}"); // calcolo dei caratteri per minuto 

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n Errori: {errori}");

            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine("\n-----------------------------------" +
                              "\n       Premi invio per uscire..." +
                              "\n-----------------------------------" );

            Console.ReadKey();           
            
        }

        #endregion

        #region Lettura frasi da file GetRandomSentence();

        static string GetRandomSentence()
        {
            // Legge da file le stringhe

            string filePath = @"..\..\sentences.txt"; // legge dalla dir del progetto il file sentences.txt

            List<string> lines = new List<string>();

            using (StreamReader sr = new StreamReader(filePath)) // StreamReader apre e chiude da solo il file
            {
                string line;

                while ((line = sr.ReadLine()) != null) // leggo tutte le stringhe che non sono 'null'
                {
                    lines.Add(line); // aggiungo alla mia lista le frasi presenti nel file
                }
            }

            return lines[new Random().Next(0, lines.Count)]; // ritorna una frase random dalla lista

        }

        #endregion

        #region Main

        static void Main(string[] args)
        {  

            FrameCreation(); // crea cornice
            StartTest(GetRandomSentence()); // inizia il test :)

        }

        #endregion

    }
}
