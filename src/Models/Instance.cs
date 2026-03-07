using System;

namespace SUSCRAFT.Models
{
    public class MinecraftInstance
    {
        public string Name { get; set; } = "New Instance";
        public string Version { get; set; } = "1.20.1";
        public string IconPath { get; set; } = "glass_block.png";
        public DateTime LastPlayed { get; set; }
    }
}
