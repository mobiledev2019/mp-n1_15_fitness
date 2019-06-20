using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BTL_LTDD.Droid;
using Xamarin.Forms;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;
[assembly:Dependency(typeof(Notification_Android))]

namespace BTL_LTDD.Droid
{
    class Notification_Android :INotification
    {
        private Notification.Builder builder = null;
        private readonly int NOTIFICATION_ID = 0;




        public void ShowNotification(string title, string content, DateTime dateTime)
        {
            var intent = CreateIntent(NOTIFICATION_ID);

            var localNotification = new LocalNotification();
            localNotification.Title = title;
            localNotification.Body = content;
            localNotification.Id = NOTIFICATION_ID;
            localNotification.NotifyTime = dateTime;

            localNotification.IconId = Resource.Drawable.ic_audiotrack_light;


            var serializedNotification = SerializeNotification(localNotification);
            intent.PutExtra(ScheduledAlarmHandler.LocalNotificationKey, serializedNotification);

            var pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, intent, PendingIntentFlags.CancelCurrent);
            var triggerTime = NotifyTimeInMilliseconds(localNotification.NotifyTime);
            var alarmManager = GetAlarmManager();

            alarmManager.Set(AlarmType.RtcWakeup, triggerTime, pendingIntent);
            // alarmManager.SetRepeating(AlarmType.RtcWakeup,triggerTime,)
        }



        void CreatNotificationChannel()
        {
            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.O)
            {
                return;
            }
            var channel = new NotificationChannel("channel_ID", "channel", NotificationImportance.Default)
            {
                Description = "description channel"
            };
            var notificationManager = Android.App.Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;
            notificationManager.CreateNotificationChannel(channel);
        }

        private Intent getLaucherActivity()
        {
            var packet = Android.App.Application.Context.PackageName;
            return Android.App.Application.Context.PackageManager.GetLaunchIntentForPackage(packet);

        }

        private long NotifyTimeInMilliseconds(DateTime notifyTime)
        {
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
            var epochDifference = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;

            var utcAlarmTimeInMillis = utcTime.AddSeconds(-epochDifference).Ticks / 10000;
            return utcAlarmTimeInMillis;
        }

        private AlarmManager GetAlarmManager()
        {
            var alarmManager = Android.App.Application.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            return alarmManager;
        }


        public void ShowNotification(string title, string content)
        {

            CreatNotificationChannel();
            Context context = Android.App.Application.Context;
            var intent = getLaucherActivity();
            var stackBuiler = TaskStackBuilder.Create(context);
            stackBuiler.AddNextIntent(intent);
            var pendingIntent = stackBuiler.GetPendingIntent(0, (int) PendingIntentFlags.UpdateCurrent);
            if (builder == null)
                builder = new Notification.Builder(context, "channel_ID");

            builder.SetAutoCancel(true);
            builder.SetContentTitle(title);
            builder.SetContentIntent(pendingIntent);
            builder.SetContentText(content);
            //

            builder.SetSmallIcon(Resource.Drawable.ic_vol_type_tv_dark);
            // Resource.Drawable.ic_stat_button_click;

            Notification notification = builder.Build();

            NotificationManager notificationManager =
                context.GetSystemService(Context.NotificationService) as NotificationManager;

            notificationManager.Notify(NOTIFICATION_ID, notification);
        }

        private Intent CreateIntent(int id)
        {
            return new Intent(Android.App.Application.Context, typeof(ScheduledAlarmHandler))
                .SetAction("LocalNotifierIntent" + id);
        }
        private string SerializeNotification(LocalNotification notification)
        {
            var xmlSerializer = new XmlSerializer(notification.GetType());
            using (StringWriter stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, notification);
                return stringWriter.ToString();
            }
        }
    }
}