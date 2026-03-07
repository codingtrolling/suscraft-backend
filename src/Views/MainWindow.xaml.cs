using System.Windows;
using SUSCRAFT.ViewModels;

namespace SUSCRAFT.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
}
