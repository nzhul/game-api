using Server.Common.Errors;
using System;
using System.Net;

namespace Server.Common.Exceptions
{
    public class RestException : Exception
    {
        public HttpStatusCode Code { get; }

        public RestError Error { get; }

        public RestException(HttpStatusCode code, RestError error)
        {
            Code = code;
            Error = error;
        }
    }
}
