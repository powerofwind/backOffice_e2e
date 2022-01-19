namespace backofficeTest
{
    public class Pages
    {
        public const string HostUrl = "https://thman-test.onmana.space";
        private static readonly string PagePath = $"{HostUrl}/app/index.html#/";

        public static string Home = $"{PagePath}home";
        public static string Ticket = $"{PagePath}ticket";
        public static string Frozen = $"{PagePath}frozen";
        public static string Fraud = $"{PagePath}fraud";
        public static string User = $"{PagePath}user";
    }
}
