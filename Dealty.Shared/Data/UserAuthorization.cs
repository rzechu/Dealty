using Dealty.Shared.Interfaces;

namespace Dealty.Shared.Data
{
    public class UserAuthorization : IUserAuthorization
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}