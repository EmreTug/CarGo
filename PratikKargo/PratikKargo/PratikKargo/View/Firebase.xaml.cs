using PratikKargo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PratikKargo.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Firebase : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public Firebase()
        {
            InitializeComponent();

        }
        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allCargo = await firebaseHelper.GetAllCargo();
            lstCargo.ItemsSource = allCargo;
        }
    

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.AddCargo(Convert.ToInt32(txtId.Text), txtName.Text,adress.Text,number.Text,X.Text,Y.Text);
            txtId.Text = string.Empty;
            await DisplayAlert("Success", "Cargo Added Successfully", "OK");
            var allCargo = await firebaseHelper.GetAllCargo();
            lstCargo.ItemsSource = allCargo;
        }
 

        private async void BtnRetrive_Clicked(object sender, EventArgs e)
        {
            var cargo = await firebaseHelper.GetCargo(Convert.ToInt32(txtId.Text));
            if (cargo != null)
            {
                txtId.Text = cargo.KargoId.ToString();
                txtName.Text = cargo.Adress;
                await DisplayAlert("Success", "Cargo Retrive Successfully", "OK");

            }
            else
            {
                await DisplayAlert("Success", "No Cargo Available", "OK");
            }

        }
        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.UpdateCargo(Convert.ToInt32(txtId.Text), txtName.Text, adress.Text, number.Text, X.Text, Y.Text);
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            await DisplayAlert("Success", "Cargo Updated Successfully", "OK");
            var allCargo = await firebaseHelper.GetAllCargo();
            lstCargo.ItemsSource = allCargo;
        }
   


        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.DeleteCarg(Convert.ToInt32(txtId.Text));
            await DisplayAlert("Success", "Cargo Deleted Successfully", "OK");
            var allCargo = await firebaseHelper.GetAllCargo();
            lstCargo.ItemsSource = allCargo;
        }
    }
}