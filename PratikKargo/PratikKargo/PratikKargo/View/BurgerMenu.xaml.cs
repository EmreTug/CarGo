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
    [Obsolete]
    public partial class BurgerMenu : MasterDetailPage
    {
        public BurgerMenu()
        {
            InitializeComponent();
            this.Master = new Master();
            this.Detail = new NavigationPage(new MainPage());
            App.MasterDet = this;
        }
    }
}