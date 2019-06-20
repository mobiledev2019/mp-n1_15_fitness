using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTL_LTDD.ViewModel;
using BTL_LTDD.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace BTL_LTDD.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WaitingExercise : ContentPage
    {
        private int i;
        private int value;
        private ListExercise listExercise;
        public WaitingExercise()
        {
            InitializeComponent();
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();
            i = 20;
            value = -1;
            progressTime.Progress = 0;

            listExercise = (ListExercise)BindingContext;
            if (listExercise.IndexExercise < (listExercise.Exercises.Count - 1))
            {
                var index = listExercise.IndexExercise;
                NameExercise.Text = listExercise.Exercises[index + 1].NameExercise;
                TimeExercise.Text = listExercise.Exercises[index + 1].Time;

                Plugin.TextToSpeech.CrossTextToSpeech.Current.Speak($"the next : {TimeExercise.Text} {NameExercise.Text} ");
            }
            
           
            await progressTime.ProgressTo(1, 20000, Easing.Linear);
            
        }

        protected override  bool OnBackButtonPressed()
        {
            DisPlay();
            return true; 
        }
        
       async private void DisPlay()
        {
           await DisplayAlert("", "You should complete all exercises", "OK");
            
        }
       

        async private void NextExercise()
        {
            await Task.WhenAll(
                frameView.ScaleTo(0,350,Easing.SpringIn)
                );
            var indexExcercise = listExercise.IndexExercise;
            if (indexExcercise <= (listExercise.Exercises.Count - 2))
            {
                listExercise.IndexExercise++;
                ExerciseStart exerciseStart = new ExerciseStart();
                exerciseStart.BindingContext = listExercise;
                await Navigation.PushModalAsync(exerciseStart);
            }
            else
            {
                string dayType = listExercise.DayType;
                Debug.WriteLine("quang : " + dayType);

                string typeExercise = "";
                string dayExercise = "";
                for (int i = 1; i <= 15; i++)
                {
                    if (dayType.Contains(i.ToString()))
                    {
                        dayExercise = $"Day {i}";
                    }

                }
                if (dayType.Contains("adult"))
                    typeExercise = "adult";
                else if (dayType.Contains("children"))
                    typeExercise = "children";
                Day dayArgument = new Day
                {
                    Type = typeExercise,
                    Name = dayExercise
                };
                IList<Day> daysResult = await App.DayDatabase.GetDayAsync(dayArgument);
                Day day = daysResult[0];
                day.Image = "checked.png";
                await App.DayDatabase.SaveDayAsync(day);
                Application.Current.MainPage = new RootPage();
                Sound();
                await DisplayAlert("", "Congratulation !", "OK");
                
            }


        }

        private void Sound()
        {
            var sound = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            sound.Load("congratulation.mp3");
            sound.Play();
        }

        async private void ChangedProperty(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ProgressBar progressBar = sender as ProgressBar;
                    var textToSpeech = Plugin.TextToSpeech.CrossTextToSpeech.Current;
                    int val = (int)(progressBar.Progress * 100);
                    if (val % 5 == 0)
                    {

                        //time.Text = i + "s";
                        if (value != val)
                        {
                            i--;
                            value = val;
                            if (i == 10)
                            {
                                 textToSpeech.Speak("half part time");
                            }
                            if (i == 3)
                            {
                                 textToSpeech.Speak("3");
                            }
                            if (i == 2)
                            {
                                 textToSpeech.Speak("2");
                            }
                            if (i == 1)
                            {
                                 textToSpeech.Speak("1");
                            }
                        }
                    }
                    


                    
                    if (progressBar.Progress == 1)
                    {
                        NextExercise();
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


    }
}