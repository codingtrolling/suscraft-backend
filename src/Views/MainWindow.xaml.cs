using System.Windows;
using System.Windows.Input;
using SUSCRAFT.ViewModels;

namespace SUSCRAFT.Views
{
    public partial class MainWindow : Window
    {
        private int _clickCount = 0;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void Logo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _clickCount++;
            if (_clickCount >= 10)
            {
                _clickCount = 0; // Reset
                var egg = new EasterEggWindow();
                egg.ShowDialog();
            }
        }
    }
}
