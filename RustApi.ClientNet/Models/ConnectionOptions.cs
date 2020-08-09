using System;

namespace RustApi.ClientNet.Models
{
    /// <summary>
    /// Connection options data model.
    /// </summary>
    public class ConnectionOptions
    {
        public ConnectionOptions(string baseUrl, string userName, string secret)
        {
            BaseUrl = FormatUrl(baseUrl ?? throw new ArgumentNullException(nameof(baseUrl)));
            UserName = userName ?? string.Empty;
            Secret = secret ?? string.Empty;
        }

        /// <summary>
        /// Base url to RustApi (e.g. 'http://localhost:28017').
        /// </summary>
        public string BaseUrl { get; }

        /// <summary>
        /// Connection user name.
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// User secret key.
        /// </summary>
        public string Secret { get; }

        /// <summary>
        /// Format to general url.
        /// </summary>
        /// <param name="url">Original url.</param>
        /// <returns></returns>
        private static string FormatUrl(string url)
        {
            // trim end symbols
            var result = url.TrimEnd('/', '&', '?');

            // add 'http://' when it required
            if (!result.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) &&
                !result.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase)) result = $"http://{result}";

            return result;
        }
    }
}
