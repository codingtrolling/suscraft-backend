using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CmlLib.Core;
using CmlLib.Core.Version;

namespace Suscraft.ViewModels
{
    public class VersionViewModel
    {
        public ObservableCollection<string> AvailableVersions { get; set; } = new();

        public async Task LoadVersions(CMLauncher launcher)
        {
            try 
            {
                var versions = await launcher.GetAllVersionsAsync();
                foreach (var v in versions)
                {
                    if (v.MType == MVersionType.Release)
                        AvailableVersions.Add(v.Name);
                }
            }
            catch (Exception)
            {
                // Catching without variable to avoid warnings
                AvailableVersions.Add("Offline: 1.21.1");
            }
        }
    }
}
