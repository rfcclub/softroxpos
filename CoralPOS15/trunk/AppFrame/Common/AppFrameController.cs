using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using AppFrame.Common.Attributes;
using Spring.Context;
using Spring.Context.Support;

namespace AppFrame.Common
{
    public class AppFrameController
    {
        public IPresenter MainPresenter { get; set; }
        public IPresenter CurrentPresenter { get; set; }
        public Control ContainerControl { get; set; }
        public IDictionary<string, TypeToolBarItem> ToolBars { get; set; }
        private static AppFrameController _appFrameController = null;

        private AppFrameController()
        {
            ToolBars = new Dictionary<string, TypeToolBarItem>();

        }
        public static AppFrameController Instance
        {
            get
            {
                if(_appFrameController ==null)
                    _appFrameController = new AppFrameController();
                return _appFrameController;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TScreen"></typeparam>
        public void Open<TScreen>() where TScreen : IPresenter
        {
            string screen = typeof (TScreen).Name;
            Open(screen);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="screenName"></param>
        public void Open(string screenName)
        {
            // lookup object in spring config
            IPresenter presenter = (IPresenter)ContextLookup(screenName);
            // get view attribute
            Type type = presenter.GetType();
            object[] viewAttributes = type.GetCustomAttributes(typeof(ViewAttribute), false);
            if (viewAttributes.Count() > 0) // has view
            {
                if (ContainerControl != null)
                {
                    // create view and show
                    ViewAttribute attribute = (ViewAttribute) viewAttributes[0];
                    Type vmType = attribute.ViewScreen;
                    object obj = Activator.CreateInstance(vmType);
                    BaseView view = (BaseView) obj;
                    view.Presenter = presenter;
                    view.DoLoad();
                    Show(view, ContainerControl);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="screenName"></param>
        /// <param name="parent"></param>
        public void Open(string screenName,Control parent)
        {
            // lookup object in spring config
            IPresenter presenter = (IPresenter)ContextLookup(screenName);
            // get view attribute
            Type type = presenter.GetType();
            object[] viewAttributes = type.GetCustomAttributes(typeof(ViewAttribute), false);
            if (viewAttributes.Count() > 0) // has view
            {
                    // create view and show
                    ViewAttribute attribute = (ViewAttribute)viewAttributes[0];
                    Type vmType = attribute.ViewScreen;
                    object obj = Activator.CreateInstance(vmType);
                    BaseView view = (BaseView)obj;
                    view.Presenter = presenter;
                    view.DoLoad();
                    Show(view, parent);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        /// <param name="containerControl"></param>
        public void Show(BaseView view, Control containerControl)
        {
            view.Visible = false;
            view.Parent = containerControl;
            view.Dock = DockStyle.Fill;
            view.Visible = true;
        }


        public void ScanToolBar(Assembly assembly)
        {
            var items = from t in assembly.GetTypes()
                                    let attributes = t.GetCustomAttributes(typeof (ToolBarItemAttribute), true)
                                    where attributes != null && attributes.Length > 0
                                    select new {Type= t, Attributes = attributes.Cast<ToolBarItemAttribute>()};
            
            foreach( var item in items)
            {
                ToolBarItemAttribute toolBarItemAttribute = item.Attributes.First();
                if(!ToolBars.ContainsKey(toolBarItemAttribute.ToolBarName))
                {
                    TypeToolBarItem toolbarItem = new TypeToolBarItem
                                               {
                                                   ToolBarType = item.Type,
                                                   ToolBarImage = toolBarItemAttribute.ToolBarImage
                                               };
                    ToolBars[toolBarItemAttribute.ToolBarName] = toolbarItem;
                }
                else
                {
                    if(toolBarItemAttribute.IsOverride)
                    {
                        TypeToolBarItem toolbarItem = new TypeToolBarItem
                        {
                            ToolBarType = item.Type,
                            ToolBarImage = toolBarItemAttribute.ToolBarImage
                        };
                        ToolBars[toolBarItemAttribute.ToolBarName] = toolbarItem;
                    }
                }
            }
        }

        public void UpdateToolBar(Assembly assembly,ToolStrip toolStrip)
        {
            ToolBars.Clear();
            ScanToolBar(assembly);
            toolStrip.Items.Clear();
            foreach (KeyValuePair<string, TypeToolBarItem> toolBar in ToolBars)
            {
                string nameOfType = toolBar.Value.ToolBarType.Name;
                ToolStripButton item = new ToolStripButton(toolBar.Key, null, 
                        (o,e) => AppFrameController.Instance.Open(nameOfType));
                if(!string.IsNullOrEmpty(toolBar.Value.ToolBarImage))
                {
                    // TODO: load toolbar image here
                }
                toolStrip.Items.Add(item);
            }
        }

        public object ContextLookup(string keyName)
        {
            IApplicationContext applicationContext = ContextRegistry.GetContext();
            return applicationContext[keyName];
        }

        public void ShowMessage(string message)
        {
            if(ContainerControl!=null)
            {
                MessageBox.Show(ContainerControl, message);
            }
        }
    }
}
