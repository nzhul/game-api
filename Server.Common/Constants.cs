using System;
using System.Linq;

namespace Server.Common
{
    public static class Constants
    {
        public const int MinUsernameLength = 3;
        public const int MaxUsernameLength = 16;
        public const int MinPasswordLength = 4;
        public const int MaxPasswordLength = 50;


        public static string GenerateUniqueUsername(string emailAddress, string firstName, string lastName)
        {
            var domain = emailAddress.Split('@')[1];
            var randomNumber = new Random().Next(0, 100000);
            var username = $"{firstName}.{lastName}_{randomNumber}@{domain}".ToLowerInvariant();
            return username;
        }

        public static string GenerateRandomPassword(int length = 9)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string StrongPasswordValidationError =>
            $"The password must be between {MinPasswordLength} and {MaxPasswordLength} characters" +
            @" and contain at least one letter, one digit and one of these special characters !@#$%^&*()_+={}[]\|;:',.?/`~><""";

        //public static string StrongPasswordECMAScriptRegex =>
        //    $@"^(?=.*?[a-zA-Z])(?=.*?[0-9])(?=.*?[!@#$%^&*()_+={{}}\[\]\\|;:',.?\/`~><""]).{{{MinPasswordLength},{MaxPasswordLength}}}$";

        public static string EmailRegexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public static string UsernameRegex = "^[A-Za-z]{3,16}$";

        public static string UsernameValidationError =>
            $"The username must be between {MinUsernameLength} and {MaxUsernameLength} characters long" +
            @" and can contain only alphabetical letters.";
    }
}
