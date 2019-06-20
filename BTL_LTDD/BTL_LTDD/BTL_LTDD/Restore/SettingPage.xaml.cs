using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BTL_LTDD
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingPage : ContentPage
	{
        
        public SettingPage ()
		{
			InitializeComponent ();
            
        }
        async private void RestoreAllClicked(object sender, EventArgs e)
        {
            var resultClick = await DisplayAlert("", "Do you want to restore all?", "YES", "NO");
            if (resultClick)
            {
                await App.DayDatabase.DeleteAllDayAsync();
                await App.ExerciseDatabase.DeleteAllExerciseAsync();
                DependencyService.Get<IMessage>().Shorttime("restore all is completed");
                Application.Current.MainPage = new RootPage();
            }
        }
        

    }
}