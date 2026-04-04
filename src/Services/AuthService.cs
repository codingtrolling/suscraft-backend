using System.Threading.Tasks;
using CmlLib.Core.Auth;
using CmlLib.Core.Auth.Microsoft;
using XboxAuthNet.Game.Msal;

namespace Suscraft.Services
{
    public class AuthService
    {
        private readonly JELoginHandler _loginHandler;

        public AuthService()
        {
            // Setup the Microsoft Login Handler
            var builder = new JELoginHandlerBuilder();
            _loginHandler = builder.Build();
        }

        public MSession LoginOffline(string username)
        {
            // Simple offline session for local testing
            return MSession.GetOfflineSession(username);
        }

        public async Task<MSession> LoginMicrosoft()
        {
            // This opens the browser for Microsoft Login
            // Once logged in, it returns the REAL session for premium servers
            return await _loginHandler.Authenticate();
        }
    }
}
