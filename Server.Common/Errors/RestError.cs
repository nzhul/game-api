using Server.Common.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Server.Common.Errors
{
    public class RestError
    {
        public string Code { get; }

        public string Target { get; }

        public string Message { get; }

        public IEnumerable<RestError> Details { get; }

        public RestError(string code, string target, string message, params RestError[] details)
        {
            Code = code;
            Target = target;
            Message = message;

            // We need this because we don't want to get the empty arrays serialized.
            Details = details.Any() ? details : null;
        }

        public RestError(RestErrorCode code, string target, string message, params RestError[] details)
            : this(code.ToString(), target, message, details)
        {
        }
    }
}
