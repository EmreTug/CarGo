using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PratikKargo.Constants;
using PratikKargo.Models;
using static PratikKargo.Model.DistanceModel;

namespace PratikKargo.Services
{
    public class ApiServices
    {
        private JsonSerializer _serializer = new JsonSerializer();

        private static ApiServices _ServiceClientInstance;

        public static ApiServices ServiceClientInstance
        {
            get
            {
                if (_ServiceClientInstance == null)
                    _ServiceClientInstance = new ApiServices();
                return _ServiceClientInstance;
            }
        }
        private HttpClient client;
        public ApiServices()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://maps.googleapis.com/maps/");
        }

        public async Task<GoogleDirection> GetDirections(string originLatitude, string originLongitude, string destinationLatitude, string destinationLongitude)
        {
            GoogleDirection googleDirection = new GoogleDirection();

            var response = await client.GetAsync($"api/directions/json?mode=driving&transit_routing_preference=less_driving&origin={originLatitude},{originLongitude}&destination={destinationLatitude},{destinationLongitude}&key={AppConstants.GoogleMapsApiKey}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    googleDirection = await Task.Run(() =>
                       JsonConvert.DeserializeObject<GoogleDirection>(json)
                    ).ConfigureAwait(false);

                }

            }

            return googleDirection;
        }
        public async Task<DistanceResponseModel> GetDistance(string originLatitude, string originLongitude, string destinationLatitude, string destinationLongitude)
        {

            string o1 = originLatitude.Replace(",", ".");
            string o2 = originLongitude.Replace(",", ".");
            string d1 = destinationLatitude.Replace(",", ".");
            string d2 = destinationLongitude.Replace(",", ".");
            DistanceResponseModel distance = new DistanceResponseModel();

            using (var response = await client.GetAsync($"https://maps.googleapis.com/maps/api/distancematrix/json?destinations={d1},{d2}&origins={o1},{o2}&key=AIzaSyBte7LN_edqgl_jwBy09Jhn4rI7frDsQVY").ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        distance = await Task.Run(() =>
                           JsonConvert.DeserializeObject<DistanceResponseModel>(json)
                        ).ConfigureAwait(false);

                    }

                }
            }


            return distance;



        }
    }
}
