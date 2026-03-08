using System.Windows;
using System.Windows.Input;
using SUSCRAFT.ViewModels;
using SUSCRAFT.Models;

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
            if (DataContext is MainViewModel vm)
            {
                await vm.LaunchSelected();
            }
        }

        private void AddInstance_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                vm.Instances.Add(new MinecraftInstance { Name = "New Sus Instance", Version = "1.20.1" });
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var settings = new SettingsWindow();
            settings.Owner = this;
            settings.ShowDialog();
        }

        private void Logo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _clickCount++;
            if (_clickCount >= 10)
            {
                _clickCount = 0;
                new EasterEggWindow().ShowDialog();
            }
        }
    }
}
