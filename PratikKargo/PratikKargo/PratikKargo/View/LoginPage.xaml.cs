using Firebase.Auth;
using Newtonsoft.Json;
using PratikKargo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PratikKargo.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public string WebAPIkey = "AIzaSyBFcUBYixnRGLBiFVmRAr9KGW0SmZs28uU";

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

        async void loginbutton_Clicked(System.Object sender, System.EventArgs e)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email.Text, password.Text);
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
                Application.Current.MainPage = new BurgerMenu();

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Invalid useremail or password", "OK");
            }
        }
    }
}