namespace Sportify.Constants
{
    using System;

    public class Constants
    {
        public const int MinUsernameLength = 3;

        public const int MinOrganizationNameLength = 3;

        public const int MinSportNameLength = 3;

        public const int MinPasswordLength = 4;

        public const int MaxPasswordLength = 10;

        public const string UsernameLengthErrorMessage = "The {0} must be at least {1} characters long!";

        public const string UsernameInvalidSymbolsErrorMessage = "Username contains invalid symbols!";

        public const string PasswordLengthErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";

        public const string ConfirmPasswordErrorMessage = "The password and confirmation password do not match.";

        public const string TownInvalidSymbolsErrorMessage = "Town name contains invalid symbols!";

        public const string SportNameLengthErrorMessage = "The {0} must be at least {1} characters long!";

        public const string NameInvalidSymbolsErrorMessage = "Name contains invalid symbols!";

        public const string UsernameAlreadyExists = "Username already exists! Please try with another one.";

        public const string UsernameOrPasswordAreInvalid = "Username or password are invalid!";

        public const string UserDoesNotExist = "User does not exist!";

        public const string MessageIsSentSuccessfully = "Message is sent successfully!";

        public const string ProfileUpdated = "Your profile has been updated!";

        public const string PasswordWasChangedSuccessfully = "Password was changed successfully!";

        public const string PasswordWasNotChanged = "Password was not changed!";

        public const string AdministratorRole = "Administrator";

        public const string UserRole = "User";

        public const string EditorRole = "Editor";

        public const string OrganizationNameLengthErrorMessage = "The {0} must be at least {1} characters long!";

        public const string OrganizationNameInvalidSymbolsErrorMessage = "Organization name contains invalid symbols!";

        public const string OrganizationDescriptionInvalidSymbolsErrorMessage = "Organization description contains invalid symbols!";
    }
}