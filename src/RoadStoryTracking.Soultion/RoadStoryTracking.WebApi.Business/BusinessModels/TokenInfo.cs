using System;

namespace RoadStoryTracking.WebApi.Business.BusinessModels
{
    public class TokenInfo
    {
        public DateTime ExpirationDate { get; private set; }
        public string Token { get; private set; }
        public string UserName { get; private set; }

        public TokenInfo(string token, DateTime expirationDate, string userName)
        {
            Token = token;
            ExpirationDate = expirationDate;
            UserName = userName;
        }
    }
}