using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Suscraft.Services;
using Suscraft.Models;
using CmlLib.Core.Auth;

namespace Suscraft.ViewModels
{
    public class MainViewModel
    {
        private readonly LauncherService _launcher = new();
        private readonly AuthService _auth = new();
        private readonly InstanceService _instanceManager = new();

        public ObservableCollection<string> LocalInstances { get; set; } = new();

        public async Task LaunchGame(string selectedVersion)
        {
            // The Prism Way: Create an isolated folder
            string instanceName = $"Instance-{selectedVersion}";
            string path = _instanceManager.CreateInstanceFolder(instanceName);

            // Using Offline for your current slow internet testing
            var session = _auth.LoginOffline("Player");

            await _launcher.LaunchIsolated(selectedVersion, path, session);
        }
    }
}
