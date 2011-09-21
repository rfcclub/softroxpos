using System;

namespace ApplicationFramework
{
	/// <summary>
	/// Summary description for ModuleRegistration.
	/// </summary>
	public class ModulesRegistration
	{
		//Register your modules here
		static public void Register() {
			ModuleInfoCollection.Add("Module1", typeof(Module1));
			ModuleInfoCollection.Add("Module2", typeof(Module2));
		}
	}
}
