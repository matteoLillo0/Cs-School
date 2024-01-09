/*
 
    Esercizio in classe, Matteo Angiolillo 3H, 2023-11-28 --> terminato il 2023-12-12 essendo stato assente il 2023-12-05 per il ministage

    Realizzare un programma che simuli il gioco della tombola, quindi che mostri il tabellone dei numeri estratti, e che permetta di estrarre i numeri.

    -) estrai numeri, verifica cinquina, decina e tombola, e generare cartella sono le funzionalità

*/

using System.Reflection.Metadata;

namespace ConsoleAppTombola
{
    internal class Program
    {
        #region Variabili Globali

        static Random rnd = new Random(); // oggetto per fare i numeri random

        static string[] tabellone = new String[90]; // array tabellone con i numeri estratti

        static int[] urna = new int[90];

        static int numeroPalline = 90;

        static string corniceSchedaTomb = "";

        #endregion

        #region Funzione stampa Cornice BuildConsoleFrame();

        static void BuildConsoleFrame()
        {
            Console.Title = "Tombola - Matteo Angiolillo 3H 2023-11-28";
            Console.WriteLine(" ---------------------------------------");
            Console.WriteLine("      Matteo Angiolillo 3H 2023-11-28      ");
            Console.WriteLine(" ---------------------------------------");
            Console.WriteLine("           Tombola Natalizia :)           ");
            Console.WriteLine(" ---------------------------------------");
        }

        #endregion

        #region Inizializza Array con "0" fillArrayOfZeros();

        static void fillArrayOfZeros()
        {
            for (int i = 0; i < tabellone.Length; i++)
            {
                tabellone[i] = "0";
            }
        }

        #endregion

        #region Stampa del Tabellone PrintArray();
        static void PrintArray() // funzione per stampare l'array
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine(" ");

            for (int i = 0; i < tabellone.Length; i++) 
            {
                if (tabellone[i] == "0") Console.Write(" # "); //stampo # se il numero non è ancora uscito

                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (int.Parse(tabellone[i]) < 10) Console.Write(" " + tabellone[i] + " "); // stampo due spazi per una cifra

                    else Console.Write(" " + tabellone[i]); // stampo solo uno spazio se il numero è a due cifre

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }

