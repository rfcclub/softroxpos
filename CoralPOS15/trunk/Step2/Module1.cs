using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace ApplicationFramework
{
	public class Module1 : ApplicationFramework.BaseModule
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox1;
		private System.ComponentModel.IContainer components = null;

		public Module1()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}
		// Update actions state
		public override void UpdateActions() {
			base.UpdateActions();
			// Made action1 enabled if the checkbox1 is unchecked
			Actions[ActionKeys.Operation1].Enabled = ! this.checkBox1.Checked;
		}
		protected override void RegisterActions() {
			base.RegisterActions();
			// we will support only one new action in this module: Operation1
			Actions.AddSupportedAction(ActionKeys.Operation1, new ActionModuleHandler(DoOperation1));
		}
		void DoOperation1(object key, object sender, EventArgs e) {
			// We will show the number Action1 has been performed in the textBox1
			this.textBox1.Text = (int.Parse(this.textBox1.Text) + 1).ToString();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Module 1";
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(10, 80);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(144, 24);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Text = "Disable Action1";
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 120);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(176, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Action1 has been performed:";
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(202, 120);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(72, 22);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = "0";
			// 
			// Module1
			// 
			this.BackColor = System.Drawing.Color.Blue;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox1,
																		  this.label2,
																		  this.checkBox1,
																		  this.label1});
			this.Name = "Module1";
			this.Size = new System.Drawing.Size(320, 256);
			this.ResumeLayout(false);

		}
		#endregion

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e) {
			UpdateActions();
		}
	}
}

