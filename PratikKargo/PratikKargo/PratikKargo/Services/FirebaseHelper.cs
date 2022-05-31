using PratikKargo.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;
using Firebase.Database;
using System.Linq;

namespace PratikKargo.Services
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://kargo-b7ed8-default-rtdb.firebaseio.com/");

        public async Task<List<Cargo>> GetAllCargo()
        {

            return (await firebase
              .Child("Cargo")
              .OnceAsync<Cargo>()).Select(item => new Cargo
              {
                  NameSurname = item.Object.NameSurname,
                  KargoId = item.Object.KargoId,
                  Adress=item.Object.Adress,
                  PhoneNumber=item.Object.PhoneNumber,
                  X=item.Object.X,
                  Y=item.Object.Y
              }).ToList();
        }
        public async Task AddCargo(int kargoId, string name,string adress, string number,string x,string y)
        {

            await firebase
              .Child("Cargo")
              .PostAsync(new Cargo() { KargoId = kargoId, NameSurname = name,Adress=adress,PhoneNumber =number,X=x,Y=y});
        }
        public async Task<Cargo> GetCargo(int kargoId)
        {
            var allCargo = await GetAllCargo();
            await firebase
              .Child("Cargo")
              .OnceAsync<Cargo>();
            return allCargo.Where(a => a.KargoId ==kargoId ).FirstOrDefault();
        }
        public async Task UpdateCargo(int kargoId, string name,string number,string adress,string x,string y)
        {
            var toUpdateCargo = (await firebase
              .Child("Cargo")
              .OnceAsync<Cargo>()).Where(a => a.Object.KargoId == kargoId).FirstOrDefault();

            await firebase
              .Child("Cargo")
              .Child(toUpdateCargo.Key)
              .PutAsync(new Cargo() { KargoId = kargoId, NameSurname = name,Adress=adress,PhoneNumber=number,X=x,Y=y });
        }

        public async Task DeleteCarg(int kargoId)
        {
            var toDeleteCargo = (await firebase
              .Child("Cargo")
              .OnceAsync<Cargo>()).Where(a => a.Object.KargoId == kargoId).FirstOrDefault();
            await firebase.Child("Cargo").Child(toDeleteCargo.Key).DeleteAsync();

        }
    }
}
