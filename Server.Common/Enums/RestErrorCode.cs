namespace Server.Common.Enums
{
    public enum RestErrorCode
    {
        Unknown,
        ValidationError,
        BadArgument,
        NullValue,
        MalformedValue,
        PasswordError,
        PasswordDoesNotMeetPolicy,
        PasswordReuseNotAllowed,
        Unauthorized,
        UserAlreadyExists,
        RemoteError
    }
}
