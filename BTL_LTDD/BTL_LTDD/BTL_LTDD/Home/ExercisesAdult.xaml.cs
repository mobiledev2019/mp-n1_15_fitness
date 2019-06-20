using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BTL_LTDD.ViewModel;
namespace BTL_LTDD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExercisesAdult : ContentPage
    {
        
        Exercise selectedExer;
        Exercise formatDay;
        public ExercisesAdult()
        {
            InitializeComponent();
            

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            formatDay = (Exercise)BindingContext;
            listView.ItemsSource = ExerciseFactory.AllExerciseAdult;
            
        }

        async private void AddExerciseToDay(object sender, EventArgs e)
        {
            if (selectedExer == null)
            {
                await DisplayAlert("Note", "You must select the exercise before click", "OK");
            }
            else
            {
                Exercise addExer = new Exercise();
                addExer.NameExercise = selectedExer.NameExercise;
                addExer.Time = selectedExer.Time;
                addExer.Type = formatDay.Type;
                addExer.ImageExercise = selectedExer.ImageExercise;
                int checkedExerExist = await App.ExerciseDatabase.EqualExercise(addExer);
                if (checkedExerExist == 0)
                {
                    await App.ExerciseDatabase.SaveExerciseAsync(addExer);
                    DependencyService.Get<IMessage>().Shorttime("exercise is added");
                    listView.SelectedItem = null;
                }
                else
                {
                    await DisplayAlert("Note", "exercise is existed", "OK");

                }

            }
        }

        private void SelectedExercise(object sender, SelectedItemChangedEventArgs e)
        {
            selectedExer = (Exercise)e.SelectedItem;
           
        }

        async private void Completed(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}