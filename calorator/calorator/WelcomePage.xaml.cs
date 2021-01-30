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
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Man BMR = (10 x weight in kg) + (6.25 × height in cm) - (5 × age in years) + 5
            //Women BMR = (10 x weight in kg) + (6.25 × height in cm) - (5 × age in years) - 161
            //Reference:https://www.integrativepro.com/Resources/Integrative-Blog/2016/How-to-Determine-Caloric-Intake-Needs

            if (rbtM.IsChecked)
            {
                if (No.IsChecked) MaleBRM(1.2);
                else if (Light.IsChecked) MaleBRM(1.55);
                else MaleBRM(1.9);



            }
            else
            {
                if (No.IsChecked) FemaleBRM(1.2);
                else if (Light.IsChecked) FemaleBRM(1.55);
                else FemaleBRM(1.9);
            }
            
        }

        public void MaleBRM(double Level)
        {
            //For Male BRM
            double BRM = ((10 * (Int32.Parse(W.Text))) + (6.25 * (Int32.Parse(H.Text))) - (5 * (Int32.Parse(A.Text))) + 5)*Level ;
            Application.Current.Properties["Goal"] = BRM;
            Navigation.PopModalAsync(true);
            App.Current.SavePropertiesAsync();
        }

        public void FemaleBRM(double Level)
        {
            //For Female BRM
            double BRM = ((10 * (Int32.Parse(W.Text))) + (6.25 * (Int32.Parse(H.Text))) - (5 * (Int32.Parse(A.Text))) - 161)*Level;
            Application.Current.Properties["Goal"] = BRM;
            Navigation.PopModalAsync(true);
            App.Current.SavePropertiesAsync();
        }
    }
}