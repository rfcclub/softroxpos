using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using AppFrame.Common;
using AppFrame.Common.Attributes;
using System.ComponentModel;

namespace AppFrame.Binding
{
    public class BindingHelper
    {
        public const string CHECKBOX = "CheckBox";
        public const string COMBOBOX = "ComboBox";
        public const string DATETIMEPICKER = "DateTimePicker";
        public const string LABEL = "Label";
        public const string LISTBOX = "ListBox";
        public const string LISTVIEW = "ListView";
        public const string MASKEDTEXTBOX = "MaskedTextBox";
        public const string NUMERICUPDOWN = "NumericUpDown";
        public const string TEXTBOX = "TextBox";
        public const string RADIOBUTTON = "RadioButton";
        public const string LINKLABEL = "LinkLabel";
        public const string DATAGRIDVIEW = "DataGridView";
        public const string PROPERTY_DATASOURCE = "DataSource";
        public const string EVENT_CLICK = "Click";
        public const string PROPERTY_CHANGED = "PropertyChanged";

        public static IDictionary<string,string> ControlEventMap = new Dictionary<string, string>();

        /// <summary>
        /// static constructor
        /// </summary>
        static BindingHelper()
        {
            // WinForm control
            ControlEventMap[CHECKBOX] = "CheckedChanged";
            ControlEventMap[COMBOBOX] = "SelectedValueChanged";
            ControlEventMap[DATETIMEPICKER] = "ValueChanged";
            ControlEventMap[LABEL] = "TextChanged";
            ControlEventMap[LINKLABEL] = "TextChanged";
            ControlEventMap[LISTBOX] = "SelectedValueChanged";
            ControlEventMap[LISTVIEW] = "SelectedValueChanged";
            ControlEventMap[MASKEDTEXTBOX] = "TextChanged";
            ControlEventMap[NUMERICUPDOWN] = "ValueChanged";
            ControlEventMap[TEXTBOX] = "TextChanged";
            ControlEventMap[RADIOBUTTON] = "CheckedChanged";
        
            // DevExpress control
        }

        /// <summary>
        /// Auto bind for method. Normally if control is button it will be auto binded to a method of ViewModel
        /// </summary>
        /// <param name="control"></param>
        /// <param name="model"></param>
        public static void AutoBindMethod(Control control,object model)
        {
            Type modelType = model.GetType();
            MethodInfo methodInfo = modelType.GetMethod(control.Name);
            if (methodInfo != null)
            {
                var observable = Observable.FromEventPattern<EventArgs>(control, EVENT_CLICK);
                observable.Subscribe(m => methodInfo.Invoke(model, null));
            }
        }

