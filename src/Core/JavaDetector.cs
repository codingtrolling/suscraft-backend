using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace SUSCRAFT.Core
{
    public static class JavaDetector
    {
        public static string GetJavaPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Check Registry for Java installation
                using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment"))
                {
                    if (key != null) return key.GetValue("JavaHome") + @"\bin\javaw.exe";
                }
            }
            return "javaw"; // Fallback to system path
        }
    }
}
