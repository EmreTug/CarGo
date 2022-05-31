using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PratikKargo.Constants;
using PratikKargo.Model;
using PratikKargo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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

            var response = await client.GetAsync($"api/directions/json?mode=driving&transit_routing_preference=less_driving&alternatives=true&origin={originLatitude}," +
                $"{originLongitude}&destination={destinationLatitude},{destinationLongitude}&key={AppConstants.GoogleMapsApiKey}").ConfigureAwait(false);
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

            using (var response = await client.GetAsync($"https://maps.googleapis.com/maps/api/distancematrix/json?destinations={d1},{d2}&origins={o1},{o2}&key={AppConstants.GoogleMapsApiKey}").ConfigureAwait(false))
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
        public partial class Tomtom
        {
            [JsonProperty("formatVersion")]
            public string FormatVersion { get; set; }

            [JsonProperty("routes")]
            public Route[] Routes { get; set; }
        }

        public partial class Route
        {
            [JsonProperty("summary")]
            public Summary Summary { get; set; }

            [JsonProperty("legs")]
            public Leg[] Legs { get; set; }

            [JsonProperty("sections")]
            public Section[] Sections { get; set; }
        }

        public partial class Leg
        {
            [JsonProperty("summary")]
            public Summary Summary { get; set; }

            [JsonProperty("points")]
            public Point[] Points { get; set; }
        }

        public partial class Point
        {
            [JsonProperty("latitude")]
            public double Latitude { get; set; }

            [JsonProperty("longitude")]
            public double Longitude { get; set; }
        }

        public partial class Summary
        {
            [JsonProperty("lengthInMeters")]
            public long LengthInMeters { get; set; }

            [JsonProperty("travelTimeInSeconds")]
            public long TravelTimeInSeconds { get; set; }

            [JsonProperty("trafficDelayInSeconds")]
            public long TrafficDelayInSeconds { get; set; }

            [JsonProperty("trafficLengthInMeters")]
            public long TrafficLengthInMeters { get; set; }

            [JsonProperty("departureTime")]
            public DateTimeOffset DepartureTime { get; set; }

            [JsonProperty("arrivalTime")]
            public DateTimeOffset ArrivalTime { get; set; }
        }

        public partial class Section
        {
            [JsonProperty("startPointIndex")]
            public long StartPointIndex { get; set; }

            [JsonProperty("endPointIndex")]
            public long EndPointIndex { get; set; }

            [JsonProperty("sectionType")]
            public string SectionType { get; set; }

            [JsonProperty("travelMode")]
            public string TravelMode { get; set; }
        }

        public partial class Tomtom
        {
            public static Tomtom FromJson(string json) => JsonConvert.DeserializeObject<Tomtom>(json, Converter.Settings);
        }



        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }
        public async Task<Tomtom> tomtomtom(string originLatitude, string originLongitude, string destinationLatitude, string destinationLongitude)
        {
            Tomtom tomtom = new Tomtom();
            var emre = $"https://api.tomtom.com/routing/1/calculateRoute/{originLongitude},{originLatitude}:{destinationLongitude},{destinationLatitude}/json?maxAlternatives=5&key=TGwcnziD6KjUlRF1Pn4ymNXcM2FgAAFU";
            var response = await client.GetAsync($"https://api.tomtom.com/routing/1/calculateRoute/{originLongitude},{originLatitude}:{destinationLongitude},{destinationLatitude}/json?maxAlternatives=5&key=TGwcnziD6KjUlRF1Pn4ymNXcM2FgAAFU").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    tomtom = await Task.Run(() =>
                       JsonConvert.DeserializeObject<Tomtom>(json)
                    ).ConfigureAwait(false);

                }

            }

            return tomtom;
        }

    }
}
