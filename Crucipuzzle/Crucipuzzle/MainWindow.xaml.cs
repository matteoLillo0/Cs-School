/*
    Matteo Angiolillo, 3°H, 2024-03-26
    Realizzare un programma wpf che risolva il gioco del crucipuzzle, ovvero che trovi da una matrice le parole di una lista, e evidenzi la parola misteriosa
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Crucipuzzle
{
    public partial class MainWindow : Window
    {

        #region variabili globali

        string nomeFile; // nome del file
        Button[,] btns; // matrice di bottoni
        string[] parole; // lista delle parole
        char[,] lettere; // lista dei caratteri
        bool[,] corrispondenzeTrovate; // matrice parallela di bool
        int height, width; // dimensioni delle matrici
        bool isCaricata = false; // controllo per lettura

        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Click btn SelezionaFile

        private void btn_SelezionaFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog(); // apre la finestra per scegliere il file

            bool? result = dialog.ShowDialog(); 

            if (result == null || result == false) return;

            nomeFile = dialog.FileName;
            string[] righeTabella;
            using (StreamReader reader = new StreamReader(nomeFile)) // legge il file
            {
                string s = reader.ReadToEnd(); // fino alla fine
                righeTabella = s.Split("\n"); // splitta a capo
                reader.Close(); // importante: chiude il file
            }

            int i; // dichiaro fuori così da riutilizzarlo

            for (i = 0; i < righeTabella.Length; i++)
            {
                if (righeTabella[i] == "\r")
                {
                    break;
                }
            }

            width = righeTabella[0].Length - 1; // trova la "width" della matrice 
            height = i; // trova la "height" della matrice, e 

            lettere = new char[width, height]; // li assegna come parametri della dimensione della matrice

            for (i = 0; i < righeTabella.Length; i++) // cicla per la lunghezza di righe tabella
            {
                if (righeTabella[i] == "\r") // il carattere che suddivide la tabella dalle parole
                {
                    break;
                }
                for (int j = 0; j < width; j++)
                {
                    lettere[j, i] = righeTabella[i][j]; // prende i caratteri della lista righe e li mette in una matrice di caratteri
                }
            }

            i++;

            if (righeTabella.Length - height <= 0) // in caso il file non sia scritto in modo corretto la lettura sarà invalidata
            {
                MessageBox.Show("File non valido...");
                return;
            }

            parole = new string[righeTabella.Length - height - 1]; // crea la nuova matrice di parole

            txt_ListaParole.Text = "Lista di parole:\n";

            for (; i < righeTabella.Length; i++) // ciclo per aggiungere le parole, anche al text della label
            {
                parole[i - height - 1] = righeTabella[i];
                parole[i - height - 1] = parole[i - height - 1].Replace("\r", string.Empty);
                txt_ListaParole.Text += parole[i - height - 1] + "\n";
            }

            CreateGrid();
        }

        #endregion

        #region Click btn Risolvi

        private void btn_Risolvi_Click(object sender, RoutedEventArgs e)
        {
            if (!isCaricata) // in caso la lettura non vada a buon fine
            {
                MessageBox.Show("Caricare un file di testo valido.");
                return;
            }

            corrispondenzeTrovate = new bool[width, height]; // matrice di bool per le corrispondenze

            foreach (string s in parole) // qui vengono fatti tutti i metodi di ricerca
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (Search(i, j, 1, 0, s)) break; // i metodi di ricerca, che variano in base ai parametri di posizione, direzione, e parola
                        if (Search(i, j, -1, 0, s)) break; // se la ricerca ritorna true usciamo dal ciclo
                        if (Search(i, j, 0, 1, s)) break;
                        if (Search(i, j, 0, -1, s)) break;
                        if (Search(i, j, 1, 1, s)) break;
                        if (Search(i, j, -1, 1, s)) break;
                        if (Search(i, j, 1, -1, s)) break;
                        if (Search(i, j, -1, -1, s)) break;
                    }
                }
            }

            string soluzione = ""; // stringa vuota della soluuzione

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (!corrispondenzeTrovate[j, i]) soluzione += lettere[j, i].ToString(); // assegna i caratteri della soluzione dalla matrice alla stringa soluzione
                }
            }

            txt_Soluzione.Content += "La soluzione è: \n" + soluzione; // stampa la soluzione
        }

        #endregion

        #region Metodo di ricerca Generico

        bool Search(int x, int y, int xDir, int yDir, string word){ // metodo di ricerca 

            int x1 = x, y1 = y;

            for (int i = 0; i < word.Length; i++) // si ripete per la lunghezza della parola
            {
                if (x1 < 0 || x1 >= lettere.GetLength(0) || y1 < 0 || y1 >= lettere.GetLength(1)) return false; // controlla se sei fuori dai limiti
                
                if (word[i] == lettere[x1, y1])
                {
                    x1 += xDir; y1 += yDir;
                }
                else return false;
            }
                        

            for (int i = 0; i < word.Length; i++) // cicla per la lunghezza della parola
            {
                x1 -= xDir; y1 -= yDir;

                corrispondenzeTrovate[x1, y1] = true;
                              
                btns[y1, x1].BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#eb2d59");
                btns[y1, x1].Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ed87ab");
            }

            return true;
        }

        #endregion

        #region Creazione Griglia di btn

        private void CreateGrid() // funzione che crea la griglia di bottoni 
        {
            btns = new Button[height, width];
            btnGrid.Height = height * 30;
            btnGrid.Width = width * 30;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Button b = new Button(); // genera i bottoni con tutte le sue caratteristiche
                    b.Content = lettere[j, i];
                    b.BorderBrush = Brushes.DarkGreen;
                    b.BorderThickness = new Thickness(1.5, 1.5, 1.5, 1.5);
                    b.Background = Brushes.LightGreen;
                    b.Width = 29;
                    b.Height = 29;
                    b.HorizontalAlignment = HorizontalAlignment.Left;
                    b.VerticalAlignment = VerticalAlignment.Top;
                    b.Margin = new Thickness(j * 30, i * 30, 0, 0);
                    b.Tag = i + "," + j;
                    btns[i, j] = b;
                    btnGrid.Children.Add(b);
                }
            }

            isCaricata = true;
        }

        #endregion
    }
}
