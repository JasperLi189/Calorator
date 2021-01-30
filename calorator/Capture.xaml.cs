using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace calorator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Capture : ContentPage
    {
        public Capture()
        {
            InitializeComponent();
        }

        async private void TakePic_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });
            if(result != null)
            {
                var stream = await result.OpenReadAsync();

                ResultImage.Source = ImageSource.FromStream(() => stream);
                stream.Dispose();
            }
            

        }

        async private void UseCamera_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                ResultImage.Source = ImageSource.FromStream(() => stream);
                stream.Dispose();
            }
            

        }

        private void Navigate_Clicked(object sender, EventArgs e)
        {
            
            Navigation.PushModalAsync(new CameraCal());
        }
    }
}