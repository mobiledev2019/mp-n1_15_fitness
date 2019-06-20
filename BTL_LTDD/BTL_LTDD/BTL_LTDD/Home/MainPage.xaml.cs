using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;

namespace BTL_LTDD
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            
            InitializeComponent();
        }
        void onClicked(object sender, EventArgs e)
        {
            var message = "clicked";
            DependencyService.Get<IMessage>().Longtime(message);
        }

        async private void onAdultFitnessNav(object sender, EventArgs e)
        {
            ClickedSound();
            await Navigation.PushAsync(new AdultFitness());

        }
        async private void onChildrenFitnessNav(object sender, EventArgs e)
        {
            ClickedSound();
            await Navigation.PushAsync(new ChildrenFitness());
        }
        void ClickedSound()
        {
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load("clicked_btn1.mp3");
            player.Play();
        }
        

    }
}
