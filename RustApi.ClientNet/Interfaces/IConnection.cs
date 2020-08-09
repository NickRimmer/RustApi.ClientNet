using System.Collections.Generic;
using System.Threading.Tasks;

namespace RustApi.ClientNet.Interfaces
{
    /// <summary>
    /// Connection to Rust API
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// Send command.
        /// </summary>
        /// <param name="parameters">Request parameters.</param>
        /// <returns></returns>
        Task SendCommandAsync(Dictionary<string, object> parameters = null);

        /// <summary>
        /// Send command.
        /// </summary>
        /// <typeparam name="TResponse">Expected response type.</typeparam>
        /// <param name="parameters">Request parameters.</param>
        /// <returns></returns>
        Task<TResponse> SendCommandAsync<TResponse>(Dictionary<string, object> parameters) where TResponse : class;
    }
}
