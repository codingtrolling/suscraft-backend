using System;
using Suscraft.Services;
using CmlLib.Core.Auth;

namespace Suscraft.Core
{
    public class AppLogic
    {
        private readonly LauncherService _launcher = new();
        private readonly InstanceService _instanceManager = new();

        public async void StartGameSession(string instanceName, string version)
        {
            // 1. Get or Create the isolated folder
            string path = _instanceManager.CreateInstanceFolder(instanceName);
            
            // 2. Setup Session (Offline for now)
            var session = MSession.GetOfflineSession("Player");

            // 3. Launch!
            Console.WriteLine($"[LSO Taran Taran] Securing instance: {instanceName}");
            await _launcher.LaunchIsolated(version, path, session);
        }
    }
}