                if ((i + 1) % 10 == 0) Console.WriteLine(); // ogni 10 si va a capo

            }
        }
        #endregion

        #region Inizializza Array per l'estrazione setUpUrna();
        static void setUpUrna() 
        {
            // inizializza array per estrazione

            for (int i = 0; i<90; i++)
            {
                urna[i] = i+1;
            }
        }

        #endregion

        #region Estrae un numero dall'urna EstraiNumero();

        static int EstraiNumero() // funzione che genera un numero casuale, e ristampa il numero estratto nel range[1, 90 compresi]
        {

            if (numeroPalline > 0)
            {
                int numEstratto = rnd.Next(0, numeroPalline); // genera numero casuale

                int output = urna[numEstratto]; // prendo il valore alla posizione corrispondente

                urna[numEstratto] = urna[numeroPalline - 1]; // swappa il valore nella posizione del numero estratto

                numeroPalline--;

                return output;

            }

            else return -1; // se le palline sono finite si ritorna -1

        }

        #endregion

        #region Aggiorna tabellone AggiornaTabellone();

        static void AggiornaTabellone() // funzione che aggiorna il tabellone inserendo il numero estratto
        {

            int numeroEstratto = EstraiNumero(); // salva il valore del numero estratto

            if (numeroEstratto == -1) // se sono già estratti tutti i numeri
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("I numeri sono usciti tutti!");
                Console.ForegroundColor = ConsoleColor.DarkGray;

                return;
            }
            else {

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Il numero estratto è --{numeroEstratto}--"); // mostra il numero estratto
                Console.ForegroundColor = ConsoleColor.DarkGray;

                tabellone[numeroEstratto-1] = numeroEstratto.ToString(); // assegna effettivamente il numero estratto alla posizione corretta
            }
        }
        #endregion

        #region generazione schedina generaSchedina();
        static void generaSchedina() // funzione per generare la scheda
        {
            int[] tombScheda = new int[27]; // array per la scheda

            for (int i = 0; i < tombScheda.Length; i++)

                tombScheda[i] = 1; // la inizializzo tutto a 1

            for (int i = 0; i < 3; i++) // Ciclo ogni riga del vettore "matrice"
            {
                int[] spazi = new int[4]; // array per le posizioni degli spazi nella matrice

                for (int contatore = 0; contatore < 4; contatore++) // cicla per generare i numeri per le posizioni degli spazi
                {

                    int spazioEstratto = rnd.Next(0, 9); // genera gli spazi
                    spazi[contatore] = spazioEstratto; // assegno gli spazi generati all'array

                    for (int j = 0; j < contatore; j++) // controllo non sia già uscito il numero estratto
                    {  

                        if (spazioEstratto == spazi[j]) 
                        {
                            contatore--;
                            break;
                        }
                    }
                }

                for (int j = 0; j < spazi.Length; j++) // riempio la scheda con gli spazi
                {                                     
                    tombScheda[i * 9 + spazi[j]] = 0; 
                }

                for (int j = 0; j < 9; j++) // inserisco i numeri
                {
                    if (tombScheda[i * 9 + j] == 0) // se c'è 0 (==spazio) non metto nulla 

                        continue; // continua

                    else
                    {
                        int numeroCartEstratto; // numero da mettere nella posizione della "matrice"

                        if (j == 0)
                            numeroCartEstratto = rnd.Next(1, 10); // non posso assegnare lo zero
                        else if (j == 8)
                            numeroCartEstratto = rnd.Next(j * 10, (j + 1) * 10 + 1); // assegno il 90
                        else
                            numeroCartEstratto = rnd.Next(j * 10, (j + 1) * 10); // niente eccezioni

                        tombScheda[i * 9 + j] = numeroCartEstratto;

                        for (int k = 0; k < i * 9 + j; k++) // controllo che non si generino doppioni
                        {
                            if (tombScheda[k] == numeroCartEstratto) 
                            {
                                j--;
                                break;
                            }
                        }
                    }
                }
            }

            // CORNICE SCHEDINA

            corniceSchedaTomb = "╔════╦════╦════╦════╦════╦════╦════╦════╦════╗";

            for (int i = 0; i < tombScheda.Length; i++)
            {
                if (i % 9 == 0)
                {
                    if (i == 0) corniceSchedaTomb += "\n║";
                    else
                    {
                        corniceSchedaTomb += "\n╠════╬════╬════╬════╬════╬════╬════╬════╬════╣\n║";
                    }
                }

                if (tombScheda[i] == 0) corniceSchedaTomb += "    ║"; 
                else if (tombScheda[i] < 10)
                    corniceSchedaTomb += " 0" + tombScheda[i].ToString() + " ║";
                else corniceSchedaTomb += " " + tombScheda[i].ToString() + " ║";

            }
            corniceSchedaTomb += "\n╚════╩════╩════╩════╩════╩════╩════╩════╩════╝";

        }

        #endregion

        #region controllo cinquina checkCinquina();
        static void checkCinquina()
        {

            Console.Clear();
            Console.WriteLine(" Inserire 5 numeri: ");


            int count = 0;
            for (int i = 0; i < 5; i++) // cicla 5 volte
            {
                Console.Write($"\n Inserisci il {i + 1}° numero -> ");
                string numeroInserito = Console.ReadLine();
                for (int j = 0; j < tabellone.Length; j++) // cicla per tutto l'array a cercare corrispondenza
                {
                    if (tabellone[j] == numeroInserito) count++;
                    else continue;
                }

            }
            if (count == 5) // se è 5 hai fatto punto
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" Hai fatto cinquina!");
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }

            else // altrimenti no
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Non hai fatto cinquina...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            
        }
        #endregion

        #region controllo Decina checkDecina();
        static void checkDecina()
        {

            Console.Clear();
            Console.WriteLine(" Inserire 10 numeri: ");


            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"\n Inserisci il {i + 1}° numero -> ");
                string numeroInserito = Console.ReadLine();
                for (int j = 0; j < tabellone.Length; j++)
                {
                    if (tabellone[j] == numeroInserito) count++;
                    else continue;
                }

            }
            if (count == 10)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" Hai fatto decina!");
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Non hai fatto decina...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }

        }
        #endregion

        #region controllo Decina checkTombola();
        static void checkTombola() // funzione uguale a decina solo che si ripete 15 volte
        {

            Console.Clear();
            Console.WriteLine(" Inserire 15 numeri: ");


            int count = 0;
            for (int i = 0; i < 15; i++)
            {
                Console.Write($"\n Inserisci il {i + 1}° numero -> ");
                string numeroInserito = Console.ReadLine();
                for (int j = 0; j < tabellone.Length; j++) // cicla per tutto l'array a cercare corrispondenza
                {
                    if (tabellone[j] == numeroInserito) count++;
                    else continue;
                }

            }
            if (count == 15) // se hai fatto tombola hai tutti i numeri della cartella usciti
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n -- Hai fatto Tombola!! -- \n ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n -- Non hai fatto Tombola... -- \n ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }

        }
        #endregion
        
        #region Main

        static void Main(string[] args)
        {
            #region Inizializzazione Programma Array

            BuildConsoleFrame(); // crea cornice
            fillArrayOfZeros(); // inizializza l'array                
            PrintArray();            
            setUpUrna();

            #endregion

            while (true) // CICLO PROGRAMMA MAIN
            {
                #region Menù scelta utente e lettura scelta

                Console.WriteLine("\n Cosa si vuole fare? \n\n" +
                                  " -) Estrai Numero     [1]\n" +
                                  " -) Genera Cartella   [2]\n" +
                                  " -) Verifica Cinquina [3]\n" +
                                  " -) Verifica Decina   [4]\n" +
                                  " -) Verifica Tombola  [5]\n"
                                  );                

                char cicleCheck = Console.ReadKey(true).KeyChar;

                #endregion

                if (cicleCheck.ToString() == "1")
                {

                    AggiornaTabellone(); // assegno il valore generato all'array e lo aggiorno appunto
                    PrintArray();// stampo tutto

                }
                else if (cicleCheck.ToString() == "2") // controlla i vari input
                {

                    generaSchedina(); // crea schedina 

                    PrintArray(); // ristampa tabellone

                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine(corniceSchedaTomb); // stampa schedina
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else if (cicleCheck.ToString() == "3")
                {
                    checkCinquina();
                }
                else if (cicleCheck.ToString() == "4") // controlla gli altri tasti per l'input
                {
                    checkDecina(); // controlli per i punti
                }
                else if (cicleCheck.ToString() == "5")
                {
                    checkTombola();
                }
                else
                {
                    Console.WriteLine("...premere uno dei tasti indicati..."); // gestisce i tasti diversi da quelli indicati
                }
                
            }

        }

        #endregion
    }
}