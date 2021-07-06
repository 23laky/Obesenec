using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace test_obesenec
{
    public partial class ZadejPrezdivkuWindow : Window
    {
        public ZadejPrezdivkuWindow(Obesenec obesenec, bool vyhra, Hra hra)
        {
            InitializeComponent();
            DataContext = obesenec;
            Vyhra = vyhra;
            Obesenec = obesenec;
            Hra = hra;
            if (vyhra)
            {
                ProhralVyhralLabel.Content = "Vyhrál jsi";
                ProhralVyhralLabel.Foreground = Brushes.DarkOliveGreen;
                skoreLabel.Content = obesenec.VypocitejSkore();
                prezdivkaTextBox.Focus();
            }
            else //prohra
            {
                zadejPrezdivkuLabel.Visibility = Visibility.Collapsed;
                prezdivkaTextBox.Visibility = Visibility.Collapsed;
                tvojeSkoreLabel.Visibility = Visibility.Collapsed;
                skoreLabel.Visibility = Visibility.Collapsed;
            }
        }
        private bool Vyhra;
        private Obesenec Obesenec;
        private Hra Hra;
        private void prezdivkaOkButton_Click(object sender, RoutedEventArgs e)
        {
            if (Vyhra)
            {
                Hra.ZapisDoZebricku((prezdivkaTextBox.Text, Obesenec.VypocitejSkore()));
                Hra.Serializuj();
                ZebricekWindow zebricek = new(Hra);
                zebricek.ShowDialog();
            }
            Close();
        }

        private void prezdivkaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                prezdivkaOkButton_Click(sender, e);
        }
    }
}
