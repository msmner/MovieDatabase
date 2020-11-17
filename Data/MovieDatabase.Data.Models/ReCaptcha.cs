namespace MovieDatabase.Data.Models
{
    public class ReCaptcha
    {
        public string Success { get; set; }

        public string Score { get; set; }

        public string ChallengeTs { get; set; }

        public string HostName { get; set; }
    }
}
