using System.Collections.ObjectModel;
using System.Windows.Input;
using SUSCRAFT.Models;
using SUSCRAFT.Services;

namespace SUSCRAFT.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<MinecraftInstance> Instances { get; set; }
        private InstanceService _instanceService;

        public MainViewModel()
        {
            // Pointing to the local AppData folder we created in Part 0
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SUSCRAFT", "instances");
            _instanceService = new InstanceService(path);
            
            Instances = new ObservableCollection<MinecraftInstance>();
        }

        public void AddNewInstance(string name, string version)
        {
            _instanceService.CreateInstance(name, version);
            Instances.Add(new MinecraftInstance { Name = name, Version = version });
        }
    }
}
