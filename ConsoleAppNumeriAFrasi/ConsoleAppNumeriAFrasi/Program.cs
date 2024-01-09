/*
 * 
    Esercizio in classe, Matteo Angiolillo 3H, 2023-10-17.
    Realizzare un programma che traduce un input intero, nello stesso intero ma scritto a lettere, i numeri in input vanno da un range di "0" --> "9999"
    Es) 10 --> "dieci"

 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleAppNumeriAFrasi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Intestazione e titolo console

            Console.Title = "ConsoleAppNumeriAFrasi, Matteo Angiolillo 3H, 2023-10-17";

            Console.WriteLine("\n ---------------------- Matteo Angiolillo | 3H | 2023-10-17 ---------------------- \n\n" +
                            "                      Traduzione di interi da cifre a lettere      ");

            Console.WriteLine(" ---------------------------------------------------------------------------------");

            #endregion

            #region Dichiarazione Variabili

            int numeroUtenteIntero;
            string finalString = "";
            bool inputOk, controlloNumeriSpeciali = false;

            #endregion

            #region Lettura Input e convalidazione

            do // ciclo per convalidare l'input
            {                
                Console.Write("\n Inserire un numero intero -> ");
                inputOk = int.TryParse(Console.ReadLine(), out numeroUtenteIntero);

                if (!inputOk) Console.WriteLine(" Input non valido...riprovare..."); // controllo se la conversione è avvenuta

                if (numeroUtenteIntero > 9999 || numeroUtenteIntero < 0) // controllo del range
                {
                    Console.WriteLine("Inserire un numero compreso tra 0 e 9999...riprovare");
                    inputOk = false;
                }                

            } while (!inputOk);

            #endregion

            #region Traduzione migliaia

            switch (numeroUtenteIntero / 1000) // Prendiamo in considerazione solo la prima cifra  
            {
                case 1: // in base ai casi
                    finalString += "mille"; // concateniamo alla stringa finale il numero in lettere
                    break;

                case 2:
                    finalString += "duemila";
                    break;

                case 3:
                    finalString += "tremila";
                    break;

                case 4:
                    finalString += "quattromila";
                    break;

                case 5:
                    finalString += "cinquemila";
                    break;

                case 6:
                    finalString += "seimila";
                    break;

                case 7:
                    finalString += "settemila";
                    break;

                case 8:
                    finalString += "ottomila";
                    break;

                case 9:
                    finalString += "novemila";
                    break;
            }
            #endregion 

            #region Traduzione centinaia

            switch ((numeroUtenteIntero / 100) % 10) // prendiamo in considerazione le centinaia, il modulo serve per non prendere le migliaia ma la cifra più a destra
            {
                case 1:
                    finalString += "cento";
                    break;
                case 2:
                    finalString += "duecento";
                    break;

                case 3:
                    finalString += "trecento";
                    break;

                case 4:
                    finalString += "quattrocento";
                    break;

                case 5:
                    finalString += "cinquecento";
                    break;

                case 6:
                    finalString += "seicento";
                    break;

                case 7:
                    finalString += "settecento";
                    break;

                case 8:
                    finalString += "ottocento";
                    break;

                case 9:
                    finalString += "novecento";
                    break;

            }
            #endregion

            #region Traduzione Decine

            switch ((numeroUtenteIntero / 10) % 10) // switch che prende in considerazione le decine
            {
                case 1: // se iniziano con l'1

                    controlloNumeriSpeciali = true; // bool per controllare se sono inseriti numeri da 11-19 allora in seguito non tradurremo le unità

                    switch (numeroUtenteIntero % 10) // si controllano le unità, poichè si scrivono in modo speciale
                    {
                        case 0:
                            finalString += "dieci";
                            break;
                        case 1:
                            finalString += "undici";
                            break;
                        case 2:
                            finalString += "dodici";
                            break;
                        case 3:
                            finalString += "tredici";
                            break;
                        case 4:
                            finalString += "quattordici";
                            break;
                        case 5:
                            finalString += "quindici";
                            break;
                        case 6:
                            finalString += "sedici";
                            break;
                        case 7:
                            finalString += "diciassette";
                            break;
                        case 8:
                            finalString += "diciotto";
                            break;
                        case 9:
                            finalString += "diciannove";
                            break;
                    }
                    break;

                case 2: // per le decine che non vanno da 11-19 si concatena normalmente la decina
                    finalString += "venti";
                    break;
                case 3:
                    finalString += "trenta";
                    break;
                case 4:
                    finalString += "quaranta";
                    break;
                case 5:
                    finalString += "cinquanta";
                    break;
                case 6:
                    finalString += "sessanta";
                    break;
                case 7:
                    finalString += "settanta";
                    break;
                case 8:
                    finalString += "ottanta";
                    break;
                case 9:
                    finalString += "novanta";
                    break;

            }
            #endregion

            #region Traduzione Unità

            if (!controlloNumeriSpeciali) { // if che controlla se la cifra è zero

                if (numeroUtenteIntero == 0)
                {
                    finalString = "zero";
                }
                else // altrimenti calcoliamo le unità
                {

                    switch (numeroUtenteIntero % 10) // si calcola il modulo poichè ci serve l'ultima cifra a destra del numero
                    {

                        case 1: 

                            if (numeroUtenteIntero / 10 % 10 > 0) finalString = finalString.Substring(0, finalString.Length - 1); // sottrae dalle decine l'ultima vocale per rendere la parola grammaticalmente corretta

                            finalString += "uno";
                            break;

                        case 2:
                            finalString += "due";
                            break;

                        case 3:
                            finalString += "tre";
                            break;
                        case 4:
                            finalString += "quattro";
                            break;
                        case 5:
                            finalString += "cinque";
                            break;
                        case 6:
                            finalString += "sei";
                            break;
                        case 7:
                            finalString += "sette";
                            break;
                        case 8:
                            if (numeroUtenteIntero / 10 % 10 > 0) finalString = finalString.Substring(0, finalString.Length - 1); // taglia la vocale per rendere la stringa grammaticalmente più corretta
                            finalString += "otto";
                            break;
                        case 9:
                            finalString += "nove";
                            break;
                    }
                }
            }
            #endregion

            #region Stampa output e chiusura finestra

            Console.WriteLine("\n Il numero in lettere è -> " + finalString); // stampa output
            Console.WriteLine("\n Premere invio per chiudere la finestra ..."); // chiusura finestra
            Console.ReadKey();

            #endregion
        }
    }
}
