using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BTL_LTDD.Data;
using System.IO;
using BTL_LTDD.ViewModel;
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BTL_LTDD
{
    public partial class App : Application
    {
        
        static ExerciseDatabase exerciseDatabase;
        static DayDatabase dayDatabase;

        public static DayDatabase DayDatabase
        {
            get
            {
                if(dayDatabase == null)
                {
                    dayDatabase = new DayDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"day.db3"));
                }
                return dayDatabase;
            }
        }

        public static ExerciseDatabase ExerciseDatabase
        {
            get
            {
                if (exerciseDatabase == null)
                {
                    exerciseDatabase = new ExerciseDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "exercise.db3"));
                    
                }
                return exerciseDatabase;
            }
        }
        public  App()
        {

            MainPage = new SplashPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
