using System.Threading.Tasks;
using CmlLib.Core.Auth;
using CmlLib.Core.Auth.Microsoft;

namespace Suscraft.Services
{
    public class AuthService
    {
        public MSession LoginOffline(string username)
        {
            // Reliable offline method
            return MSession.GetOfflineSession(username);
        }

        public async Task<MSession> LoginMicrosoft()
        {
            // Uses the standard CmlLib handler which is most compatible with 0.1.3
            var loginHandler = new JELoginHandlerBuilder().Build();
            return await loginHandler.Authenticate();
        }
    }
}
