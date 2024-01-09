/*
    Esercizio in classe, Matteo Angiolillo 3H, il 2023-10-03
    Questo programma gestisce un gioco "d'azzardo", e riguarda il puntare un certo numero di sesterzi su una somma di due dadi, in caso sia giusta si vincono dei sesterzi, moltiplicati per 10, altrimenti si perdono quelli puntati.
    E' inoltre possibile fare quante partite si desidirino, anche quando si va in bancarotta e si vuole ricominciare il numero dei sesterzi viene ricaricato.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAleaIactaEst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Alea Iacta Est, realizzato da Matteo Angiolillo 3H"; // Titolo console

            

            const int MINSESTERZI = 1, SESTERZIINIZIALI = 50, MINDADO = 1, MAXDADO = 7, MOLTIPLICATOREVINCITA = 10; // costanti per gestire minimo e massimo dei sesterzi giocabili, i numeri minimi e massimi dei dadi e il molt. delle vincite

            int numeroSesterziPuntati, numeroPuntataUtente, sommaDadi, nDado1, nDado2, sesterziGiocabili = SESTERZIINIZIALI; // variabili di input per, sesterzi puntati, il numero scommesso, la sommaDadi e i dadi singoli, poi i sesterzi giocabili che vanno aggiornati mano a mano che si punta
            bool vuoleGiocare = true, inputOkSesterzi, inputOkNumeroPuntata, inputOkChar; // variabili che controllano, se il giocatore vuole ancora giocare, e se i numeri inseriti rispetttano il tipo di dato della variabile (int)
            
            do
            { // ciclo che permette all'utente di rigiocare più partite

                Console.Clear();

                Console.WriteLine(" Matteo Angiolillo 3H\n");
                Console.WriteLine(" --- Gioco dei sesterzi ---");

                do { // ciclo che controlla che l'input dei sesterzi sia corretto

                    Console.Write($"\n Quanti sesterzi si vuole puntare ? (Min = 1, Max = {sesterziGiocabili}) --> ");

                    inputOkSesterzi = int.TryParse(Console.ReadLine(), out numeroSesterziPuntati); // conversione str --> int

                    if (inputOkSesterzi == false) Console.WriteLine("\n Inserire un input intero valido...riprovare!..."); // se l'input è errato allora ritorna errore

                    if (numeroSesterziPuntati < MINSESTERZI || numeroSesterziPuntati > sesterziGiocabili) // si controlla se la puntata è minore del min o maggiore del max
                    
                    {
                        Console.WriteLine("\n Numero dei sesterzi non valido...riprovare..."); // in caso sia vero l'if si da errore
                        inputOkSesterzi = false;
                    }

                } while (!inputOkSesterzi);

                sesterziGiocabili -= numeroSesterziPuntati;

                do

                { // ciclo che controlla la validità dell'input dei dadi

                    Console.Write("\n Scegliere un numero sul quale puntare (Min = 2, Max = 12) --> "); // Inserimento del numero da puntare

                    inputOkNumeroPuntata = int.TryParse(Console.ReadLine(), out numeroPuntataUtente); // conversione + lettura str --> int

                    if (inputOkNumeroPuntata == false) Console.WriteLine("\n Il numero inserito non è valido...riprovare!..."); // controllo dell'input corretto + stringa di errore               

                    else if (numeroPuntataUtente < 2 || numeroPuntataUtente > 12)

                    {
                        Console.WriteLine("\n Il numero puntanto non è valido, selezionare un altro numero (Min = 2, Max = 12) ");
                        inputOkNumeroPuntata = false;
                    }

                } while (!inputOkNumeroPuntata) ;

                    Console.WriteLine("\n Lanciare i dadi? -->"); // richiesta lancio dei dadi con conferma tramite ReadKey();
                    Console.ReadKey();

                    Random rnd = new Random(); // generazione oggetto Random()

                    nDado1 = rnd.Next(1, 7); // lancio dei dati random
                    nDado2 = rnd.Next(1, 7);

                    sommaDadi = nDado1 + nDado2;

                    Console.WriteLine("\n Il numero del primo dado è: " + nDado1 + "\n Il numero del secondo dado è " + nDado2 + "\n Il numero risultante è " + sommaDadi);

                    if (numeroPuntataUtente == sommaDadi) // in caso in cui si vinca
                    {

                        numeroSesterziPuntati *= MOLTIPLICATOREVINCITA;
                        sesterziGiocabili += numeroSesterziPuntati;
                        Console.WriteLine("\n Hai indovinato!...hai vinto " + numeroSesterziPuntati + " sesterzi !!...");

                    }

                    else
                    {
                        Console.WriteLine("\n Hai perso! ... il numero dei tuoi sesterzi è -> " + sesterziGiocabili);
                    }

                if (sesterziGiocabili == 0) // in caso si abbiano finito i sesterzi si manda questo messaggio

                {
                    Console.WriteLine("\n Sei in bancarotta...vuoi cominciare una nuova partita?...");

                }

                char vuoleRigiocareChar; // variabile per controllare se si vuole ricominciare la partita

                do {  // ciclo che controlla la validità dell'input del ciclo per ripetere la partita

                    Console.Write("\n Si vuole rigiocare? (s = sì, n = no) --> ");
                    
                    string varInput = Console.ReadLine();
                    inputOkChar = char.TryParse(varInput, out vuoleRigiocareChar);

                    if (!inputOkChar) Console.WriteLine("Input non valido...riprovare...");

                    if (vuoleRigiocareChar != 's' && vuoleRigiocareChar !=  'n')
                    {
                        inputOkChar = false;
                        Console.WriteLine("Input non valido...riprovare...");
                    }
                    

                 } while (!inputOkChar) ;

                if (sesterziGiocabili == 0 && vuoleRigiocareChar == 's') // in caso siamo con 0 sesterzi e il player vuole rigiocare si riparte con nuovi sesterzi
                {
                    sesterziGiocabili = 50;
                }

                if (vuoleRigiocareChar == 'n') // in caso non voglia rigiocare si esce dal ciclo e si chiude la finestra
                {
                    Console.WriteLine("\n Premi enter per chiudere la finestra...");
                    Console.ReadKey();
                    vuoleGiocare = false;
                }

            } while (vuoleGiocare);                      

        }
    }
}
