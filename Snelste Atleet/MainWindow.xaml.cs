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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snelste_Atleet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Lijsten
        List<string> Namen = new List<string>(); //voor namen
        List<int> Tijd = new List<int>(); //Voor behaalde tijden

        private void BtnNieuwe_Click(object sender, RoutedEventArgs e)
        {
            //Fout Procedure
            if (TxtNaam.Text.Length < 3)
            {
                MessageBox.Show("Voer een naam in met minstens 3 karakters.", "Fout");
                return;
            }

            try
            {
                int i = int.Parse(TxtTijd.Text);
            }
            catch (Exception)
            {
                try
                {
                    double d = double.Parse(TxtTijd.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Voer een geldige tijd waarde in.", "Fout");
                    return;
                }

                MessageBox.Show("Voer volledige waardes in. (Geen kommagetallen)", "Fout");
                return;
            }

            //Geen fout gecatched?
            //Voeg namen en tijden in de lijsten
            Namen.Add(TxtNaam.Text);
            Tijd.Add(int.Parse(TxtTijd.Text));

            //Toon dat de namen zijn toegevoegd
            TxtResultaat.Text = $"{TxtNaam.Text} is toegevoegd met {TxtTijd.Text} seconden.";

            //Maak tekstvakken leeg en focus op naam textvak
            TxtNaam.Clear();
            TxtTijd.Clear();
            TxtNaam.Focus();
        }

        private void BtnSnelste_Click(object sender, RoutedEventArgs e)
        {
            ///Naam van de snelste atleet word gevonden op basis van index nummer
            ///Namen en tijden hebben dezelfde indexnr
            ///Er word eerst gezocht naar de indexnummer van de snelste tijd
            ///Die tijd word getoond samen met de naam die op hetzelfde indexnr staat als de snelste tijd
            ///BV. Als snelste tijd op indexnr [5] staat, word er gezocht naar de naam die ook op indexnr [5] staat

            var NL = Environment.NewLine; //For simplicity sake

            //Wie heeft de snelste tijd?
            var SnelsteTijd = Tijd.Min();

            //Op welke index staat deze tijd?
            int IndexNr = Tijd.FindIndex(Tijd => Tijd == SnelsteTijd);

            //Creer een nieuw TimeSpan
            TimeSpan time = TimeSpan.FromSeconds(SnelsteTijd);

            //Toon Resultaat
            TxtResultaat.Text = $"De snelste atleet is {Namen[IndexNr]}.{NL}" +
                $"Totaal aantal seconden: {SnelsteTijd} {NL} {NL}" +
                $"Totaal aantal uren: {time.Hours} {NL}" +
                $"Totaal aantal minuten: {time.Minutes} {NL}" +
                $"Totaal aantal seconden: {time.Seconds} {NL} {NL}" +
                $"Stopwatch View: {time}";
        }

        private void BtnWissen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u het lijst wilt wissen?", "Confirmatie", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            //Maak lijsten schoon
            Namen.Clear();
            Tijd.Clear();
            //Verwijder alle tekst
            TxtResultaat.Text = "Lijst leeg gemaakt.";
            TxtNaam.Clear();
            TxtTijd.Clear();
        }

        private void BtnAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Applicatie afsluiten?", "Confirmatie", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            Close();
        }

        private void BtnLijst_Click(object sender, RoutedEventArgs e)
        {
            var Br = Environment.NewLine;

            TxtResultaat.Text = "Namen:" + Br;
            int NI = 1; //Nr index
            foreach(string Naam in Namen)
            {
                TxtResultaat.Text += $"[{NI}] - " + Naam + Br;
                NI++;
            }

            TxtResultaat.Text += $"{Br}Tijden: {Br}";
            NI = 1;
            foreach(int T in Tijd)
            {
                TxtResultaat.Text += $"[{NI}] - " + T.ToString() + Br;
                NI++;
            }
        }
    }
}
