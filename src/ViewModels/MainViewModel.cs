using System.Collections.ObjectModel;
using SUSCRAFT.Models;

namespace SUSCRAFT.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<MinecraftInstance> Instances { get; set; }

        public MainViewModel()
        {
            Instances = new ObservableCollection<MinecraftInstance>();
            
            // Adding a "Dummy" instance so the grid isn't empty on first run
            Instances.Add(new MinecraftInstance { Name = "Survival 1.20", Version = "1.20.1" });
            Instances.Add(new MinecraftInstance { Name = "Trolling World", Version = "1.8.9" });
        }
    }
}
