namespace Shared
{
    public class SD
    {
        public const string Domain = "@payroll.com";
        private static string _apiBaseUrl = "https://localhost:7262/api";

        public static string FormatUri(string url)
        {
            return string.Format("{0}{1}", _apiBaseUrl, url);
        }

        public static string SessionCurrentUser = "CurrentUser";

        public const string Status_Approved = "APP";
        public const string Status_Pending = "PEN";
        public const string Status_Rejected = "REJ";
    }
}
