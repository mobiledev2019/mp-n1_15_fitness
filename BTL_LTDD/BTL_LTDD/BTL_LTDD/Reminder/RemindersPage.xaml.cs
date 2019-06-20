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
	public partial class RemindersPage : ContentPage
	{
        DateTime _secondDilivery;
        public RemindersPage ()
		{
			InitializeComponent ();
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);
        }
        bool OnTimerTick()
        {
            if (_switch.IsToggled && DateTime.Now >= _secondDilivery)
            {
                _switch.IsToggled = false;
                DependencyService.Get<INotification>().ShowNotification("Reminder","Time for Fitness");

            }
            return true;
        }




        private void OnTimePickerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Time")
            {
                SetTriggerTime();
            }

        }

        private void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            SetTriggerTime();
        }

        void SetTriggerTime()
        {
            if (_switch.IsToggled)
            {
                _secondDilivery = DateTime.Today + _timePicker.Time;
                if (_secondDilivery < DateTime.Now)
                {
                    _secondDilivery += TimeSpan.FromDays(1);
                }
            }
        }
    }
}