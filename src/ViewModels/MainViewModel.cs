using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SUSCRAFT.Models;
using SUSCRAFT.Core;
using CmlLib.Core.Auth;

namespace SUSCRAFT.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<MinecraftInstance> Instances { get; set; }
        public MinecraftInstance SelectedInstance { get; set; }
        private LaunchController _launchController;

        public MainViewModel()
        {
            Instances = new ObservableCollection<MinecraftInstance>();
            _launchController = new LaunchController();
            
            // Default RAM setting: 2GB
            Instances.Add(new MinecraftInstance { Name = "Main Survival", Version = "1.20.1" });
        }

        public async Task LaunchSelected()
        {
            if (SelectedInstance == null) return;

            // Using a dummy session for now - Part 12 handles the real auth
            var session = MSession.GetOfflineSession("SusPlayer");
            
            try {
                await _launchController.StartGame(session, SelectedInstance.Version, 2048);
            } catch (Exception ex) {
                // This is where your "You think I'm stupid" error would appear
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
