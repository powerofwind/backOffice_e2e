namespace backofficeTest
{
    public class Pages
    {
        public const string HostUrl = "https://thman-test.onmana.space";
        private static readonly string PagePath = $"{HostUrl}/app/index.html#/";

        public static readonly string Home = $"{PagePath}home";
        public static readonly string Ticket = $"{PagePath}ticket";
        public static readonly string Frozen = $"{PagePath}frozen";
        public static readonly string Fraud = $"{PagePath}fraud";
        public static readonly string User = $"{PagePath}user";
    }
}
