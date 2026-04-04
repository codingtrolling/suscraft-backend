namespace Suscraft.Models
{
    public enum AuthType { Microsoft, Offline }

    public class Account
    {
        public string Username { get; set; }
        public string UUID { get; set; }
        public string AccessToken { get; set; }
        public AuthType Type { get; set; }
        public string SkinUrl { get; set; } // For the Prism-style avatar
    }
}
