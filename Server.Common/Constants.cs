using System.Linq;
using System;

namespace Server.Common
{
    public static class Constants
    {
        public const int MinUsernameLength = 3;
        public const int MaxUsernameLength = 16;
        public const int MinPasswordLength = 3;
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

        public static string EmailRegexPattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[-_0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public static string UsernameRegex = "^[A-Za-z]{3,16}$";

        public static string UsernameValidationError =>
            $"The username must be between {MinUsernameLength} and {MaxUsernameLength} characters long" +
            @" and can contain only alphabetical letters.";
    }
}
