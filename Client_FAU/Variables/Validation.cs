namespace Client_FAU.Variables
{
    public static class Validation
    {
        public static bool IsLoggedIn { get; set; } = false;
        public static string AccountCode { get; set; } = string.Empty;
        public static string JwtToken { get; set; } = string.Empty;
        public static string FullName { get; set; } = string.Empty;

    }
}
