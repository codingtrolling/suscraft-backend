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
            // Global storage for JARs and Assets (to save bandwidth)
            _commonStorage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SUSCRAFT-LAUNCHER");
        }

        public async Task LaunchIsolated(string version, string instancePath, MSession session)
        {
            var path = new MinecraftPath(_commonStorage);
            var launcher = new CMLauncher(path);

            var launchOption = new MLaunchOption
            {
                Session = session,
                MaximumRamMb = 2048,
                
                // THE PRISM FINISH: 
                // All worlds and logs go to the Instance folder, NOT the global folder.
                GameLauncherName = "SUSCRAFT",
                CustomJavaArgs = new string[] { $"-Djava.library.path={instancePath}" },
                Path = new MinecraftPath(instancePath) 
            };

            // This downloads only missing global files to _commonStorage
            // but runs the game using instancePath as the root.
            var process = await launcher.CreateProcessAsync(version, launchOption);
            process.Start();
        }
    }
}
