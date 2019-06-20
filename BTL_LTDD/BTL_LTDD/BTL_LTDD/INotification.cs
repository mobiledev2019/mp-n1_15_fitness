using System;
using System.Collections.Generic;
using System.Text;

namespace BTL_LTDD
{
   public interface INotification
    {
        void ShowNotification(string title, string body, DateTime date);
        void ShowNotification(string title, string body);
    }
}
