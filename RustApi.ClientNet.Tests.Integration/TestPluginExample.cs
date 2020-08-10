using RustApi.ClientNet.Models;

namespace RustApi.ClientNet.Tests.Integration
{
    public class TestPluginExample
    {
        [ApiCommand ("test_arguments_3", "admin")]
        public string SomeMethod2 (ApiCommandAttribute attribute, ApiUserInfo user, ApiCommandRequest request) {
            var message = string.Join(", ", request.Parameters.Keys);
            Puts(message);

            return message;
        }
    }
}
