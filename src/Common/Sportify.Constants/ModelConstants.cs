﻿namespace Sportify.Constants
{
    public static class ModelConstants
    {
        // Disciplines
        public const string AddDiscipline_Display_Name = "Name";

        public const string AddDiscipline_Display_Description = "Description";

        public const string AddDiscipline_Display_Sport = "Sport";

        // Events
        public const int MinNumberOfParticipants = 5;

        public const int MaxNumberOfParticipants = 100;

        public const string CreateEvent_Display_EventName = "Event Name";

        public const string CreateEvent_Regex_EventName = "[a-zA-z0-9-.*/_\\s]+";

        public const string EventNameInvalidSymbolsErrorMessage = "Event name contains invalid symbols!";

        public const string CreateEvent_Display_Date = "Date and Time of Events";

        public const string CreateEvent_Display_Oraganization = "Organization";

        public const string CreateEvent_Display_Discipline = "Discipline";

        public const string CreateEvent_Display_Venue = "Venue";

        public const string CreateEvent_Display_NumberOfParticipants = "Number of Participants";

        // Messages
        public const string SendMessage_Display_Username = "Username";

        public const string MessageFullNameLength = "The {0} must be at least {1} characters long.";

        public const string SendMessage_Regex_FullName = "[a-zA-z0-9-.*/_\\s]+";

        public const string MessageFullNameContainsInvalidSymbols = "Full name contains invalid symbols!";

        public const string SendMessage_Display_FullName = "Full Name";

        public const string SendMessage_Display_Email = "Email";

        public const string SendMessage_Display_Subject = "Subject";

        public const string SendMessage_Display_Content = "Content";

        // Organizations
        public const int MinOrganizationNameLength = 3;

        public const int MinOrganizationAbbreviationLength = 2;
        
        public const string OrganizationNameLengthErrorMessage = "The {0} must be at least {1} characters long!";

        public const string OrganizationNameInvalidSymbolsErrorMessage = "Organization name contains invalid symbols!";

        public const string CreateOrganization_Regex_Name = "[a-zA-z0-9-.*/_\\s]+";

        public const string CreateOrganization_Display_Name = "Name";

        public const string OrganizationAbbreviationLengthErrorMessage = "The {0} must be at least {1} characters long!";

        public const string OrganizationAbbreviationInvalidSymbolsErrorMessage = "Organization abbreviation contains invalid symbols!";
        
        public const string CreateOrganization_Regex_Abbreviation = "[A-Z]+";

        public const string CreateOrganization_Display_Abbreviation = "Abbreviation";

        public const string CreateOrganization_Display_Description = "Description";

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

        public const int MinUsernameLength = 3;
        
        public const string PasswordLengthErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";

        public const string ChangePassword_Display_CurrentPassword = "Current Password";

        public const string ChangePassword_Display_NewPassword = "New password";

        public const string ChangePassword_Display_ConfirmNewPassword = "Confirm new password";

        public const string ChangePassword_Compare = "NewPassword";

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

        // Venues
        public const int MinVenueNameLength = 3;

        public const int MinVenueCapacity = 1;

        public const int MaxVenueCapacity = 100000;

        public const string VenueNameLengthErrorMessage = "The {0} must be at least {1} characters long!";

        public const string AddVenue_Regex_Name = "[a-zA-z0-9-\\s]+";

        public const string VenueNameInvalidSymbolsErrorMessage = "Venue name contains invalid symbols!";

        public const string AddVenue_Display_Name = "Name";

        public const string AddVenue_Display_Address = "Address";

        public const string AddVenue_Display_Capacity = "Capacity";

        public const string AddVenue_Display_ImageUrl = "Image Url";

        public const string AddVenue_Display_Town = "Town";
    }
}