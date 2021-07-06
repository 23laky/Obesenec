using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Input;


namespace test_obesenec
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ZadejTajnkuWindow tajenkaWindow = new();
            Hra = new();
            tajenkaWindow.ShowDialog();
            Obesenec = new(tajenkaWindow.zadatTajenkuTextBox.Text.ToLower());
            Obesenec.ZmenaTajenky += Aktualizuj;
            Hra.Deserializuj();
            DataContext = Obesenec;
            tipTextBox.Focus();
        }

        private Hra Hra { get; set; }
        private Obesenec Obesenec { get; set; }

        private void tipnoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tipTextBox.Text) && tipTextBox.Text.First() != '*') //validace
            {
                if (Obesenec.Tajenka.Contains(tipTextBox.Text.ToLower())) //dobrý tip
                {
                    string s = "";
                    int index = 0;
                    foreach (char ch in Obesenec.Tajenka)
                    {
                        if (ch == tipTextBox.Text.ToLower().First() || Obesenec.SkrytaTajenka[index] != '*')
                            s += ch;
                        else
                            s += '*';
                        index++;
                    }
                    Obesenec.SkrytaTajenka = s;
                    tajenkaLabel.Content = Obesenec.SkrytaTajenka;
                    if (!Obesenec.SkrytaTajenka.Contains('*')) //výhra
                        Hra.SkonciHru(Obesenec, true, Hra);
                }
                else //špatný tip
                {
                    Obesenec.NastavPokusy(Obesenec.Pokus + 1);
                    ZobrazObrazek(Obesenec.Pokus);
                    if (Obesenec.Pokus == 12) //prohra
                        Hra.SkonciHru(Obesenec, false, Hra);
                }
                tipTextBox.Text = "";
                tipTextBox.Focus(); //nastavíme focus zpět na textbox
            }
        }

        private void ZobrazObrazek(int pocetChyb)
        {
            obesenecImage1.Visibility = pocetChyb > 0 ? Visibility.Visible : Visibility.Hidden;
            obesenecImage2.Visibility = pocetChyb > 1 ? Visibility.Visible : Visibility.Hidden;
            obesenecImage3.Visibility = pocetChyb > 2 ? Visibility.Visible : Visibility.Hidden;
            obesenecImage4.Visibility = pocetChyb > 3 ? Visibility.Visible : Visibility.Hidden;
            obesenecImage5.Visibility = pocetChyb > 4 ? Visibility.Visible : Visibility.Hidden;
            obesenecImage6.Visibility = pocetChyb > 5 ? Visibility.Visible : Visibility.Hidden;
            obesenecImage7.Visibility = pocetChyb > 6 ? Visibility.Visible : Visibility.Hidden;
            obesenecImage8.Visibility = pocetChyb > 7 ? Visibility.Visible : Visibility.Hidden;
            obesenecImage9.Visibility = pocetChyb > 8 ? Visibility.Visible : Visibility.Hidden;
            obesenecImage10.Visibility = pocetChyb > 9 ? Visibility.Visible : Visibility.Hidden;
            obesenecImage11.Visibility = pocetChyb > 10 ? Visibility.Visible : Visibility.Hidden;
        }

        private void AktualizujTajenku()
        {
            tajenkaLabel.Content = Obesenec.SkrytaTajenka;
        }

        private void Aktualizuj(object sender, EventArgs e)
        {
            AktualizujTajenku();
            ZobrazObrazek(Obesenec.Pokus);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ZebricekWindow zebricek = new(Hra);
            zebricek.ShowDialog();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                tipnoutButton_Click(sender, e);
        }
    }


    public class Obesenec
    {
        public event EventHandler ZmenaTajenky;

        public int Pokus { get; private set; }
        public string Tajenka { get; set; }
        public string StaraTajenka { get; set; }
        public string SkrytaTajenka { get; set; }
        public Obesenec(string tajenka)
        {
            Pokus = 0;
            Tajenka = tajenka;
            for (int i = 0; i < tajenka.Length; i++)
            {
                SkrytaTajenka += @"*";
            }
        }

        public int VypocitejSkore() //délka tajenky * úspěšnost ve zbylých pokusech
        {
            return Tajenka.Length * (10 - Pokus);
        }

        public void NastavPokusy(int pokusy)
        {
            Pokus = pokusy;
        }

        protected void PriZmeneTajenky(EventArgs e)
        {
            if (Tajenka != StaraTajenka)
                ZmenaTajenky?.Invoke(this, e);
        }
        public void NastavNovouTajenku(string tajenka)
        {
            StaraTajenka = Tajenka;
            Tajenka = tajenka;
            SkrytaTajenka = "";
            for (int i = 0; i < tajenka.Length; i++)
            {
                SkrytaTajenka += @"*";
            }
            PriZmeneTajenky(EventArgs.Empty);
        }
    }

    public class Hra
    {
        public Hra() { Aktivni = true; Zebricek = new(); }

        public static List<(string, int)> Zebricek { get; private set; }

        public static bool Aktivni { get; private set; }

        public List<Tuple<string, int>> Zebrik
        {
            get
            {
                List<Tuple<string, int>> zebrik = new();
                foreach ((string, int) tuple in Zebricek)
                {
                    var item = new Tuple<string, int>(tuple.Item1, tuple.Item2);
                    zebrik.Add(item);
                }
                return zebrik;
            }
        }

        public static void SkonciHru(Obesenec obesenec, bool vyhra, Hra hra)
        {
            if (Hra.Aktivni)
            {
                ZadejPrezdivkuWindow zadejPrezdivku = new(obesenec, vyhra, hra);
                zadejPrezdivku.ShowDialog();
                Serializuj();
            }
            Hra.Aktivni = false;
            if (MessageBox.Show("Začít novou hru?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Hra.Aktivni = true;
                obesenec.NastavPokusy(0);
                ZadejTajnkuWindow zadejNovouTajenku = new();
                zadejNovouTajenku.ShowDialog();
                obesenec.NastavNovouTajenku(zadejNovouTajenku.zadatTajenkuTextBox.Text.ToLower());
            }
        }

        public static void Serializuj()
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                @"23lakySoft\")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                @"23lakySoft\"));
            XmlSerializer serializer = new(Zebricek.GetType());
            using (StreamWriter writer = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                @"23lakySoft\Sibenice.xml")))
            {
                serializer.Serialize(writer, Zebricek);
            }
        }

        public static void Deserializuj()
        {
            XmlSerializer serializer = new(Zebricek.GetType());
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                @"23lakySoft\Sibenice.xml")))
            {
                using (StreamReader reader = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    @"23lakySoft\Sibenice.xml")))
                {
                    NactiZebricek((List<(string, int)>)serializer.Deserialize(reader));
                }
            }
        }

        public static void NactiZebricek(List<(string, int)> list) => Zebricek = list;

        public static void ZapisDoZebricku((string, int) tuple)
        {
            //přidání skóre a seřazení žebříčku
            Zebricek.Add(tuple);
            var dotaz = from z in Zebricek
                        orderby z.Item2 descending
                        select z;
            Zebricek = dotaz.ToList();
        }
    }
}
