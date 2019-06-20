using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BTL_LTDD.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace BTL_LTDD
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdultFitness : ContentPage
	{
        private IList<Day> listDay;
		public AdultFitness ()
		{
			InitializeComponent ();
           
        }

       async protected override void OnAppearing()
        {
            base.OnAppearing();
            listDay = await App.DayDatabase.GetAdultDaysAsync();
            if(listDay.Count == 0)
            {
                for(int i = 1; i <= 15; i++)
                {
                    await App.DayDatabase.SaveDayAsync(new Day
                    {
                        Name = $"Day {i}",
                        Image = "unchecked.png",
                        Type = "adult"
                    });
                }
            }
            listDay = await App.DayDatabase.GetAdultDaysAsync();
            lstView.ItemsSource = listDay;
        }

        async private void DaySelected(object sender, SelectedItemChangedEventArgs e)
        {
            
                Day selecedDay = (Day)e.SelectedItem;
            var index = selecedDay.Name.Split(' ')[1].Trim();
           

           var checkIndex = int.Parse(index) - 1;
                    var checkDay = new Day {
                        Name = $"Day {checkIndex}",
                        Type = "adult"
                    };
            if (selecedDay.Name.Contains("1"))
            {
                await Navigation.PushAsync(new ExerciseDetail
                {
                    BindingContext = new Exercise
                    {
                        Type = $"adultday{index}"
                    }
                });
                return;
            }
                    var resultDay = await App.DayDatabase.GetDayAsync(checkDay);

                   if(resultDay != null)
                {
                    var ok = (resultDay[0].Image.Equals("checked.png")) ? true : false;
                    
                    if ((selecedDay.Name.Equals($"Day {index}") && ok) )
                    {

                        await Navigation.PushAsync(new ExerciseDetail
                        {
                            BindingContext = new Exercise
                            {
                                Type = $"adultday{index}"
                            }
                        });

                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Note", "You must complete it sequentially", "OK");
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().Shorttime($"error");
                }
                

               
            
        }
    }
}