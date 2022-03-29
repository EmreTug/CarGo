using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PratikKargo.Models;
using PratikKargo.Services;

namespace PratikKargo.ViewModels
{
    public class MapPageViewModel
    {
        public MapPageViewModel()
        {
        }


        public class VehicleLocations
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }


        internal async Task<System.Collections.Generic.List<Xamarin.Forms.GoogleMaps.Position>> LoadRoute(string start1, string start2, string finish1, string finish2)
        {
            var googleDirection = await ApiServices.ServiceClientInstance.GetDirections(start1, start2,finish1,finish2);
            if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
            {
                var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points)));
                return positions;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", ".", "Ok");
                return null;

            }

        }
    }

}
