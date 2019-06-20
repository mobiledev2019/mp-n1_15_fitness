using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BTL_LTDD.ViewModel;
using BTL_LTDD.Data;
using System.IO;
using BTL_LTDD.Home;
using System.Diagnostics;

namespace BTL_LTDD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseDetail : ContentPage
    {
        Exercise dayexercise;
        IList<Exercise> listExercise;
        public ExerciseDetail()
        {
            InitializeComponent();


        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            dayexercise = (Exercise)BindingContext;
            
            

            listExercise = await App.ExerciseDatabase.GetExercisesByTypeAsync(dayexercise);
            if (listExercise.Count == 0)
            {
                
                 string[] exercise = { "hand start", "shoulder start", "waist start", "legs start" };
                 for (int i = 0; i < exercise.Length; i++)
                 {
                    string image ="",time ="10 reps";
                    switch (i)
                    {
                        case 0:
                            image = "boxing.PNG";
                            
                            break;
                        case 1:
                            image = "hiking.PNG";
                            break;
                        case 2:
                            image = "dancing.PNG";
                            break;
                        case 3:
                            image = "rowing.PNG";
                            break;
                    }
                    await App.ExerciseDatabase.SaveExerciseAsync(new Exercise
                     {
                         NameExercise = exercise[i],
                         Type = dayexercise.Type,
                         ImageExercise = image,
                         Time = time
                     });
                 }
                 listExercise = await App.ExerciseDatabase.GetExercisesByTypeAsync(dayexercise);
                
            }

            lstView.ItemsSource = listExercise;

        }





       async private void ExcerciseSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Exercise selectedExe = (Exercise)e.SelectedItem;

            if(selectedExe != null)
            {
                string[] buttons = { "YES", "NO" };
                string result = await DisplayActionSheet("do you want to delete exercice ?", null, null, buttons);
                if (!string.IsNullOrEmpty(result) && result.Equals(buttons[0]))
                {


                    await App.ExerciseDatabase.DeleteExerciseAsync(selectedExe);
                    listExercise = await App.ExerciseDatabase.GetExercisesByTypeAsync(dayexercise);
                    lstView.ItemsSource = listExercise;
                    
                }
                lstView.SelectedItem = null;
            }
           
        }

        async private void OnExerciseModifyed(object sender, EventArgs e)
        {
            if (dayexercise.Type.Contains("adult"))
            {
                await Navigation.PushAsync(new ExercisesAdult
                {
                    BindingContext = dayexercise
                });
            }
            else if (dayexercise.Type.Contains("children"))
            {
                await Navigation.PushAsync(new ExercisesChildren
                {
                    BindingContext = dayexercise
                });
            }
        }

        async private void OnExerciseRenewed(object sender, EventArgs e)
        {
            string[] buttons = { "YES", "NO" };
            string result = await DisplayActionSheet("do you want to renew exercices ?", null, null, buttons);
            if (!string.IsNullOrEmpty(result) && result.Equals(buttons[0]))
            {
                RestoreDay();
                DependencyService.Get<IMessage>().Shorttime("renew is sucessed");
            }
        }

       async private void RestoreDay()
        {
            string dayType = dayexercise.Type;
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
            day.Image = "unchecked.png";
            await App.DayDatabase.SaveDayAsync(day);
        }

        async private void OnExerciseStarted(object sender, EventArgs e)
        {

          if(listExercise.Count <= 0)
            {
                await DisplayAlert("Note", "you must to add exercises", "OK");
                if (dayexercise.Type.Contains("adult")){
                    await Navigation.PushAsync(new ExercisesAdult
                    {
                        BindingContext = dayexercise
                    });

                }
                else if (dayexercise.Type.Contains("children"))
                {
                    await Navigation.PushAsync(new ExercisesChildren
                    {
                        BindingContext = dayexercise
                    });
                }
            }
            else
            {
                ListExercise listExer = new ListExercise
                {
                    Exercises = (List<Exercise>)this.listExercise,
                    IndexExercise = 0,
                    DayType = dayexercise.Type

                };
                var exerciseStart = new ExerciseStart();
                exerciseStart.BindingContext = listExer as ListExercise;
                await Navigation.PushModalAsync(exerciseStart, true);
            }



        }
    }
}