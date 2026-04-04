using System.Windows;
using Suscraft.ViewModels;

namespace Suscraft.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private async void OnPlayClicked(object sender, RoutedEventArgs e)
        {
            string version = VersionSelector.Text;
            if (string.IsNullOrEmpty(version) || version == "Select Version...")
            {
                MessageBox.Show("Please select or type a version first!", "LSO Taran Taran Security");
                return;
            }

            // Trigger the launch logic
            await _viewModel.LaunchGame(version);
        }
    }
}
