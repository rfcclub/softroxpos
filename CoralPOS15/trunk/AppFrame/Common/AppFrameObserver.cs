using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFrame.Common
{
    class AppFrameObserver<T>
    {
            public delegate void ValueChangeDelegate(object sender, T value);
            public event ValueChangeDelegate ValueChangeNotification;

            private T currentValue;

            public AppFrameObserver()
            {
                currentValue = default(T);
            }

            public AppFrameObserver(T initValue)
            {
                currentValue = initValue;
            }


            public void Update(object source, T value)
            {
                if (currentValue != null)
                {
                    if (!currentValue.Equals(value) && ValueChangeNotification != null)
                        ValueChangeNotification(source, value);
                }
                else
                    if (value != null && ValueChangeNotification != null)
                        ValueChangeNotification(source, value);
                currentValue = value;
            }
    }
}
