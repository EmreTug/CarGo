using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using PratikKargo.ViewModels;
using Plugin.Geolocator;
using static PratikKargo.MainPage;
using System.Globalization;

namespace PratikKargo
{

    [DesignTimeVisible(false)]
    public partial class MapPage : ContentPage
    {
        MapPageViewModel mapPageVModel;
        public MapPage()
        {
            InitializeComponent();
            BindingContext = mapPageVModel = new MapPageViewModel();
            ApplyMapTheme();
        }

        private void ApplyMapTheme()
        {
            var assembly = typeof(MapPage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"PratikKargo.MapResources.MapTheme.json");
            string themeFile;
            using (var reader = new System.IO.StreamReader(stream))
            {

                themeFile = reader.ReadToEnd();
                map.MapStyle = MapStyle.FromJson(themeFile);
            }

            //This is my actual location as of now we are taking it from google maps. But you have to use location plugin to generate latitude and longitude.
            var locator = CrossGeolocator.Current;
            //locator.DesiredAccuracy = 50;


            Position position1 = new Position(38.69301742336234, 35.549143170636405);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position1, Distance.FromMeters(300)));


            //map.MoveToRegion(MapSpan.FromCenterAndRadius(position1, Distance.FromMeters(1500)));
            //         await map.MoveCamera(CameraUpdateFactory.NewCameraPosition(
            //new CameraPosition(
            //    new Position(position1.Latitude, position1.Longitude), // center
            //    17d, // zoom
            //    45d, // bearing(rotation)
            //    60d)));



        }















        int nokta = 0;
        public List<Position> pathcontent = null;
        async void TrackPath_Clicked(System.Object sender, System.EventArgs e)
        {

            //if ((nokta + 2) != MainPage.city_list.Count)
            //{

            //    int start = best_tour_list[nokta];
            //    int finish = best_tour_list[nokta + 1];
            //    var startLocation = city_list[start].getLocation();
            //    var finishLocation = city_list[finish].getLocation();

            //    var pathcontent = await mapPageVModel.LoadRoute(startLocation.X.ToString(CultureInfo.GetCultureInfo("en-US")),startLocation.Y.ToString(CultureInfo.GetCultureInfo("en-US")),finishLocation.X.ToString(CultureInfo.GetCultureInfo("en-US")),finishLocation.Y.ToString(CultureInfo.GetCultureInfo("en-US")));


            //    map.Polylines.Clear();

            //    var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
            //    polyline.StrokeColor = Color.Black;
            //    polyline.StrokeWidth = 3;

            //    foreach (var p in pathcontent)
            //    {
            //        polyline.Positions.Add(p);

            //    }
            //    map.Polylines.Add(polyline);

            //    map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Xamarin.Forms.GoogleMaps.Distance.FromMiles(0.50f)));

            //    var pin = new Xamarin.Forms.GoogleMaps.Pin
            //    {
            //        Type = PinType.SearchResult,
            //        Position = new Xamarin.Forms.GoogleMaps.Position(polyline.Positions.First().Latitude, polyline.Positions.First().Longitude),
            //        Label = "Pin",
            //        Address = "Pin",
            //        Tag = "CirclePoint",
            //        Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CircleImg.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CircleImg.png", WidthRequest = 25, HeightRequest = 25 })

            //    };
            //    map.Pins.Add(pin);
            //    var positions = new Xamarin.Forms.GoogleMaps.Position(polyline.Positions.First().Latitude, polyline.Positions.First().Longitude);

            //    CameraPosition cameraPosition = new CameraPosition(positions, 45, 10, 0);

            //    CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            //    await map.MoveCamera(cameraUpdate);
            //    var positionIndex = 1;

            //    Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            //    {
            //        if (pathcontent.Count > positionIndex)
            //        {
            //            UpdatePostions(pathcontent[positionIndex]);
            //            positionIndex++;
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    });
            //}
            //nokta++;

            if ((nokta + 2) != MainPage.best_tour_list.Count)
            {

                //int start = MainPage.siralama[nokta];
                //int finish = siralama[nokta + 1];
                //var startLocation = points[start];
                //var finishLocation = points[finish];

                int start = MainPage.best_tour_list[nokta];
                int finish = best_tour_list[nokta + 1];
                var startLocation = city_list[start];
                var finishLocation = city_list[finish];



                pathcontent = await mapPageVModel.LoadRoute(startLocation.getLocation().X.ToString(CultureInfo.GetCultureInfo("en-US")), startLocation.getLocation().Y.ToString(CultureInfo.GetCultureInfo("en-US")), finishLocation.getLocation().X.ToString(CultureInfo.GetCultureInfo("en-US")), finishLocation.getLocation().Y.ToString(CultureInfo.GetCultureInfo("en-US")));

                map.Polylines.Clear();

                var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
                polyline.StrokeColor = Color.Black;
                polyline.StrokeWidth = 3;

                foreach (var p in pathcontent)
                {
                    polyline.Positions.Add(p);

                }
                map.Polylines.Add(polyline);

                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Xamarin.Forms.GoogleMaps.Distance.FromMiles(0.80f)));

                var pin = new Xamarin.Forms.GoogleMaps.Pin
                {
                    Type = PinType.SearchResult,
                    Position = new Position(polyline.Positions.First().Latitude, polyline.Positions.First().Longitude),
                    Label = "Pin",
                    Address = "Pin",
                    Tag = "CirclePoint",
                    Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CircleImg.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CircleImg.png", WidthRequest = 25, HeightRequest = 25 })

                };
                map.Pins.Add(pin);

                nokta++;

            }



        }

        void UpdatePostions(Xamarin.Forms.GoogleMaps.Position position)
        {
            if (map.Pins.Count == 1 && map.Polylines != null && map.Polylines?.Count > 1)
                return;

            var cPin = map.Pins.FirstOrDefault();

            if (cPin != null)
            {
                cPin.Position = new Xamarin.Forms.GoogleMaps.Position(position.Latitude, position.Longitude);
                cPin.Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 25, HeightRequest = 25 });
                map.MoveToRegion(MapSpan.FromCenterAndRadius(cPin.Position, Distance.FromMeters(300)));
                var previousPosition = map.Polylines?.FirstOrDefault()?.Positions?.FirstOrDefault();
                map.Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition.Value);

                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                //            var positionn = await locator.GetPositionAsync();

                //            await map.MoveCamera(CameraUpdateFactory.NewCameraPosition(
                //new CameraPosition(
                //    new Position(position.Latitude, position.Longitude), // center
                //    17d, // zoom
                //    45d, // bearing(rotation)
                //    30d)));
            }
            else
            {
                map.Polylines?.FirstOrDefault()?.Positions?.Clear();
            }
        }



        private void Start(object sender, EventArgs e)
        {
            var positionIndex = 1;

            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                if (pathcontent.Count > positionIndex)
                {
                    UpdatePostions(pathcontent[positionIndex]);
                    positionIndex++;
                    return true;
                }
                else
                {
                    return false;
                }
            });
            if (pathcontent.Count == positionIndex)
            {
                pathcontent = null;
            }



        }
    }
}

