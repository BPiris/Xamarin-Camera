using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Xamarin.Forms;

namespace Xamarin_Camera.Vistas
{
    public class Camara:ContentPage
    {
        /// <summary>
        /// Gets if a camera is available on the device
        /// </summary>
        bool IsCameraAvailable { get; }

        /// <summary>
        /// Gets if ability to take photos supported on the device
        /// </summary>
        bool IsTakePhotoSupported { get; }

        /// <summary>
        /// Gets if the ability to pick photo is supported on the device
        /// </summary>
        bool IsPickPhotoSupported { get; }

        /// <summary>
        /// Gets if ability to take video is supported on the device
        /// </summary>
        bool IsTakeVideoSupported { get; }

        /// <summary>
        /// Gets if the ability to pick a video is supported on the device
        /// </summary>
        bool IsPickVideoSupported { get; }

        public Camara()
        {
            var btnCamara = new Button();
            Image image = new Image();

            btnCamara.Clicked += async(sender, args) =>
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");

                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });

                //or:
                //image.Source = ImageSource.FromFile(file.Path);
                //image.Dispose();
            };
            Content = new StackLayout()
            {
                Children = { btnCamara}
            };

        }
    }
}
