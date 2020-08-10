using RustApi.ClientNet.Interfaces;
using RustApi.ClientNet.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace RustApi.ClientNet.Tests.Integration
{
    /// <summary>
    /// Connection tests.
    /// To run these tests, game server should be started with test plugin.
    /// </summary>
    public class HooksTest
    {
        /// <summary>
        /// Common connection method to post data and retrieve response
        /// </summary>
        [Fact]
        public async Task CallHook_WithParams_Expected()
        {
            // arrange
            var connection = ConnectionHelper.GetConnection();
            var name = "TestHook";
            var data = new Dictionary<string, object>
            {
                {"name", "MyNameIs"},
                {"value", 1},
            };

            // act
            var result = await connection.CallHookAsync<ExpectedResult>(name, data);

            // assert
            Assert.NotNull(result);
            Assert.Equal(data["name"], result.Name);
            Assert.Equal(data["value"], result.Value);
        }

        private class ExpectedResult
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }
    }
}
