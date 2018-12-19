namespace Sportify.Data.ViewModels.Constants
{
    internal static class Constants
    {
        // Disciplines
        public const string AddDiscipline_Display_Name = "Name";

        public const string AddDiscipline_Display_Description = "Description";

        public const string AddDiscipline_Display_Sport = "Sport";

        // Events
        public const string CreateEvent_Display_EventName = "Event Name";

        public const string CreateEvent_Regex_EventName = "[a-zA-z0-9-.*/_\\s]+";

        public const string EventNameInvalidSymbolsErrorMessage = "Event name contains invalid symbols!";

        public const string CreateEvent_Display_Date = "Date and Time of Events";

        public const string CreateEvent_Display_Oraganization = "Organization";

        public const string CreateEvent_Display_Discipline = "Discipline";

        public const string CreateEvent_Display_Venue = "Venue";

        public const string CreateEvent_Display_NumberOfParticipants = "Number of Participants";

        public const int MinNumberOfParticipants = 5;

        public const int MaxNumberOfParticipants = 100;

        // Messages
        public const string SendMessage_Display_Username = "Username";

        public const string MessageFullNameLength = "The {0} must be at least {1} characters long.";

        public const string SendMessage_Regex_FullName = "[a-zA-z0-9-.*/_\\s]+";

        public const string MessageFullNameContainsInvalidSymbols = "Full name contains invalid symbols!";

        public const string SendMessage_Display_FullName = "Full Name";

        public const string SendMessage_Display_Email = "Email";

        public const string SendMessage_Display_Subject = "Subject";

        public const string SendMessage_Display_Content = "Content";

        // Oraganizations
        public const int MinOrganizationNameLength = 3;

        public const string OrganizationNameLengthErrorMessage = "The {0} must be at least {1} characters long!";

        public const string OrganizationNameInvalidSymbolsErrorMessage = "Organization name contains invalid symbols!";

        public const string CreateOrganization_Regex_Name = "[a-zA-z0-9-.*/_\\s]+";

        public const string CreateOrganization_Display_Name = "Name";

        public const string CreateOrganization_Regex_Description = "[a-zA-z0-9-.\\s]+";

        public const string CreateOrganization_Display_Description = "Description";

        public const string OrganizationDescriptionInvalidSymbolsErrorMessage = "Organization description contains invalid symbols!";

        // Sports
        public const int MinSportNameLength = 3;

        public const string SportNameLengthErrorMessage = "The {0} must be at least {1} characters long!";

        public const string AddSport_Regex_Name = "[a-zA-z\\s]+";

        public const string SportNameInvalidSymbolsErrorMessage = "Sport name contains invalid symbols!";

        public const string AddSport_Display_Name = "Name";

        public const string AddSport_Display_Description = "Description";

        public const string AddSport_Display_ImageUrl = "Image Url";

        // Towns
        public const int MinTownNameLength = 3;

        public const string TownNameLengthErrorMessage = "The {0} must be at least {1} characters long!";

        public const string TownInvalidSymbolsErrorMessage = "Town name contains invalid symbols!";

        public const string AddTown_Regex_Name = "[a-zA-z\\s]+";

        public const string AddTown_Display_Name = "Name";

        public const string AddTown_Display_Country = "Country";

        // Users
        public const int MinPasswordLength = 4;

        public const int MaxPasswordLength = 20;

        public const string PasswordLengthErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";

        public const string ChangePassword_Display_CurrentPassword = "Current Password";

        public const string ChangePassword_Display_NewPassword = "New password";

        public const string ChangePassword_Display_ConfirmNewPassword = "Confirm new password";

        public const string ChangePassword_Compare = "NewPassword";

        public const int MinUsernameLength = 3;

        public const string UsernameLengthErrorMessage = "The {0} must be at least {1} characters long!";

        public const string UsernameInvalidSymbolsErrorMessage = "Username contains invalid symbols!";

        public const string CreateAccount_Regex_Username = "[a-zA-z0-9-.*/_]+";

        public const string CreateAccount_Display_Username = "Username";

        public const string CreateAccount_Display_Email = "Email";

        public const string CreateAccount_Display_Password = "Password";

        public const string ConfirmPasswordErrorMessage = "The password and confirmation password do not match.";

        public const string CreateAccount_Compare = "Password";

        public const string CreateAccount_Display_ConfirmPassword = "Confirm Password";

        public const string CreateAccount_Display_FirstName = "First Name";

        public const string CreateAccount_Display_LastName = "Last Name";

        public const string CreateAccount_Display_BirthDate = "Birth Date";

        public const string CreateAccount_Display_Country = "Country";

        public const string UpdateAccount_Display_PhoneNumber = "Phone Number";
    }
}