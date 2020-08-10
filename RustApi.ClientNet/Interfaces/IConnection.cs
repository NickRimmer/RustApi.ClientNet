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
        /// <param name="commandName">Command name.</param>
        /// <param name="parameters">Request parameters.</param>
        /// <returns></returns>
        Task SendCommandAsync(string commandName, Dictionary<string, object> parameters = null);

        /// <summary>
        /// Send command.
        /// </summary>
        /// <typeparam name="TResponse">Expected response type.</typeparam>
        /// <param name="commandName">Command name.</param>
        /// <param name="parameters">Request parameters.</param>
        /// <returns></returns>
        Task<TResponse[]> SendCommandAsync<TResponse>(string commandName, Dictionary<string, object> parameters) where TResponse : class;

        /// <summary>
        /// Send command.
        /// </summary>
        /// <param name="hookName">Command name.</param>
        /// <param name="parameters">Request parameters.</param>
        /// <returns></returns>
        Task CallHookAsync(string hookName, Dictionary<string, object> parameters = null);

        /// <summary>
        /// Send command.
        /// </summary>
        /// <typeparam name="TResponse">Expected response type.</typeparam>
        /// <param name="hookName">Command name.</param>
        /// <param name="parameters">Request parameters.</param>
        /// <returns></returns>
        Task<TResponse> CallHookAsync<TResponse>(string hookName, Dictionary<string, object> parameters) where TResponse : class;

        /// <summary>
        /// Connection test method.
        /// </summary>
        /// <returns></returns>
        Task<string> SystemPing();
    }
}
