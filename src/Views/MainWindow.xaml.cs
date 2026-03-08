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

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is MainViewModel vm)
            {
                await vm.LaunchSelected();
            }
        }

        private void Logo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _clickCount++;
            if (_clickCount >= 10)
            {
                _clickCount = 0;
                var egg = new EasterEggWindow();
                egg.ShowDialog();
            }
        }
    }
}
