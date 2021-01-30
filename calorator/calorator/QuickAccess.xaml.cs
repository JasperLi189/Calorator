using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace calorator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuickAccess : ContentPage
    {
        public QuickAccess()
        {
            InitializeComponent();
            Navigation.PushModalAsync(new WelcomePage());
        }

        private void ToCalories_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new Calories());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            Application.Current.Properties["PickedFood"] = button.Text;
            DisplayAlert("Added", "Please go to <Result Page> for more details!", "OK");
        }
    }
}