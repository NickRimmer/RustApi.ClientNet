using System.Threading.Tasks;
using Xunit;

namespace RustApi.ClientNet.Tests.Integration
{
    public class SystemTest
    {
        [Fact]
        public async Task Ping_Default_Expected()
        {
            // arrange
            var connection = ConnectionHelper.GetConnection();

            // act
            var result = await connection.SystemPing();

            // assert
            Assert.Equal("Pong", result);
        }
    }
}
