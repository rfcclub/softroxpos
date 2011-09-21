using System;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace ApplicationFramework {
	// Contains information about a module
	public class ModuleInfo {
		string name;
		Type moduleType;
		BaseModule module;
		public ModuleInfo(string name, Type moduleType) {
			// throw exception if the module is not inherited from BaseModule class
			if(!moduleType.IsSubclassOf(typeof(BaseModule)))
				throw new ArgumentException("moduleClass has to be inherited from BaseModule");
			// if there is no any category yet, create the default one
			this.name = name;
			this.moduleType = moduleType;
			this.module = null;
		}
		public string Name { get { return this.name; } }
		//Show the module on the particular control
		public void Show(Control control) {
			CreateModule();
			module.Visible = false;
			module.Parent = control;
			module.Dock = DockStyle.Fill;
			module.Visible = true;
		}
		//Make the module invisible
		public void Hide() {
			if(module != null) 
				module.Visible = false;
		}
		// Create the module instance
		protected void CreateModule() {
			if(this.module == null) {
				ConstructorInfo constructorInfoObj = moduleType.GetConstructor(Type.EmptyTypes);
				if (constructorInfoObj == null) 
					throw new ApplicationException(moduleType.FullName + " doesn't have public constructor with empty parameters");					
				this.module =  constructorInfoObj.Invoke(null) as BaseModule;
			}
		}
		// Module instance
		public BaseModule Module {	get {  return this.module;	} }
	}

	// The list of modules registered in the system
	[ListBindable(false)]
	public class ModuleInfoCollection : CollectionBase {
		static ModuleInfoCollection instance;
		ModuleInfo currentModule;
		// create the static instance of the class
		static ModuleInfoCollection() {
			instance = new ModuleInfoCollection();
		}
		ModuleInfoCollection() {
			this.currentModule = null;
		}
		public ModuleInfo this[int index] { get { return List[index] as  ModuleInfo; } }
		public ModuleInfo this[string name] {
			get {
				foreach(ModuleInfo info in this) 
					if(info.Name.Equals(name)) 
						return info;
				return null;
			}
		}
		// Register the module in the system
		public static void Add(string name, Type moduleType) {
			ModuleInfo item = new ModuleInfo(name, moduleType);
			instance.Add(item);
		}
		public static ModuleInfoCollection Instance { get { return instance; } }
		//Show the module on the particular control
		public static void ShowModule(ModuleInfo item, Control parent) {
			if(item == instance.currentModule) return;
			if(instance.currentModule != null)
				instance.currentModule.Hide();
			item.Show(parent);
			instance.currentModule = item;
			// update UI action objects
			item.Module.Actions.UpdateVisibility();
			item.Module.UpdateActions();
		}
		// return the currently showing module
		public static ModuleInfo CurrentModuleInfo { get { return instance.currentModule; } }
		void Add(ModuleInfo value) { 
			if(List.IndexOf(value) < 0)
				List.Add(value);
		}
	}
}