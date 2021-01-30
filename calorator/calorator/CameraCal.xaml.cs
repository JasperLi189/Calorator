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
    public partial class CameraCal : ContentPage
    {
        public CameraCal()
        {
            InitializeComponent();
        }

        private void Calculate_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["Other"] = Calories.Text;
            DisplayAlert("Added", "Please go to <Result Page> for more details!", "OK");
            Navigation.PopModalAsync(true);
        }
    }
}