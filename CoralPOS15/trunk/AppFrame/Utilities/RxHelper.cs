using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;

namespace AppFrame.Utilities
{
    public static class RxHelper
    {
        public static PropertyInfo ToPropertyInfo<TTarget, TValue>(this Expression<Func<TTarget, TValue>> expression)
        {
            // Get the body of the expression
            Expression body = expression.Body;
            if (body.NodeType != ExpressionType.MemberAccess)
                throw new ArgumentException("Property expression must be of the form 'x => x.SomeProperty'",
                                            "expression");
            // Cast the expression to the appropriate type
            MemberExpression memberExpression = (MemberExpression)body;
            return memberExpression.Member as PropertyInfo;
        }

        public static IObservable<TValue> ObservableForProperty<TSource, TValue>(this TSource source, Expression<Func<TSource, TValue>> propertyExpression)
            where TSource : INotifyPropertyChanged
        {

            // Get the information for the property
            PropertyInfo property = propertyExpression.ToPropertyInfo();
            if (property == null)
            {
                // Expression does not indicate a property
                throw new ArgumentException("Property expression must point to a valid property", "propertyExpression");
            }

            // Convert the PropertyChanged event to an Observable
            var eventObservable = Observable.FromEventPattern<PropertyChangedEventArgs>(source, "PropertyChanged");

            // Filter the event and return it)))
            return eventObservable.Where(e => e.EventArgs.PropertyName == property.Name).Select(e => (TValue)property.GetValue(source, null));
        }

        public static IDisposable BindTo<TTarget, TValue>(this IObservable<TValue> observable, TTarget target, Expression<Func<TTarget, TValue>> propertyExpression)
        {
            // Get the information for the property
            PropertyInfo property = propertyExpression.ToPropertyInfo();
            if (property == null)
            {
                // Expression does not indicate a property
                throw new ArgumentException("Property expression must point to a valid property", "propertyExpression");
            }

            // Subscribe to the observable
            return observable.Subscribe(value => property.SetValue(target, value, null));
        }
        
    }
}
