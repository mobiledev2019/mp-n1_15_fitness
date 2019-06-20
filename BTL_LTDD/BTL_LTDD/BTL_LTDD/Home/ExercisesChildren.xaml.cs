using System;

using System.Collections.ObjectModel;
using BTL_LTDD.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BTL_LTDD
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExercisesChildren : ContentPage
	{
        Exercise selectedExer;
        Exercise formatDay;
        public ExercisesChildren ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            formatDay = (Exercise)BindingContext;
            listView.ItemsSource = ExerciseFactory.AllExerciseChildren;
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