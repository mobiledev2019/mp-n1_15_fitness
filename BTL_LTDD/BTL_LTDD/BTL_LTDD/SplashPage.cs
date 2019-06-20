using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace BTL_LTDD
{
    class SplashPage : ContentPage
    {
        Image splashImage;

        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "splash.jpg",
                WidthRequest = 700,
                HeightRequest = 1400
            };
            AbsoluteLayout.SetLayoutFlags(splashImage,
               AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
             new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.Black;
            this.Content = sub;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await splashImage.ScaleTo(0.4, 1000,Easing.Linear);
            await splashImage.ScaleTo(0.7, 1000,Easing.Linear); //Time-consuming processes such as initialization
            await splashImage.ScaleTo(0.5, 1000, Easing.Linear);
            await splashImage.ScaleTo(10, 1000, Easing.Linear);
            Application.Current.MainPage = new RootPage();    //After loading  MainPage it gets Navigated to our new Page
        }
    }
}
