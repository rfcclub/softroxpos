using System;
using System.Collections;

namespace ApplicationFramework {
	
	// The abstract Action class
	public abstract class Action {
		object control;
		object key;

		public Action(object control) {
			this.control = control;
		}
		// If true the action is implemented in the particular module and UI object is visible
		// If false the action is not implemented and UI object is invisible
		public abstract bool Visible { get; set; }
		// Enabled state of the UI object
		public abstract bool Enabled { get; set; }
		// True if the UI control in the down/pushed state
		public virtual bool IsDown { get { return false; } set {} }
		// The link to the UI control
		protected object Control { get { return control; } }
		// The action identifying code
		internal protected object Key { get { return key; } set { key = value; } }
	}

	public delegate void ActionModuleHandler(object key, object sender, EventArgs e);

	// The action manager class
	// It is created in every module
	// There is the static hashtable of registered global actions. 
	public class Actions {
		// The global action list
		static Hashtable registeredActions;
		// the list of supported actions in the particular module
		Hashtable supportedActions;

		static Actions() {
			registeredActions = new Hashtable();
		}
		// Register the global action. The key is the action identifying code
		public static void RegisterAction(object key, Action action) {
			// If there is already action with the same identifying code, thow exception
			if(Actions.registeredActions[key] != null) 
				throw new ApplicationException(string.Format("There is already register action with the key '{0}'.", key));
			// Add action into the static hash table
			Actions.registeredActions.Add(key, action);		
			action.Key = key;
		}
		public static void PerformAction(object key, object sender, EventArgs e) {
			// For the currently showing module
			if(ModuleInfoCollection.CurrentModuleInfo != null) {
				BaseModule module = ModuleInfoCollection.CurrentModuleInfo.Module;
				// call perform action for the currenlty showing module
				module.Actions.PerformModuleAction(key, sender, e);
				module.UpdateActions();
			}
		}
		public static void PerformAction(Action action, object sender, EventArgs e) {
			Actions.PerformAction(action.Key, sender, e);
		}
		// This event will be handled in the BaseModule class
		public event ActionModuleHandler OnPerformModuleAction;
		public Actions() {
			// create the hashtable of the supported actions in the current module
			this.supportedActions = new Hashtable();
		}
		// Tell that the action will be supported
		// Provide actionHandler parameter if you want to perform the operation
		// on the action in the separate method
		// If the actionHandler is null, Actions.PerformAction method will be called
		// Please look at PerformModuleAction method
		public void AddSupportedAction(object key, ActionModuleHandler actionHandler) {
			if(! Actions.registeredActions.ContainsKey(key))
				new System.Exception(string.Format("The action key '{0}' is incorrect", key));
			this.supportedActions.Add(key, actionHandler);
		}
		public void AddSupportedAction(object key) {
			AddSupportedAction(key, null);
		}
		// Remove the action from the supported action list
		public void RemoveSupportedActions(object key) {
			this.supportedActions.Remove(key);
		}
		public Action this[object key] {
			get {
				if(! this.supportedActions.ContainsKey(key))
					return null;
				else return Actions.registeredActions[key] as Action;
			}
		}
		// Make UI controls binded with supported actions visible
		// and UI contorls binded with non-supported actions invisible
		public void UpdateVisibility() {
			foreach(object key in Actions.registeredActions.Keys) 
				((Action)Actions.registeredActions[key]).Visible = this.supportedActions.ContainsKey(key);
		}
		// It is called on clicking UI controls binded with actions
		public void PerformModuleAction(object key, object sender, EventArgs e) {
			object handler = this.supportedActions[key];
			if(handler != null)
				((ActionModuleHandler)handler)(key, sender, e);
			else {
				if(this.OnPerformModuleAction != null) 
					this.OnPerformModuleAction(key, sender, e);
			}

		}
	}
}