        /// <summary>
        /// Auto bind using data binding
        /// </summary>
        /// <param name="control"></param>
        /// <param name="model"></param>
        public static void AutoBindDataProperty(Control control,object model)
        {
            Type modelType = model.GetType();
            Type controlType = control.GetType();
            PropertyInfo propertyInfo = modelType.GetProperty(control.Name);
            // auto-binding for simple type
            if (propertyInfo != null)
            {
                string name = control.GetType().Name;
                PropertyInfo controlPropInfo = modelType.GetProperty(PROPERTY_DATASOURCE);
                bool canBindDataSource = true;
                if(controlPropInfo == null)
                {
                    canBindDataSource = false;
                }
                switch (name)
                {
                        // simple type
                    case CHECKBOX:
                        BindSimpleDataProperty((CheckBox)control, tb => tb.Checked, model, propertyInfo.Name, canBindDataSource);
                        break;
                    case DATETIMEPICKER:
                        var dateTimePicker = (DateTimePicker) control;
                        BindSimpleDataProperty(dateTimePicker, tb => tb.Value, model, propertyInfo.Name, canBindDataSource);
                        break;
                    case LABEL:
                        var label = (Label) control;
                        BindSimpleDataProperty(label, tb => tb.Text, model, propertyInfo.Name, canBindDataSource);
                        break;
                    case LINKLABEL:
                        var linkLabel = (LinkLabel) control;
                        BindSimpleDataProperty(linkLabel, tb => tb.Text, model, propertyInfo.Name,canBindDataSource);
                        break;
                    
                    case LISTVIEW:
                        var listView = (ListView) control;
                        BindSimpleDataProperty(listView, tb => tb.SelectedItems, model, propertyInfo.Name,canBindDataSource);
                        break;
                    case MASKEDTEXTBOX:
                        var maskedTextBox = (MaskedTextBox) control;
                        BindSimpleDataProperty(maskedTextBox, tb => tb.Text,  model, propertyInfo.Name, canBindDataSource);
                        break;
                    case NUMERICUPDOWN:
                        var numericUpDown = (NumericUpDown) control;
                        BindSimpleDataProperty(numericUpDown, tb => tb.Value, model, propertyInfo.Name,canBindDataSource);
                        break;
                    case TEXTBOX:
                        var textBox = (TextBox) control;
                        //BindTextBoxProperty(textBox,model);
                        BindSimpleDataProperty(textBox, tb => tb.Text,  model, propertyInfo.Name, canBindDataSource);
                        break;
                    case RADIOBUTTON:
                        var radioButton = (RadioButton) control;
                        BindSimpleDataProperty(radioButton, tb => tb.Checked, model, propertyInfo.Name, canBindDataSource);
                        break;
                        // TODO : for other types
                        // complex types
                    case COMBOBOX:
                        var comboBox = (ComboBox)control;
                        BindComplexDataProperty(comboBox, model, propertyInfo,true);
                        break;
                    case LISTBOX:
                        var listBox = (ListBox)control;
                        BindComplexDataProperty(listBox,model,propertyInfo,true);
                        break;
                    case DATAGRIDVIEW:
                        var dataGridView = (DataGridView)control;
                        BindDataGridDataProperty(dataGridView, model, propertyInfo);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="model"></param>
        /// <param name="propertyInfo"></param>
        public static void BindDataGridDataProperty(DataGridView dataGridView, object model, PropertyInfo propertyInfo)
        {            
            BindingSource binding = new BindingSource();            
            binding = new BindingSource(model, propertyInfo.Name);                        
            BindingList<string> list = new BindingList<string>();
            binding.RaiseListChangedEvents = true;
            dataGridView.DataSource = binding;
            UpdateBindingCache(model, propertyInfo.Name, binding);
        }

        /// <summary>
        /// Bind complex data binding
        /// </summary>
        /// <param name="control"></param>
        /// <param name="model"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="bindDisplayValue"></param>
        public static void BindComplexDataProperty(dynamic control, object model, PropertyInfo propertyInfo,bool bindDisplayValue = false)
        {   
            BindingSource binding = new BindingSource(model, propertyInfo.Name);            
            control.DataSource = binding;            
            object[] attlist = propertyInfo.GetCustomAttributes(typeof(DisplayValueAttribute), false);
            if (bindDisplayValue && attlist.Length > 0)
            {
                DisplayValueAttribute attribute = (DisplayValueAttribute)attlist[0];
                control.DisplayMember = attribute.DisplayMember;
                control.ValueMember = attribute.ValueMember;                
            }
            UpdateBindingCache(model, propertyInfo.Name, binding);
        }

        /// <summary>
        /// Bind simple data binding
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TSourceValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="sourceExpression"></param>
        /// <param name="target"></param>
        /// <param name="datamember"></param>
        /// <param name="dataSourceUsing"></param>
        public static void BindSimpleDataProperty<TSource, TSourceValue>
                                   (TSource source,
                                    Expression<Func<TSource, TSourceValue>> sourceExpression,
                                    object target,
                                    string datamember,
                                    bool dataSourceUsing = false
                                    ) where TSource : Control
        {
            System.Windows.Forms.Binding binding = new System.Windows.Forms.Binding(sourceExpression.ToPropertyInfo().Name, target, datamember);
            binding.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged;
            source.DataBindings.Add(binding);
        }

        private static void UpdateBindingCache(object model, string property, System.Windows.Forms.BindingSource binding)
        {
            if (model is BasePresenter)
            {
                ModelBindingCache cache = ((BasePresenter)model).BindingCache;
                cache.Put(property, binding);
            }
        }

        /// <summary>
        /// Bind a specific control to ViewModel using Reactive Extension
        /// Default is binding one-way only
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="model">ViewModel</param>
        public static void AutoBindProperty(Control control, object model)
        {
            Type modelType = model.GetType();
            PropertyInfo propertyInfo = modelType.GetProperty(control.Name);
            // auto-binding for simple type
            if (propertyInfo != null)
            {
                string name = control.GetType().Name;
                string magicString = ControlEventMap[name];
                
                switch (name)
                {
                    case CHECKBOX:
                        var checkBox = (CheckBox)control;
                        BindProperty(checkBox, tb => tb.Checked, magicString, model, propertyInfo);
                        break;
                    case COMBOBOX:
                        var comboBox = (ComboBox)control;
                        BindProperty(comboBox, tb => tb.SelectedValue, magicString, model, propertyInfo);
                        break;
                    case DATETIMEPICKER:
                        var dateTimePicker = (DateTimePicker)control;
                        BindProperty(dateTimePicker, tb => tb.Value, magicString, model, propertyInfo);
                        break;
                    case LABEL:
                        var label = (Label)control;
                        BindProperty(label, tb => tb.Text, magicString, model, propertyInfo);
                        break;
                    case LINKLABEL:
                        var linkLabel = (LinkLabel)control;
                        BindProperty(linkLabel, tb => tb.Text, magicString, model, propertyInfo);
                        break;
                    case LISTBOX:
                        var listBox = (ListBox)control;
                        BindProperty(listBox, tb => tb.SelectedValue, magicString, model, propertyInfo);
                        break;
                    case LISTVIEW:
                        var listView = (ListView)control;
                        BindProperty(listView, tb => tb.SelectedItems, magicString, model, propertyInfo);
                        break;
                    case MASKEDTEXTBOX:
                        var maskedTextBox = (MaskedTextBox)control;
                        BindProperty(maskedTextBox, tb => tb.Text, magicString, model, propertyInfo);
                        break;
                    case NUMERICUPDOWN:
                        var numericUpDown = (NumericUpDown)control;
                        BindProperty(numericUpDown, tb => tb.Value, magicString, model, propertyInfo);
                        break;
                    case TEXTBOX:
                        var textBox = (TextBox) control;
                        //BindTextBoxProperty(textBox,model);
                        BindProperty(textBox, tb => tb.Text, magicString, model, propertyInfo);
                        break;
                    case RADIOBUTTON:
                        var radioButton = (RadioButton)control;
                        BindProperty(radioButton, tb => tb.Checked, magicString, model, propertyInfo);
                        break;
                    // TODO : for other types
                    default:
                        break;
                }

                // auto-binding for complex type
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TSourceValue"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="sourceExpression"></param>
        /// <param name="eventChangedString"></param>
        /// <param name="target"></param>
        /// <param name="targetPropertyInfo"></param>
        /// <param name="twoWay"></param>
        public static void BindProperty<TSource,TSourceValue,TTarget>
                                   (TSource source,
                                    Expression<Func<TSource,TSourceValue>> sourceExpression,
                                    string eventChangedString,
                                    TTarget target,
                                    PropertyInfo targetPropertyInfo,
                                    bool twoWay =false
                                    )
        {            
            PropertyInfo sourcePropertyInfo = sourceExpression.ToPropertyInfo();
            BindProperty(source, sourcePropertyInfo, eventChangedString, target, targetPropertyInfo, twoWay);

            //var observable = Observable.FromEventPattern<EventArgs>(source, eventChangedString);
            //observable.Subscribe(p => targetPropertyInfo.SetValue(
            //                                   target, sourcePropertyInfo.GetValue(source,null), null));

            //if (twoWay)
            //{
            //    var reverseObservable = Observable.FromEventPattern<PropertyChangedEventArgs>(target, "PropertyChanged");
            //    reverseObservable.Subscribe(rp => 
            //    {                      
            //            if(rp.EventArgs.PropertyName.Equals(targetPropertyInfo.Name))
            //            {
            //                sourcePropertyInfo.SetValue(source, targetPropertyInfo.GetValue(target, null), null);                    
            //            }
            //    });
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TSourceValue"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <typeparam name="TTargetValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="sourceExpression"></param>
        /// <param name="eventChangedString"></param>
        /// <param name="target"></param>
        /// <param name="targetExpression"></param>
        /// <param name="twoWay"></param>
        public static void BindProperty<TSource, TSourceValue, TTarget,TTargetValue>
                                   (TSource source,
                                    Expression<Func<TSource, TSourceValue>> sourceExpression,
                                    string eventChangedString,
                                    TTarget target,
                                    Expression<Func<TSource, TTargetValue>> targetExpression,
                                    bool twoWay = false
                                    )
        {
            PropertyInfo sourcePropertyInfo = sourceExpression.ToPropertyInfo();
            PropertyInfo targetPropertyInfo = targetExpression.ToPropertyInfo();
            BindProperty(source, sourcePropertyInfo,eventChangedString, target, targetPropertyInfo,twoWay);
        }

        /// <summary>
        /// Bind from a WinForm event to a ViewModel event. It means that the binding is applied on UI elements.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TSourceValue"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="sourcePropertyInfo"></param>
        /// <param name="eventChangedString"></param>
        /// <param name="target"></param>
        /// <param name="targetPropertyInfo"></param>
        /// <param name="twoWay"></param>
        public static void BindProperty<TSource, TTarget>
                                   (TSource source,
                                    PropertyInfo sourcePropertyInfo,
                                    string eventChangedString,
                                    TTarget target,
                                    PropertyInfo targetPropertyInfo,
                                    bool twoWay = false
                                    )
        {            
            var observable = Observable.FromEventPattern<EventArgs>(source, eventChangedString);
            observable.Subscribe(p => targetPropertyInfo.SetValue(
                                               target, sourcePropertyInfo.GetValue(source, null), null));

            if (twoWay)
            {
                var reverseObservable = Observable.FromEventPattern<PropertyChangedEventArgs>(target, PROPERTY_CHANGED);
                reverseObservable.Subscribe(rp =>
                {
                    if (rp.EventArgs.PropertyName.Equals(targetPropertyInfo.Name))
                    {
                        sourcePropertyInfo.SetValue(source, targetPropertyInfo.GetValue(target, null), null);
                    }
                });
            }
        }

    }
}
