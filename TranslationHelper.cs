using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Configuration;

namespace StarCitizenTranslator
{
    public static class TranslationHelper
    {
        private const string DEEPL_API_ENDPOINT_FREE = "https://api-free.deepl.com/v2/translate";
        private const string DEEPL_API_ENDPOINT = "https://api.deepl.com/v2/translate";
        private static readonly string DEEPL_API_KEY = ConfigurationManager.AppSettings["DEEPL_API_KEY"];
        private static readonly bool free = ConfigurationManager.AppSettings["Free"] == "true";
        private static readonly string language = ConfigurationManager.AppSettings["Language"];

        public static async Task<string> TranslateText(string text)
        {
            using HttpClient client = new HttpClient();
            var requestContent = new
            {
                text = new[] { text }, // Send text as an array
                target_lang = language
            };

            string jsonContent = JsonSerializer.Serialize(requestContent);
            StringContent content = new(jsonContent, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Add("Authorization", $"DeepL-Auth-Key {DEEPL_API_KEY}");

            string endpoint = free ? DEEPL_API_ENDPOINT_FREE : DEEPL_API_ENDPOINT;
            HttpResponseMessage response = await client.PostAsync(endpoint, content);
            string responseBody = await response.Content.ReadAsStringAsync();

            var translationResponse = JsonSerializer.Deserialize<TranslationResponse>(responseBody);
            return translationResponse.translations[0].text;
        }

        class TranslationResponse
        {
            public Translation[] translations { get; set; }
        }

        class Translation
        {
            public string detected_source_language { get; set; }

            public string text { get; set; }
        }
    }
}
