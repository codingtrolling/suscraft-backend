using System;
using System.Windows;
using System.Windows.Threading;

namespace SUSCRAFT.Core
{
    public static class SusNotification
    {
        public static void Show(string message)
        {
            // In a real WPF app, this would trigger a custom popup
            // For now, we'll use a MessageBox with a custom 'Sus' title
            MessageBox.Show(message, "SUSCRAFT - Taran Taran is Watching", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void TriggerRizz()
        {
            string[] quotes = { 
                "Rizzing up the Minecraft assets...", 
                "Taran Taran approved this launch.", 
                "Searching for hidden Diamonds (and your search history)...",
                "Successfully trolled the Microsoft login." 
            };
            Random r = new Random();
            Show(quotes[r.Next(quotes.Length)]);
        }
    }
}
