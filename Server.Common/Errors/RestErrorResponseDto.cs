using Server.Common.Enums;
using Server.Common.Exceptions;
using System;

namespace Server.Common.Errors
{
    public class RestErrorResponseDto
    {
        public RestError Error { get; }

        public RestErrorResponseDto(RestException restException)
        {
            Error = restException.Error;
        }

        public RestErrorResponseDto(string target, Exception exception)
        {
            Error = new RestError(RestErrorCode.Unknown, target, exception.Message);
        }

        public RestErrorResponseDto(RestErrorCode code, string target, string message, params RestError[] errors)
        {
            Error = new RestError(code, target, message, errors);
        }
    }
}
