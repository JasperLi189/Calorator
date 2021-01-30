using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace calorator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Result : ContentPage
    {
        //initialization
        public Dictionary<string, int> Library;
        private List<string> Itemlist;
        private List<double> ItemWeight;
        private List<string> QuickList;
        public int currentCaloris;
        public double TotalCaloris;
        ObservableCollection<string> ViewList;


        public Result()
        {
            InitializeComponent();
            //Set up the List 
            Itemlist = new List<string>();
            ItemWeight = new List<double>();
            QuickList = new List<string>();
            ViewList = new ObservableCollection<string>();
            //Set to each 100g in caloris
            LoadLibrary();
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            //Clear up the result list before load in new data
            ViewList.Clear();
            //Get the food from Application 
            GetFood();
            //Quick Access get data From application to load in to a list
            LoadListQuick();
            //Detail view get data From application to load in to a list 
            LoadListDetail();
            //Update UI components 
            UpdateUI();
            
        }

        public void GetFood()
        {
            //For getting up the qucik access food
            if (Application.Current.Properties.ContainsKey("PickedFood"))
            {
                object GetPickedQuick = Application.Current.Properties["PickedFood"];
                QuickList.Add((string)GetPickedQuick);
                if (Library.ContainsKey((string)GetPickedQuick ))
                {
                    Library.TryGetValue((string)GetPickedQuick,out currentCaloris);
                    TotalCaloris += currentCaloris;
                    Application.Current.Properties.Remove("PickedFood");
                }
            }

            //For getting up the detial view food
            if (Application.Current.Properties.ContainsKey("Picked"))
            {
                object GetPicked = Application.Current.Properties["Picked"];
                Itemlist.Add((string)GetPicked);
                object GetWeight = Application.Current.Properties["Weight"];
                ItemWeight.Add((double)GetWeight);
                if (Library.ContainsKey((string)GetPicked))
                {
                    Library.TryGetValue((string)GetPicked, out currentCaloris);
                    TotalCaloris += currentCaloris * (double)GetWeight;
                    Application.Current.Properties.Remove("Picked");
                }
            }

            //For getting up the others food 
            if (Application.Current.Properties.ContainsKey("Other"))
            {
                object GetOther = Application.Current.Properties["Other"];
                string Others = GetOther.ToString();
                try
                {
                    TotalCaloris += Convert.ToDouble(Others);
                    Itemlist.Add("Others");
                    ItemWeight.Add(Convert.ToDouble(Others));
                    Application.Current.Properties.Remove("Other");
                }
                catch (Exception)
                {
                    DisplayAlert("Error", "Please entry numeric number!", "Ok");
                }

            }
        }

        public void LoadListQuick()
        {
            //Add every thing into ViewList prepare for display
            for (int i = 0; i < QuickList.Count; i++)
            {
                string item = QuickList[i];
                int caloris;
                if (Library.TryGetValue(item,out caloris))
                {
                    Library.TryGetValue(item, out caloris);
                    ViewList.Add(item + " ----- " + caloris.ToString());
                }
            }
        }

        public void LoadListDetail()
        {
            //Add every thing into ViewList prepare for display
            for (int i = 0; i < Itemlist.Count; i++)
            {
                string item = Itemlist[i];
                double weight = ItemWeight[i];
                int caloris;
                if (Library.TryGetValue(item, out caloris))
                {
                    Library.TryGetValue(item, out caloris);
                    ViewList.Add(item + " ----- " + weight.ToString() + " X " + caloris.ToString());
                }
                else
                {
                    ViewList.Add("Others" + " ---- " + weight.ToString());
                }

            }
        }

        public void UpdateUI()
        {
            //Updated the list
            ItemDisplay.ItemsSource = ViewList;

            //Show up the Goals
            object BMR = Application.Current.Properties["Goal"];
            
            Goal.Text =  BMR.ToString();
            Test.Text =  TotalCaloris.ToString();

            //Prograss bar update
            double inPrograss = TotalCaloris / (double)BMR;
            Prograss.Progress = inPrograss;
            if(Prograss.Progress >= 1)
            {
                Prograss.ProgressColor = Color.Red;
                DisplayAlert("Warning", "You exceed the daily calories intakes!", "Ok");
            }
            else if (Prograss.Progress > 0.8)
            {
                Prograss.ProgressColor = Color.Yellow;
            }
            else
            {
                Prograss.ProgressColor = Color.Green;
            }
        }

        public void LoadLibrary()
        {
            //The calories index for all 
            Library = new Dictionary<string, int>()
            {
                { "Apple",53 },{ "Banana", 90},{"Orange", 45 },{"Pear", 102 },{"Broccoli", 32 },{"Corn", 96 },{"Carrot", 41 },{"Chicken", 220 },
                {"Lettuce", 16 },{"Milk", 51 },{"Coca-Cola", 38 },{"Coffee", 1 },{"Pork", 238 },{"Lamb", 294 },{"Crab", 83 },{"Beef", 291 },
                {"Rice", 130 },{"Wheat", 329 },{"Oats", 379 },{"Barley", 123 },{"Bak Kut Teh", 260 },{"Chips", 150},{"Burger", 225},{"Chickenrice", 600},
                {"Friedrice",163 },{"Friedchicken", 492 },{"Fries", 312},{"Noodle", 511},{"Pasta", 131},{"Salad", 417},{"soup", 71},{"Sushi", 250},{"Tea", 1},
                {"Beer", 154},{"Bubbletea", 320},{"Curry", 1000},{"HotDog", 160},{"HotPot", 500},{"Icecream", 520},{"Sandwich", 300},{"Taco", 160},{"Ramen",436 }
            };
            
        }

        private void ClearAll_Clicked(object sender, EventArgs e)
        {
            //Clear every thing
            Application.Current.Properties.Remove("PickedFood");
            Application.Current.Properties.Remove("Picked");
            Application.Current.Properties.Remove("Weight");
            TotalCaloris = 0;
            currentCaloris = 0;
            ViewList.Clear();
            Itemlist.Clear();
            ItemWeight.Clear();
            QuickList.Clear();
            UpdateUI();
        }

        private void ResetInfo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new WelcomePage());
        }
    }
}