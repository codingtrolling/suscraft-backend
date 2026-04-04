using System.Threading.Tasks;
using CmlLib.Core.Auth;
using CmlLib.Core.Auth.Microsoft;

namespace Suscraft.Services
{
    public class AuthService
    {
        public MSession LoginOffline(string username)
        {
            return MSession.GetOfflineSession(username);
        }

        public async Task<MSession> LoginMicrosoft()
        {
            // Simple Login flow that doesn't rely on manual Msal namespaces
            var loginHandler = new JELoginHandlerBuilder().Build();
            return await loginHandler.Authenticate();
        }
    }
}
