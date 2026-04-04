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
                // Fetch the version list (This is a small JSON download)
                var versions = await launcher.GetAllVersionsAsync();
                foreach (var v in versions)
                {
                    // Only show Releases to keep the UI clean (Prism Style)
                    if (v.MType == MVersionType.Release)
                        AvailableVersions.Add(v.Name);
                }
            }
            catch (Exception ex)
            {
                // If internet is too slow/down, we show a fallback
                AvailableVersions.Add("Offline: 1.21.1");
            }
        }
    }
}
