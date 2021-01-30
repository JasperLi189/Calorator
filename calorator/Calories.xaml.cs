using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace calorator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calories : ContentPage
    {
        
        private object SetWeight;

        public Calories()
        {
            InitializeComponent();
            
        }

        private void F_Clicked(object sender, EventArgs e)
        {
            //Make up the list 
            List<String> temp = new List<string> { "Apple", "Banana", "Orange", "Pear" };
            Lists.ItemsSource = temp;
            DisplayOn();
        }

        private void V_Clicked(object sender, EventArgs e)
        {
            //Make up the list 
            List<String> temp = new List<string> { "Broccoli", "Corn", "Carrot", "Lettuce" };
            Lists.ItemsSource = temp;
            DisplayOn();
        }

        private void G_Clicked(object sender, EventArgs e)
        {
            //Make up the list
            List<String> temp = new List<string> { "Rice", "Wheat", "Oats", "Barley" };
            Lists.ItemsSource = temp;
            DisplayOn();
        }

        private void B_Clicked(object sender, EventArgs e)
        {
            //Make up the list
            List<String> temp = new List<string> { "Beer", "Milk", "Coca-Cola", "Coffee" };
            Lists.ItemsSource = temp;
            DisplayOn();
        }

        private void M_Clicked(object sender, EventArgs e)
        {
            //Make up the list
            List<String> temp = new List<string> { "Pork", "Lamb", "Crab", "Beef","Chicken" };
            Lists.ItemsSource = temp;
            DisplayOn();
        }

        private void Lists_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Make up the list
            var Item = (ListView)sender;
            Picked.Text = Item.SelectedItem.ToString();
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            //Set up the weight for the seleted item
            weight.Text = ":" + MainSlider.Value.ToString() + " x 100 g";
            SetWeight = MainSlider.Value;
        }

        async private void calculate_Clicked(object sender, EventArgs e)
        {
            //Error handling 
            if (Picked.IsVisible == true && Picked.Text != "Item picked" && SetWeight != null)
            {
                //Stored it at Application state 
                Application.Current.Properties["Picked"] = Picked.Text;
                Application.Current.Properties["Weight"] = SetWeight;
                SuccessDisplay();

            }
            else if(Picked.IsVisible == false && MainSlider.IsVisible == false && weight.IsVisible == false)
            {
                //Stored it at Application state 
                if (GetOther.Text != null)
                {
                    Application.Current.Properties["Other"] = GetOther.Text;
                    SuccessDisplay();
                }
                else
                {
                    await DisplayAlert("Error", "Please indicate the weight or calories !", "Ok");
                }

            }
            else
            {
                await DisplayAlert("Error", "Please indicate the weight or calories !", "Ok");
            }
        }
            

       async public void SuccessDisplay()
        {
            await DisplayAlert("Added", "Please go to <Result Page> for more details!", "OK");
            await Navigation.PopAsync();
        }

        private void O_Clicked(object sender, EventArgs e)
        {
            Picked.IsVisible = false;
            MainSlider.IsVisible = false;
            weight.IsVisible = false;
            Lists.IsVisible = false;
            GetOther.IsVisible = true;

        }
        
        public void DisplayOn()
        {
            Picked.IsVisible = true;
            MainSlider.IsVisible = true;
            weight.IsVisible = true;
            Lists.IsVisible = true;
            GetOther.IsVisible = false;
        }
    }
}