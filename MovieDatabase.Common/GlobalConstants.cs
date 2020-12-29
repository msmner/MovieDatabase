namespace MovieDatabase.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "MovieDatabase";

        public const string AdministratorRoleName = "Administrator";

        public const string UserRoleName = "User";

        public const int ItemsPerPage = 6;

        public const string EmailFormat = "<b>From</b>: {0}<br/><b>Email</b>: {1}<br/><b>Phone</b>: {2}<br/><br/>";

        public const string GoogleReCaptcha = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";

        public const string ReCaptchaExc = "The submission failed the spam bot verification. If you have " +
                            "JavaScript disabled in your browser, please enable it and try again.";

        public const string EmailSubject = "Website Inquiry";

        public const string EmailHost = "smtp.gmail.com";

        public const int EmailPort = 587;

        public const string EmailSuccessMessage = "Your message was successfully sent.";
    }
}
