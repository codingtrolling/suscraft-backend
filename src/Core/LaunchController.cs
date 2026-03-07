using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CmlLib.Core;
using CmlLib.Core.Auth;

namespace SUSCRAFT.Core
{
    public class LaunchController
    {
        public async Task StartGame(MSession session, string version, int ramMb)
        {
            var path = new MinecraftPath(System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SUSCRAFT"));
            var launcher = new CMLauncher(path);

            var launchOption = new MLaunchOption
            {
                MaximumRamMb = ramMb,
                Session = session,
                JavaPath = JavaDetector.GetJavaPath(),
                VersionType = "SUSCRAFT-Launcher"
            };

            var process = await launcher.CreateProcessAsync(version, launchOption);
            process.Start();
        }
    }
}
