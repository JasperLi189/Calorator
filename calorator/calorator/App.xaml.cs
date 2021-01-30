using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace calorator
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            //Add for RadioButton sincce it sitll in experimental use
            Device.SetFlags(new string[] { "RadioButton_Experimental" });
            //create a tabbed page with tabs
            var tabs = new TabbedPage();
            tabs.Title = "Calorator";
            tabs.Children.Add(new QuickAccess());
            tabs.Children.Add(new Capture());
            tabs.Children.Add(new Result());
            MainPage = new NavigationPage(tabs);
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
            Application.Current.Properties.Clear();
        }

        protected override void OnResume()
        {
        }
       
    }
}
