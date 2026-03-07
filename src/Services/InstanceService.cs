using System;
using System.IO;
using SUSCRAFT.Models;

namespace SUSCRAFT.Services
{
    public class InstanceService
    {
        private readonly string _basePath;

        public InstanceService(string basePath)
        {
            _basePath = basePath;
            if (!Directory.Exists(_basePath))
                Directory.CreateDirectory(_basePath);
        }

        public void CreateInstance(string name, string version)
        {
            string instancePath = Path.Combine(_basePath, name, ".minecraft");
            
            // Create the isolated folder structure
            Directory.CreateDirectory(instancePath);
            Directory.CreateDirectory(Path.Combine(instancePath, "mods"));
            Directory.CreateDirectory(Path.Combine(instancePath, "resourcepacks"));
            Directory.CreateDirectory(Path.Combine(instancePath, "saves"));

            // Note: In Part 23, we will save the instance.json here
        }
    }
}
