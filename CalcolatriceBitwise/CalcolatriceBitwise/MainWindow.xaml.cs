/*
    Matteo Angiolillo 3H, 2024-02-06 -- consegna : 2024-02-20

    Realizzare una calcolatrice bitwise che lavori coi bit appunto.
    Che permetta di modificare le due singole stringhe inserite con shift left e shift right, e col not
    Poi di compararle con xor, and e or.

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalcolatriceBitwise
{
 
    public partial class MainWindow : Window
    {       
        #region Finestra Principale

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Inserimento 0 - 1 della prima stringa binaria

        // quando clicco lo zero della prima stringa
        private void btnZero_First_Click(object sender, RoutedEventArgs e)
        {

            if (txtBinario_First.Text.Length < 8) // per inserire solo una stringa da 8 bit
            {
                txtBinario_First.Text += "0"; // aggiunge 0 alla txt
            }
            else
            {
                btnZero_First.IsEnabled = false; // disabilita i bottoni in caso si superino gli 8 bit
                btnUno_First.IsEnabled = false;
            }
        }

       
        // quando clicco l'uno della prima stringa
        private void btnUno_First_Click(object sender, RoutedEventArgs e)
        {

            if (txtBinario_First.Text.Length < 8)
            {
                txtBinario_First.Text += "1"; // aggiunge 0 alla txt
            }
            else
            {
                btnZero_First.IsEnabled = false; // disabilita i bottoni per non inserirne altri
                btnUno_First.IsEnabled = false;
            }
        }

        #endregion

        #region Inserimento 0 - 1 della seconda stringa binaria

        private void btnZero_Second_Click(object sender, RoutedEventArgs e)
        {
            if (txtBinario_Second.Text.Length < 8)
            {
                txtBinario_Second.Text += "0"; // aggiunge 0 alla txt
            }
            else
            {
                btnZero_Second.IsEnabled = false;
                btnUno_Second.IsEnabled = false;
            }
        }

        private void btnUno_Second_Click(object sender, RoutedEventArgs e)
        {
            if (txtBinario_Second.Text.Length < 8) // per inserire una stringa di 8 bit
            {
                txtBinario_Second.Text += "1"; // aggiunge 0 alla txt
            }
            else
            {
                btnZero_Second.IsEnabled = false;
                btnUno_Second.IsEnabled = false;
            }
        }

        #endregion

        #region Funzione che reversa la stringa per la conversione Reverse(string strIn)

        static string Reverse(string strIn) // funzione che reversa la stringa in modo da poter fare le conversioni di base
        {
            string reversedString = "";
            char[] chars = strIn.ToCharArray();
            Array.Reverse(chars);

            return new string(chars);
        }

        #endregion

        #region Funzione che converte le stringhe binarie in interi -> convertiBintoDec(str stringa)

        static int convertiBinToDec(string stringa)
        {

            int risultato = 0; // stringa finale
            int resTemp = 0; // singolo risultato da sommare alla finale

            string invertedStringa = Reverse(stringa);

            for (int i = 0; i< invertedStringa.Length; i++)
            {
                resTemp = (invertedStringa[i] - '0') * (int)Math.Pow(2, i);
                risultato += resTemp;
            }

            return risultato;
        }

        #endregion

        #region Converte da Intero a Binario e ritorna la str IntToBin(int valoreInput)

        static string IntToBin(int valoreInput) // converte da intero a binario
        {
            int resto;
            string result = "";

            while (valoreInput != 0) // conversione
            {
                resto = valoreInput % 2;

                result += resto;

                valoreInput /= 2;
            }
            return Reverse(result); // reversa tutto grazie alla funzione scritta sopra Reverse()
        }

        #endregion
             
        #region Not della prima TextBox

        private void btnNot_First_Click(object sender, RoutedEventArgs e)
        {
            string stringaIniziale = txtBinario_First.Text; // prendo la stringa iniziale

            int resIniziale = convertiBinToDec(stringaIniziale); // converto la stringa binaria a intero

            int resFinale = ~resIniziale; // not del numero intero iniziale

            resFinale = resFinale & ((1 << 8) - 1); // sistema la lunghezza 

            string stringaInvertita = IntToBin(resFinale); // stringa finale 

            txtBinario_First.Text = stringaInvertita;
        }

        #endregion
               
        #region Not della seconda TextBox

        private void btnNot_Second_Click(object sender, RoutedEventArgs e)
        {

            string stringaIniziale = txtBinario_Second.Text; // prendo la stringa iniziale

            int resIniziale = convertiBinToDec(stringaIniziale); // converto la stringa binaria a intero

            int resFinale = ~resIniziale; // not del numero intero iniziale

            resFinale = resFinale & ((1 << 8) - 1);

            string stringaInvertita = IntToBin(resFinale); // stringa finale 

            txtBinario_Second.Text = stringaInvertita;

        }

        #endregion

        #region Shifts della prima TextBox

        private void btnShiftRight_First_Click(object sender, RoutedEventArgs e)
        {
            int parametro = 0;
            
            try // gestisce le varie eccezioni dell'inserimento del parametro
            {                
                parametro = int.Parse(txtParametroS_First.Text); // parametro di shift
                if (parametro > 8 && parametro < 0)
                    throw new Exception(); // lancia un eccezione che gestiamo dopo in caso che il parametro superi gli 8 
            }
            catch (Exception)
            {
                if (parametro > 8 && parametro < 0)
                    MessageBox.Show("INSERIRE UN VALORE MINORE DI 8 E MAGGIORE DI 0"); // mostra le msgBox
                else
                    MessageBox.Show("Inserire un valore compreso tra 0 e 8");
            }

            int res = convertiBinToDec(txtBinario_First.Text) >> parametro; // fa lo shift con gli operatori bitwise

            txtBinario_First.Text = IntToBin(res).PadLeft(8 - txtParametroS_First.Text.Length, '0'); // controlla che non si superi gli 8 bit

            
        }

        private void btnShiftLeft_First_Click(object sender, RoutedEventArgs e) // è uguale allo shift right, cambia solo al direzione dello shift
        {
            int parametro = 0;

            try // gestisce le varie eccezioni dell'inserimento del parametro

            {
                parametro = int.Parse(txtParametroS_First.Text); // parametro di sfhift
                if (parametro > 8 && parametro < 0)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (parametro > 8 && parametro < 0)
                    MessageBox.Show("INSERIRE UN VALORE MINORE DI 8 E MAGGIORE DI 0");
                else
                    MessageBox.Show("Inserire un valore compreso tra 0 e 8");
            }

            int res = convertiBinToDec(txtBinario_First.Text) << parametro;


            txtBinario_First.Text = IntToBin(res).PadLeft(8 - txtParametroS_First.Text.Length, '0');
            if(txtBinario_First.Text.Length > 8)txtBinario_First.Text = txtBinario_First.Text.Substring(txtBinario_First.Text.Length-8);

        }

        #endregion

        #region Shifts della seconda TextBox

        private void btnShiftRight_Second_Click(object sender, RoutedEventArgs e)
        {
            int parametro = 0;

            try // gestisce le varie eccezioni dell'inserimento del parametro

            {
                parametro = int.Parse(txtParametroS_Second.Text); // parametro di sfhift
                if (parametro > 8 && parametro < 0)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (parametro > 8 && parametro < 0)
                    MessageBox.Show("INSERIRE UN VALORE MINORE DI 8 E MAGGIORE DI 0");
                else
                    MessageBox.Show("Inserire un valore compreso tra 0 e 8");
            }

            int res = convertiBinToDec(txtBinario_Second.Text) >> parametro;


            txtBinario_Second.Text = IntToBin(res).PadLeft(8 - txtParametroS_Second.Text.Length, '0');

        }

        private void btnShiftLeft_Second_Click(object sender, RoutedEventArgs e) // è uguale allo shift right, cambia solo al direzione dello shift
        {
            int parametro = 0;

            try // gestisce le varie eccezioni dell'inserimento del parametro

            {
                parametro = int.Parse(txtParametroS_Second.Text); // parametro di sfhift
                if (parametro > 8)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (parametro > 8)
                    MessageBox.Show("INSERIRE UN VALORE MINORE DI 8");
                else
                    MessageBox.Show("Inserire un valore compreso tra 0 e 8");
            }

            int res = convertiBinToDec(txtBinario_Second.Text) << parametro;

            txtBinario_Second.Text = IntToBin(res).PadLeft(8 - txtParametroS_Second.Text.Length, '0'); 
            if (txtBinario_Second.Text.Length > 8) txtBinario_Second.Text = txtBinario_Second.Text.Substring(txtBinario_Second.Text.Length - 8); // controlla la lunghezza ancora
        }

        #endregion

        #region comparatore And

        private void btnAnd_Click(object sender, RoutedEventArgs e)
        {

            // devo convertire tutte e due le stringhe in intero, compararle e ritornare il risultato nella textBox finale

            int res = convertiBinToDec(txtBinario_First.Text) & convertiBinToDec(txtBinario_Second.Text);
            
            if (res == 0) // se il risultato è zero
            {
                txtRisultato.Text = "0";
            }
            else 
                txtRisultato.Text = IntToBin(res);

        }

        #endregion

        #region comparatore Or

        private void btnOr_Click(object sender, RoutedEventArgs e)
        {
            int res = convertiBinToDec(txtBinario_First.Text) | convertiBinToDec(txtBinario_Second.Text); // l'operatore or bitwise

            if (res == 0)
            {
                txtRisultato.Text = "0";
            }
            else
                txtRisultato.Text = IntToBin(res);

        }

        #endregion

        #region comparatore Xor

        private void btnXor_Click(object sender, RoutedEventArgs e)
        {
            // devo convertire tutte e due le stringhe in intero, compararle e ritornare il risultato nella textBox finale

            int res = convertiBinToDec(txtBinario_First.Text) ^ convertiBinToDec(txtBinario_Second.Text); // xor bitwise

            if (res == 0)
            {
                txtRisultato.Text = "0";
            }
            else
                txtRisultato.Text = IntToBin(res); // se non è zero riporto il risultato
        }

        #endregion

        #region button Reset

        private void btnReset_Click(object sender, RoutedEventArgs e) // resetta tutto
        {
            txtBinario_First.Text = "";

            txtBinario_Second.Text = "";

            txtRisultato.Text = "";

            btnUno_First.IsEnabled = true;

            btnZero_First.IsEnabled = true;

            btnUno_Second.IsEnabled = true;

            btnZero_Second.IsEnabled = true;
        }

        #endregion
    }
}
