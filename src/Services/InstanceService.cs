using System;
using System.IO;
using System.Threading.Tasks;

namespace Suscraft.Services
{
    public class InstanceService
    {
        private readonly string _instancesRoot;

        public InstanceService()
        {
            _instancesRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppData", "instances");
            if (!Directory.Exists(_instancesRoot)) Directory.CreateDirectory(_instancesRoot);
        }

        public string CreateInstanceFolder(string instanceName)
        {
            // Create a dedicated folder for this specific instance (The Prism Way)
            string instancePath = Path.Combine(_instancesRoot, instanceName);
            if (!Directory.Exists(instancePath))
            {
                Directory.CreateDirectory(instancePath);
                // Create the .minecraft sub-folders for world/assets isolation
                Directory.CreateDirectory(Path.Combine(instancePath, "saves"));
                Directory.CreateDirectory(Path.Combine(instancePath, "screenshots"));
            }
            return instancePath;
        }
    }
}
