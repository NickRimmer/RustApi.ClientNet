using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RustApi.ClientNet.Tests.Integration
{
    /// <summary>
    /// Connection tests.
    /// To run these tests, game server should be started with test plugin.
    /// </summary>
    public class CommandsTest
    {
        /// <summary>
        /// Common connection method to post data and retrieve response
        /// </summary>
        [Fact]
        public async Task SendCommand_WithParams_Expected()
        {
            // arrange
            var connection = ConnectionHelper.GetConnection();
            var data = new Dictionary<string, object>
            {
                {"p1", "1"},
                {"p2", 1},
                {"p3", true},
            };

            // act
            var result = await connection.SendCommandAsync<string>("test_arguments_3", data);
            var expected = string.Join(", ", data.Select(x => x.Key));

            // assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, result[0]);
        }

        [Fact]
        public async Task SendCommand_SecureNoData_Empty()
        {
            // arrange
            var connection = ConnectionHelper.GetConnection();

            // act
            await connection.SendCommandAsync("test_arguments_2");
        }

        [Fact]
        public async Task SendCommand_PublicNoData_Empty()
        {
            // arrange
            var connection = ConnectionHelper.GetConnection(true);

            // act
            await connection.SendCommandAsync("test_arguments_1");
        }
    }
}
