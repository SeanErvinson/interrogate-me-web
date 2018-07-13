using InterrogateMe.Core.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace InterrogateMe.Utilities
{
    public static class ReCaptchaHelper
    {
        private static IConfiguration _configuration;
        private const string ApiVerificationEndpoint = "https://www.google.com/recaptcha/api/siteverify";
        private static string secret;

        /// <summary>
        /// Candidate for refactor to depend less on other project's model
        /// </summary>
        /// <param name="configuration"></param>
        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
            secret = _configuration["GoogleReCaptcha:secret"];
        }

        public static bool ValidateRecaptcha(string gRecaptchaResponse)
        {
            var client = new HttpClient();
            using (var content = new FormUrlEncodedContent(new[]{
                        new KeyValuePair<string, string>("secret", secret),
                        new KeyValuePair<string, string>("response", gRecaptchaResponse) }))
            {

                var response = client.PostAsync(ApiVerificationEndpoint, content).Result;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return false;
                }
                var json = response.Content.ReadAsStringAsync().Result;
                var reCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(json);
                if (!reCaptchaResponse.Success)
                    return false;
                return true;
            }
        }
    }
}