using Firebase.Auth;
using Newtonsoft.Json;
using PratikKargo.Model;
using PratikKargo.Services;
using PratikKargo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static PratikKargo.MainPage;
using static PratikKargo.Model.DistanceModel;

namespace PratikKargo.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public string WebAPIkey = "AIzaSyBFcUBYixnRGLBiFVmRAr9KGW0SmZs28uU";
        static public List<City> cities = new List<City>();
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();

        }
        //async void signupbutton_Clicked(System.Object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
        //        var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email.Text, password.Text);
        //        string gettoken = auth.FirebaseToken;
        //        await App.Current.MainPage.DisplayAlert("Alert", gettoken, "Ok");
        //    }
        //    catch (Exception ex)
        //    {
        //        await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
        //    }

        //}
        public List<CargoDistance> distances;

        [Obsolete]
        async void loginbutton_Clicked(System.Object sender, System.EventArgs e)
        {


          // await deneme().ConfigureAwait(false);


            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email.Text, password.Text);
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
               
                Application.Current.MainPage = new NavigationPage(new BurgerMenu());

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Invalid useremail or password", "OK");
            }
        }
        //async Task deneme()
        //{
        //    FirebaseHelper firebaseHelper = new FirebaseHelper();
        //    var allCargo = await firebaseHelper.GetAllCargo().ConfigureAwait(false);
        //    distances = new List<CargoDistance>();
        //    Random r = new Random();
        //    int index = r.Next(0, allCargo.Count);
        //    for (int i = 0; i < allCargo.Count; i++)

        //    {
        //        if (i == index)
        //        {
        //            continue;
        //        }
        //        DistanceResponseModel model = await ApiServices.ServiceClientInstance.GetDistance(allCargo[index].X,
        //           allCargo[index].Y, allCargo[i].X, allCargo[i].Y).ConfigureAwait(false);
        //        distances.Add(new CargoDistance
        //        {
        //            Distance = model.Rows.FirstOrDefault().Elements.FirstOrDefault().Distance.Value,
        //            KargoId = allCargo[i].KargoId,
        //            X = allCargo[i].X,
        //            Y = allCargo[i].Y,
        //        });
        //    }
        //    distances.Add(new CargoDistance
        //    {
        //        KargoId = allCargo[index].KargoId,
        //        Distance = 0,
        //        X = allCargo[index].X,
        //        Y = allCargo[index].Y,
        //    });
        //    distances = distances.OrderBy(a => a.Distance).Take(4).ToList();
        //    foreach (var item in distances)
        //    {
        //        StaticClass.CityList.Add(new City(new Point(Double.Parse(item.X, System.Globalization.CultureInfo.InvariantCulture), Double.Parse(item.Y, System.Globalization.CultureInfo.InvariantCulture))));
        //    }


        //    //for (int i = 0; i < allCargo.Count; i++)
        //    //{
        //    //    Cargo cargo = new Cargo();
        //    //    cargo.Adress = allCargo[i].Adress;
        //    //    cargo.PhoneNumber = allCargo[i].PhoneNumber;
        //    //    cargo.NameSurname = allCargo[i].NameSurname;
        //    //    cargo.PhoneNumber = allCargo[i].PhoneNumber;
        //    //    cargo.X = allCargo[i].X;
        //    //    cargo.Y = allCargo[i].Y;
        //    //    cargo.KargoId = allCargo[i].KargoId;


        //    //    all.Add(cargo);


        //    //}
        //    var mehmet = 0;
        //    //MainViewModel.repos = new Repo[5];
        //    //for (int i = 0; i < all.Count; i++)
        //    //{
        //    //    MainViewModel.repos[i] = new Repo();
        //    //    MainViewModel.repos[i].address = all[i].Adress;
        //    //    MainViewModel.repos[i].distance = "500m";
        //    //    MainViewModel.repos[i].NameSurname = all[i].NameSurname;
        //    //    MainViewModel.repos[i].number = all[i].KargoId;
        //    //    MainViewModel.repos[i].phoneNumber = all[i].PhoneNumber;
        //    //    MainViewModel.repos[i].X= all[i].X;
        //    //    MainViewModel.repos[i].Y= all[i].Y;
        //    //    MainPage.city_list.Add(new City(new Point(Double.Parse(all[i].X, System.Globalization.CultureInfo.InvariantCulture), Double.Parse(all[i].Y, System.Globalization.CultureInfo.InvariantCulture))));


        //    //}
        //}
    }
}