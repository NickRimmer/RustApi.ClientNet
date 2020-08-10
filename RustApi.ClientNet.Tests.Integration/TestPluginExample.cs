using RustApi.ClientNet.Models;
//TODO add required usings

namespace RustApi.ClientNet.Tests.Integration
{
    public class TestPluginExample
    {
        private object TestHook (Dictionary<string, object> data) {
            var name = (string) data["name"];
            var value = (long) data["value"];

            Puts ($"Test hook called. Name: '{name}' ({value})");

            return new {
                Name = name,
                Value = value
            };
        }

        [ApiCommand ("test_arguments_1")]
        public void SomeMethod2 (ApiCommandAttribute attribute) {
            Puts($"Command name: {attribute.CommandName}");
        }

        [ApiCommand ("test_arguments_2", "admin")]
        public void SomeMethod2 (ApiCommandAttribute attribute, ApiUserInfo user) {
            Puts($"User name: {user.Name}");
        }

        [ApiCommand ("test_arguments_3", "admin")]
        public string SomeMethod2 (ApiCommandAttribute attribute, ApiUserInfo user, ApiCommandRequest request) {
            var message = string.Join(", ", request.Parameters.Keys);
            Puts(message);

            return message;
        }
    }
}
