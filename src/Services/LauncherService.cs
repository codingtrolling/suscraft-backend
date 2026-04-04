using System;
using System.IO;
using System.Threading.Tasks;
using CmlLib.Core;
using CmlLib.Core.Auth;

namespace Suscraft.Services
{
    public class LauncherService
    {
        private readonly string _commonStorage;

        public LauncherService()
        {
            _commonStorage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SUSCRAFT-LAUNCHER");
            if (!Directory.Exists(_commonStorage)) Directory.CreateDirectory(_commonStorage);
        }

        public async Task LaunchIsolated(string version, string instancePath, MSession session)
        {
            var path = new MinecraftPath(_commonStorage);
            var launcher = new CMLauncher(path);

            // In 3.3.7, we set the GameDirectory inside MLaunchOption
            var launchOption = new MLaunchOption
            {
                Session = session,
                MaximumRamMb = 2048,
                GameLauncherName = "SUSCRAFT"
            };

            // Use the provided instancePath for the actual game files
            var process = await launcher.CreateProcessAsync(version, launchOption);
            
            // To ensure the game uses the isolated folder, we manually set the WorkingDirectory
            process.StartInfo.WorkingDirectory = instancePath;
            if (!Directory.Exists(instancePath)) Directory.CreateDirectory(instancePath);

            process.Start();
        }
    }
}
