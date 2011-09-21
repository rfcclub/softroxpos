using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ApplicationFramework
{
	// The base module class of the Application Framework
	public class BaseModule : System.Windows.Forms.UserControl
	{
		private System.ComponentModel.Container components = null;
		private Actions actions;

		public BaseModule()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			// Create actions instance
			this.actions = new Actions();
			// Create the hanler on 
			Actions.OnPerformModuleAction += new ActionModuleHandler(DoActionModuleHandler);
			// Register supported actions
			RegisterActions();
		}
		public Actions Actions { get { return actions; } }
		// This method has to be overrided in the successors classes
		// to update action states, like Enabled and IsDown property
		public virtual void UpdateActions() {}
		// This method has to be overrided in the successors classes 
		// Here you should register supported actions
		protected virtual void RegisterActions() {}
		protected virtual void DoActionModuleHandler(object key, object sender, EventArgs e) {}
		

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					Actions.OnPerformModuleAction -= new ActionModuleHandler(DoActionModuleHandler);
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// BaseModule
			// 
			this.Name = "BaseModule";
			this.Size = new System.Drawing.Size(224, 208);

		}
		#endregion
	}
}
