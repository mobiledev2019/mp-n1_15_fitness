using System;
using System.Collections.Generic;
using System.Text;

namespace BTL_LTDD
{
   public interface IMessage
    {
        void Longtime(string message);
        void Shorttime(string message);
    }
}
