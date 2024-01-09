/*
 
  Esercizio 3)

        Scrivere un programma che accetti in input delle coppia di valori  <nome_prodotto, prezzo>, rispettivamente di tipo string e double, e
        alla fine stampi il nome_prodotto ed il prezzo del prodotto più  costoso. L'elenco termina quando il nome_prodotto è "" (stringa vuota).

*/

using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEsercizio3Cicli
{
    internal class Program
    {
        static void FindMostExpensiveObj()
        {
            string nomeProdottoUser, nomeProdottoMax = "", stTemporanea;
            double prezzoUser, prezzoMax = double.MinValue; // prezzoMax inizializzato al minimo valore dei double e non 0.
            
            while (true) // ciclo per poter inserire quanti prodotti vogliamo
            {
                Console.Write(" Inserisci il nome del prodotto: "); // legge il nome del prodotto
                nomeProdottoUser = Console.ReadLine(); // lo salva in una variabile

                if (nomeProdottoUser == "") // se si inserisce lo spazio vuoto, allora si esce dal ciclo
                {
                    Console.WriteLine("\n ---- Fine Inserimeno ---- \n");
                    break;                    
                }

                prezzoUser = InsertAndCheckValidityPrice(); // salviamo il valore convalidato e inserito tramite la funzione();, nella variabile prezzoUser

                if(prezzoUser > prezzoMax) // controllo per il prezzo maggiore
                {
                    prezzoMax = prezzoUser; // assegnamento nome e prezzo ai valori max
                    nomeProdottoMax = nomeProdottoUser;
                }
            }

            Console.WriteLine($" Il nome del prodotto + costoso è: {nomeProdottoMax}\n Il prezzo del prodotto + costoso è: {prezzoMax}"); // stringa di output
            
        }
        static double InsertAndCheckValidityPrice()
        {
            string stInput; // stringa per salvare il Console.ReadLine()
            double result; // double per salvare la conversione stInput --> double
            
            do
            {
                Console.Write(" Inserisci un prezzo: ");
                stInput = Console.ReadLine();

                if (!double.TryParse(stInput, out result))
                {
                    Console.WriteLine(" Input non valido ... riprovare");
                }

            } while (!double.TryParse(stInput, out result)); // ripete il ciclo fino a che la conversione non ha avuto successo

            return result; // ci ritorna il risultato in modo da poterlo riutilizzare per la nostra funzione dei max

        }
        static void MakeFrame() // funzione che crea una cornice bellina alla console :)
        {
            Console.Title = " Trova il prodotto + costoso || Matteo Angiolillo 3H ";
            Console.WriteLine("\n                  ---------- Programma realizzato da Matteo Angiolillo 3H ---------- \n");
            Console.WriteLine(" ---------- Inserire una lista di: nome-prodotto e prezzo, per trovare il prodotto + costoso ---------- \n");
        }
        static void Main(string[] args)
        {
            #region Ragionamento ;)

            // programma legge i valori nomeProdottoUser e prezzoUser
            // controlla che il prezzo inserito sia più grande del precedente
            // in caso di si, assegna alle variabili, nomeProdottoMax e prezzoMax gli input utente
            // altrimenti non lo fa e va avanti
            // se nomeProdotto = "" allora si ferma il ciclo, e si stampano nomeProdottoMax e prezzoMax
            // per non fare tutto questo nel main, posso fare una funzione che stampi a console i max, un'altra che mi validi l'input del prezzo

            #endregion

            MakeFrame();

            FindMostExpensiveObj();

            Console.ReadKey();
        }
    }
}
