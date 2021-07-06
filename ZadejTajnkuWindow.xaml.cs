using System.Windows;
using System.Windows.Input;

namespace test_obesenec
{
    public partial class ZadejTajnkuWindow : Window
    {
        public ZadejTajnkuWindow()
        {
            InitializeComponent();
            zadatTajenkuTextBox.Focus();
        }

        private void potvrdZadaniTajenkyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(zadatTajenkuTextBox.Text))
                Close();
        }

        private void zadatTajenkuTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                potvrdZadaniTajenkyButton_Click(sender, e);
        }
    }
}
