using System.Windows;
using System.Windows.Input;

namespace test_obesenec
{
    public partial class ZebricekWindow : Window
    {
        public ZebricekWindow(Hra hra)
        {
            InitializeComponent();
            DataContext = hra;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Escape)
                Close();
        }
    }
}
