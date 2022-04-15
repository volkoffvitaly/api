using System;

namespace TinkoffWatcher_Api.Models.Auth
{
    public class JwtInfoModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
