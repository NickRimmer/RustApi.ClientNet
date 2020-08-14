using Newtonsoft.Json;
using RustApi.ClientNet.Interfaces;
using RustApi.ClientNet.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RustApi.ClientNet
{
    /// <inheritdoc />
    public class RustApiClient : IRustApiClient
    {
        private const string UserHeaderName = "ra_u";
        private const string SecretHeaderName = "ra_s";

        private readonly RustApiClientOptions _clientOptions;

        public RustApiClient(RustApiClientOptions clientOptions)
        {
            _clientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
        }

        /// <inheritdoc />
        public async Task SendCommandAsync(string commandName, Dictionary<string, object> parameters = null)
        {
            await SendCommandAsync<NoResponse>(commandName, parameters);
        }

        /// <inheritdoc />
        public Task<TResponse[]> SendCommandAsync<TResponse>(string commandName, Dictionary<string, object> parameters = null) 
        {
            const string route = "command";
            var requestData = new RustApiCommandRequest(commandName, parameters ?? new Dictionary<string, object>());

            var result = PostDataAsync<TResponse[]>(route, requestData);
            return result;
        }

        /// <inheritdoc />
        public async Task CallHookAsync(string hookName, Dictionary<string, object> parameters = null)
        {
            await CallHookAsync<NoResponse>(hookName, parameters);
        }

        /// <inheritdoc />
        public Task<TResponse> CallHookAsync<TResponse>(string hookName, Dictionary<string, object> parameters = null)
        {
            const string route = "hook";
            var requestData = new RustApiHookRequest(hookName, parameters ?? new Dictionary<string, object>());

            var result = PostDataAsync<TResponse>(route, requestData);
            return result;
        }

        /// <inheritdoc />
        public Task<string> SystemPing() => PostDataAsync<string>("system/ping");

        /// <summary>
        /// Post data to remote.
        /// </summary>
        /// <typeparam name="TResponse">Expected response type</typeparam>
        /// <param name="route">Endpoint url.</param>
        /// <param name="data">Data to send.</param>
        /// <returns></returns>
        private async Task<TResponse> PostDataAsync<TResponse>(string route, object data = default)
        {
            using (var client = BuildClient())
            {
                var url = $"{_clientOptions.BaseUrl}/{route}";
                var body = data == default
                    ? string.Empty
                    : JsonConvert.SerializeObject(data);

                var response = await client.UploadStringTaskAsync(url, body);
                var result = BuildResponse<TResponse>(response);

                return result;
            }
        }

        /// <summary>
        /// Build client for connection.
        /// </summary>
        /// <returns></returns>
        private WebClient BuildClient()
        {
            var client = new WebClient();

            client.BaseAddress = _clientOptions.BaseUrl;
            client.Encoding = Encoding.UTF8;
            client.Headers.Add(UserHeaderName, _clientOptions.UserName);
            client.Headers.Add(SecretHeaderName, _clientOptions.Secret);

            return client;
        }

        /// <summary>
        /// Build response object from string.
        /// </summary>
        /// <typeparam name="TResponse">Expected response type.</typeparam>
        /// <param name="clientResponse">Original response string.</param>
        /// <returns></returns>
        private static TResponse BuildResponse<TResponse>(string clientResponse)
        {
            if (string.IsNullOrEmpty(clientResponse)) return default;
            if (typeof(TResponse) == typeof(NoResponse)) return default;
            //if (typeof(TResponse) == typeof(string)) return clientResponse as TResponse;

            var result = JsonConvert.DeserializeObject<TResponse>(clientResponse);
            return result;
        }

        private abstract class NoResponse
        {

        }
    }
}
