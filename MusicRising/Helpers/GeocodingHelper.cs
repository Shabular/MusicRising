using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MusicRising.Helpers
{
    public static class GeocodingHelper
    {
        private static readonly string GoogleGeocodingApiUrl = "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}";
        private static readonly string ApiKey = "NoApiKeyJetDueToGettingMyCreditCard";

        public static async Task<MapsHelper> GetCoordinatesAsync(string address)
        {
            // Mock response for when the API key is not available
            if (string.IsNullOrEmpty(ApiKey) || ApiKey == "NoApiKeyJetDueToGettingMyCreditCard")
            {
                // Return mock coordinates (e.g., for the Google HQ)
                var mapsHelper = new MapsHelper
                {
                    Address = address,
                    Latitude = 37.4224764,
                    Longitude = -122.0842499
                };
                
                return (mapsHelper); 
            }

            var url = string.Format(GoogleGeocodingApiUrl, address, ApiKey);

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var json = JObject.Parse(response);

                if (json["status"].ToString() != "OK" || !json["results"].Any())
                {
                    throw new Exception("Geocoding API response invalid or empty.");
                }

                var location = json["results"][0]["geometry"]["location"];
                double latitude = location["lat"].Value<double>();
                double longitude = location["lng"].Value<double>();

                var mapsHelper = new MapsHelper
                {
                    Address = address,
                    Latitude = latitude,
                    Longitude = longitude
                };

                return (mapsHelper);
            }
        }
    }
}