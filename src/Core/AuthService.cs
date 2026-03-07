using System;
using System.Threading.Tasks;
using CmlLib.Core.Auth;
using CmlLib.Core.Auth.Microsoft;

namespace SUSCRAFT.Core
{
    public class AuthService
    {
        public MSession LoginOffline(string username)
        {
            return MSession.GetOfflineSession(username);
        }

        public async Task<MSession> LoginMicrosoft()
        {
            try 
            {
                // This is a placeholder for the login logic
                // GitHub Actions will build this into a real Windows tool
                throw new Exception("GameNotFound"); 
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("GameNotFound"))
                {
                    throw new Exception("You think that i'm stupid, you don't own that game, buy it in minecraft.net");
                }
                throw;
            }
        }
    }
}
