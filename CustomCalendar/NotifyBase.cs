using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace CustomCalendar
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged<T>(Expression<Func<T>> exp)
        {
            //the cast will always succeed
            var memberExpression = (MemberExpression) exp.Body;
            var propertyName = memberExpression.Member.Name;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}