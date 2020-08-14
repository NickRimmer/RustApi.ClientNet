using System;
using System.Collections.Generic;

namespace RustApi.ClientNet.Models
{
    /// <summary>
    /// Request data for command.
    /// </summary>
    public class RustApiCommandRequest
    {
        public RustApiCommandRequest(string commandName, Dictionary<string, object> parameters)
        {
            CommandName = commandName ?? throw new ArgumentNullException(nameof(commandName));
            Parameters = parameters ?? new Dictionary<string, object>();
        }

        /// <summary>
        /// Command name.
        /// </summary>
        public string CommandName { get; }

        /// <summary>
        /// Command parameters.
        /// </summary>
        public Dictionary<string, object> Parameters { get; }
    }
}
