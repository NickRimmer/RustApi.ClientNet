using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RustApi.ClientNet.Interfaces;
using RustApi.ClientNet.Models;

namespace RustApi.ClientNet
{
    /// <inheritdoc />
    public class Connection: IConnection
    {
        private const string UserHeaderName = "ra_u";
        private const string SecretHeaderName = "ra_s";

        private readonly ConnectionOptions _options;

        public Connection(ConnectionOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <inheritdoc />
        public async Task SendCommandAsync(Dictionary<string, object> parameters = null)
        {
            await SendCommandAsync<string>(parameters);
        }

        /// <inheritdoc />
        public Task<TResponse> SendCommandAsync<TResponse>(Dictionary<string, object> parameters) where TResponse: class
        {
            const string route = "command";
            var result = PostDataAsync<TResponse>(route, parameters);

            return result;
        }

        /// <summary>
        /// Post data to remote.
        /// </summary>
        /// <typeparam name="TResponse">Expected response type</typeparam>
        /// <param name="route">Endpoint url.</param>
        /// <param name="data">Data to send.</param>
        /// <returns></returns>
        private async Task<TResponse> PostDataAsync<TResponse>(string route, object data = default) where TResponse: class
        {
            using (var client = BuildClient())
            {
                var url = $"{_options.BaseUrl}/{route}";
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

            client.BaseAddress = _options.BaseUrl;
            client.Encoding = Encoding.UTF8;
            client.Headers.Add(UserHeaderName, _options.UserName);
            client.Headers.Add(SecretHeaderName, _options.Secret);

            return client;
        }

        /// <summary>
        /// Build response object from string.
        /// </summary>
        /// <typeparam name="TResponse">Expected response type.</typeparam>
        /// <param name="clientResponse">Original response string.</param>
        /// <returns></returns>
        private static TResponse BuildResponse<TResponse>(string clientResponse) where TResponse: class
        {
            if (string.IsNullOrEmpty(clientResponse)) return default;
            if (typeof(TResponse) == typeof(string)) return clientResponse as TResponse;

            var result = JsonConvert.DeserializeObject<TResponse>(clientResponse);
            return result;
        }
    }
}
