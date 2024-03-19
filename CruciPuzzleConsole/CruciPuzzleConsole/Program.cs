/*  
    Matteo Angiolillo, 3H, 2024-03-19
    Realizzare un app console che risolva il gioco del crucipuzzle leggendo da file: matrice con le parole e la lista di parole nascoste nella matrice 
*/

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Xml.Schema;

namespace Crucipuzzle
{
    internal class Program
    {
        #region globali

        static string filename = "..\\..\\..\\Crucipuzzle1.txt";
        static char[,] matriceFinale = RiempiMatriceQuiz(filename);
        static bool[,] indiciTrovati = RiempiIndiciTrovati(filename);
        static List<string> parole = LeggiParole(filename);

        #endregion

        #region RiempiMatriceQuiz(string)

        static char[,] RiempiMatriceQuiz(string nome_file)
        {
            int rows = 0;
            int columns = 0;
            using (StreamReader sr = new StreamReader(nome_file))
            {

                string firstLine = sr.ReadLine(); // leggiamo la prima riga
                columns = firstLine.Length; // la sua lunghezza saranno le colonne della matrice

                string line = "";
                do
                {
                    rows++;
                    line = sr.ReadLine();

                } while (line != ""); // leggiamo una linea a incrementiamo il numero di righe, andiamo a capo e ripetiamo

                sr.Close(); // important: chiudere il file di lettura
            }
            using (StreamReader sr = new StreamReader(nome_file)) // qui invece leggiamo tutta la matrice e ci salviamo i valori
            {
                char[,] matrice = new char[rows, columns];

                for (int i = 0; i < rows; i++) // lavoriamo spostandoci per righe
                {
                    string line = sr.ReadLine();
                    for (int j = 0; j < columns; j++)
                    {
                        matrice[i, j] = line[j];
                    }
                }
                sr.Close();
                return matrice; // la funzione ritornerà la matrice
            }
        }

        #endregion

        #region RiempIndiciTrovati(string)

        static bool[,] RiempiIndiciTrovati(string nome_file) // questa funzione ci serve per creare una seconda matrice, di dimensioni uguali alla prima ma di bool
                                                             // per poterci salvare le posizioni delle parole trovate
        {
            int rows = 0;
            int columns = 0;
            using (StreamReader sr = new StreamReader(nome_file))
            {

                string firstLine = sr.ReadLine();
                columns = firstLine.Length; // calcolo col

                string line = "";
                do
                {
                    rows++; // incremento row
                    line = sr.ReadLine();

                } while (line != "");

                sr.Close(); // prima del close abbiamo trovato columns e rows, dimensioni della matrice di bool
            }

            bool[,] matrice = new bool[rows, columns]; // matrice di bool

            for (int i = 0; i < rows; i++) // questo setta falso tutti gli elementi
            {
                for (int j = 0; j < columns; j++)
                {
                    matrice[i, j] = false;
                }
            }

            return matrice;
        }

        #endregion

        #region Leggi Parole(string)

        static List<string> LeggiParole(string nome_file) // questa funzione legge la lista di parole da trovare
        {
            using (StreamReader sr = new StreamReader(nome_file))
            {
                bool read = false;
                List<string> parole = new List<string>(); // lista di parole
                while (!sr.EndOfStream) 
                {
                    string line = sr.ReadLine();
                    if (read)
                    {
                        parole.Add(line); // se read == true aggiungiamo alla lista di parole la linea appena letta
                    }
                    if (line == "")
                    {
                        read = true;
                    }
                }
                return parole;
            }
        }

        #endregion

        #region Ricerca Orizzontale Sinistra --> Destra

        static void RicercaSinistraDestra(string word) // 
        {
            int len = word.Length;

            for (int i = 0; i < matriceFinale.GetLength(0); i++) // cicla per la dimensione 0
            { 
                for (int j = 0; j < matriceFinale.GetLength(1) - len + 1; j++) // ora per la dimensione 1
                {
                    string check = ""; // aggiungiamo i caratteri della matrice a una stringa per la condizione dell'if a riga 149
                    for (int x = 0; x < len; x++)
                    {
                        check += matriceFinale[i, j + x];
                    }
                    if (check == word) // controllo effettivo
                    {
                        for (int x = 0; x < len; x++)
                        {
                            indiciTrovati[i, j + x] = true; // modifica gli indici
                        }
                        break;
                    }
                }
            }
        }

        #endregion

        #region Ricerca Orizzontale Destra --> Sinistra

        static void RicercaDestraSinistra(string word) // fa la stessa cosa della funzione di prima ma partiamo dalla fine della matrice
        {
            int len = word.Length;

            for (int i = matriceFinale.GetLength(0) - 1; i > 0; i--)
            {
                for (int j = matriceFinale.GetLength(1) - 1; j > len - 1; j--)
                {
                    string check = "";
                    for (int x = 0; x < len; x++)
                    {
                        check += matriceFinale[i, j - x];
                    }
                    if (check == word)
                    {
                        for (int x = 0; x < len; x++)
                        {
                            indiciTrovati[i, j - x] = true;
                        }
                        break;
                    }
                }
            }
        }

        #endregion

        #region Ricerca Verticale Alto --> Basso

        static void RicercaAltoBasso(string word) // qui ci si sposta prima sul carattere delle colonne e poi di riga in riga
        {
            int len = word.Length;

            for (int i = 0; i < matriceFinale.GetLength(1); i++)
            {
                for (int j = 0; j < matriceFinale.GetLength(0) - len + 1; j++)
                {
                    string check = "";
                    for (int x = 0; x < len; x++)
                    {
                        check += matriceFinale[j + x, i];
                    }
                    if (check == word)
                    {
                        for (int x = 0; x < len; x++)
                        {
                            indiciTrovati[j + x, i] = true;
                        }
                        break;
                    }
                }
            }
        }

        #endregion

        #region Ricerca Verticale Basso --> Alto

        static void RicercaBassoAlto(string word) // qui uguale alla funzione sopra ma partiamo dal basso all'alto
        {
            int len = word.Length;

            for (int i = matriceFinale.GetLength(1) - 1; i > 0; i--)
            {
                for (int j = matriceFinale.GetLength(0) - 1; j > len - 2; j--)
                {
                    string check = "";
                    for (int x = 0; x < len; x++)
                    {
                        check += matriceFinale[j - x, i];
                    }
                    if (check == word)
                    {
                        for (int x = 0; x < len; x++)
                        {
                            indiciTrovati[j - x, i] = true;
                        }
                        break;
                    }
                }
            }
        }

        #endregion

        #region Stampa tabella e colorazione parole trovate

        static void StampaTabella(string word) // funzione che stampa la tabella e colora le parole trovate
        {
            Console.Clear();

            int y = matriceFinale.GetLength(0);
            int x = matriceFinale.GetLength(1);

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (indiciTrovati[i, j])
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    Console.Write($"{matriceFinale[i, j]} ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("\n");
            }
            Console.Write($"\n{word}");
        }

        #endregion

        #region Main()

        static void Main(string[] args)
        {
            StampaTabella("");
            foreach (string word in parole)
            {
                Console.ReadKey();

                RicercaDestraSinistra(word);
                RicercaSinistraDestra(word);
                RicercaAltoBasso(word);
                RicercaBassoAlto(word);

                StampaTabella(word);
            }

            Console.ReadKey();
        }

        #endregion

    }
}