using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Process;

namespace Suscraft.Services
{
    public class LauncherService
    {
        private readonly string _commonStorage;

        public LauncherService()
        {
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
                GameLauncherName = "SUSCRAFT",
                // Correct way to set the instance directory in 3.3.7
                Path = new MinecraftPath(instancePath)
            };

            // Using JVMArguments for any extra flags
            launchOption.JVMArguments = new string[] { $"-Djava.library.path={Path.Combine(instancePath, "natives")}" };

            var process = await launcher.CreateProcessAsync(version, launchOption);
            process.Start();
        }
    }
}
