using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTL_LTDD.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace BTL_LTDD.Home
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExerciseStart : ContentPage
	{
        ListExercise listExercise;

        public ExerciseStart ()
		{
			InitializeComponent ();
		}

        private void Sound()
        {
            var sound = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            sound.Load("whistle.mp3");
            sound.Play();
            
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listExercise = (ListExercise)BindingContext;
            Exercise exercise = listExercise.Exercises[listExercise.IndexExercise];
            image.Source = exercise.ImageExercise;
            nameExercise.Text = exercise.NameExercise;
            repNumber.Text = exercise.Time;
            Sound();
            Plugin.TextToSpeech.CrossTextToSpeech.Current.Speak($"you need exercise {repNumber.Text}  {nameExercise.Text}  " );

        }

        protected override bool OnBackButtonPressed()
        {
            DisPlay();
            return true;
        }

        async private void DisPlay()
        {
            await DisplayAlert("", "You should complete all exercises", "OK");

        }


        async private void buttonClicked(object sender, EventArgs e)
        {
            
            await Task.WhenAll(
                 frameView.ScaleTo(0,350,Easing.SpringIn)
                
                );
            
            WaitingExercise waitingExercise = new WaitingExercise();
            waitingExercise.BindingContext = listExercise;
            await Navigation.PushModalAsync(waitingExercise);


        }
    }
}