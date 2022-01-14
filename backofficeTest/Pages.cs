namespace backofficeTest
{
    public class Pages
    {
        public const string HostUrl = "https://thman-test.onmana.space";
        private static string PagePath => $"{HostUrl}/app/index.html#/";

        public static string Ticket => $"{PagePath}ticket";
        public static string Frozen => $"{PagePath}frozen";
        public static string Fraud => $"{PagePath}fraud";
    }
}
