using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SUSCRAFT.Models;
using SUSCRAFT.Core;

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
            Instances.Add(new MinecraftInstance { Name = "Main Survival", Version = "1.20.1" });
        }

        public async Task LaunchSelected()
        {
            if (SelectedInstance == null) {
                SusNotification.Show("Select an instance first, you absolute troll.");
                return;
            }

            // Trigger the Sus Moment
            SusNotification.TriggerRizz();

            // Actual launch logic
            var session = CmlLib.Core.Auth.MSession.GetOfflineSession("SusPlayer");
            await _launchController.StartGame(session, SelectedInstance.Version, 2048);
        }
    }
}
