using RustApi.ClientNet.Interfaces;
using RustApi.ClientNet.Models;

namespace RustApi.ClientNet.Tests.Integration
{
    public static class ConnectionHelper
    {
        public const string BaseUrl = "http://localhost:28017";
        public const string UserName = "admin";
        public const string UserSecret = "secret1";    

        public static IRustApiClient GetConnection(bool isAnonymous = false)
        {
            var options = isAnonymous
                ? new RustApiClientOptions(ConnectionHelper.BaseUrl, string.Empty, string.Empty)
                : new RustApiClientOptions(ConnectionHelper.BaseUrl, ConnectionHelper.UserName, ConnectionHelper.UserSecret);

            IRustApiClient rustApiClient = new RustApiClient(options);
            return rustApiClient;
        }
    }
}
