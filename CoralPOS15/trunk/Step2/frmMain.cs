using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ApplicationFramework
{
	/// <summary>
	/// Summary description for frmMain.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem mView;
		private System.Windows.Forms.Panel pnlNavigation;
		private System.Windows.Forms.Splitter splitter;
		private System.Windows.Forms.Panel pnlWorkingArea;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ListBox lbNavigation;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1;
		private System.ComponentModel.IContainer components;

		public frmMain()
		{
            //
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			// Set up menu and navigation controls
			RegisterModules();
			// Register actions
			RegisterActions();
			//Show the first module by default
			if(ModuleInfoCollection.Instance.Count > 0) 
				ModuleInfoCollection.ShowModule(ModuleInfoCollection.Instance[0], pnlWorkingArea);
		}
		
		// Set up menu and navigation controls
		private void RegisterModules() {
			foreach(ModuleInfo mInfo in ModuleInfoCollection.Instance) {
				lbNavigation.Items.Add(mInfo.Name);
				// Create menu item
				MenuItem menuItem = new MenuItem();
				// Add menu item into 'View' sub menu
				this.mView.MenuItems.Add(menuItem);
				//Set up menu text
				menuItem.Text = mInfo.Name;
				// Set up the handler on Click event
				menuItem.Click += new System.EventHandler(this.mView_Click);
			}
		}
		private void RegisterActions() {
			Actions.RegisterAction(ActionKeys.Operation1, new ToolBarButtonAction(this.toolBarButton1));
			Actions.RegisterAction(ActionKeys.Operation2, new ToolBarButtonAction(this.toolBarButton2));
			Actions.RegisterAction(ActionKeys.Operation3, new ToolBarButtonAction(this.toolBarButton3));
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mView = new System.Windows.Forms.MenuItem();
            this.pnlNavigation = new System.Windows.Forms.Panel();
            this.lbNavigation = new System.Windows.Forms.ListBox();
            this.splitter = new System.Windows.Forms.Splitter();
            this.pnlWorkingArea = new System.Windows.Forms.Panel();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlNavigation.SuspendLayout();
            this.pnlWorkingArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.mView});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2});
            this.menuItem1.Text = "File";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Exit";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // mView
            // 
            this.mView.Index = 1;
            this.mView.Text = "View";
            // 
            // pnlNavigation
            // 
            this.pnlNavigation.Controls.Add(this.lbNavigation);
            this.pnlNavigation.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNavigation.Location = new System.Drawing.Point(0, 28);
            this.pnlNavigation.Name = "pnlNavigation";
            this.pnlNavigation.Size = new System.Drawing.Size(119, 322);
            this.pnlNavigation.TabIndex = 0;
            // 
            // lbNavigation
            // 
            this.lbNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNavigation.Location = new System.Drawing.Point(0, 0);
            this.lbNavigation.Name = "lbNavigation";
            this.lbNavigation.Size = new System.Drawing.Size(119, 322);
            this.lbNavigation.TabIndex = 1;
            this.lbNavigation.SelectedIndexChanged += new System.EventHandler(this.lbNavigation_SelectedIndexChanged);
            // 
            // splitter
            // 
            this.splitter.Location = new System.Drawing.Point(119, 28);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(3, 322);
            this.splitter.TabIndex = 1;
            this.splitter.TabStop = false;
            // 
            // pnlWorkingArea
            // 
            this.pnlWorkingArea.Controls.Add(this.dataGridView1);
            this.pnlWorkingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWorkingArea.Location = new System.Drawing.Point(122, 28);
            this.pnlWorkingArea.Name = "pnlWorkingArea";
            this.pnlWorkingArea.Size = new System.Drawing.Size(400, 322);
            this.pnlWorkingArea.TabIndex = 2;
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButton1,
            this.toolBarButton2,
            this.toolBarButton3});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(522, 28);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(32, 46);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(522, 350);
            this.Controls.Add(this.pnlWorkingArea);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.pnlNavigation);
            this.Controls.Add(this.toolBar1);
            this.Menu = this.mainMenu1;
            this.Name = "frmMain";
            this.Text = "Application Framework. Step 2";
            this.pnlNavigation.ResumeLayout(false);
            this.pnlWorkingArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			// call the module registration before run the main form
			ModulesRegistration.Register();
			Application.Run(new frmMain());
		}
		// Change the module on changing selected index in the navigation listbox
		// Change the module on navigation menu item click
		private void mView_Click(object sender, System.EventArgs e) {
			ModuleInfoCollection.ShowModule(ModuleInfoCollection.Instance[((MenuItem)sender).Text], pnlWorkingArea);
		}
		private void menuItem2_Click(object sender, System.EventArgs e) {
			Close();
		}
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			Actions.PerformAction(e.Button.Tag as Action, sender, e);
		}

		private void lbNavigation_SelectedIndexChanged(object sender, System.EventArgs e) {
			if(lbNavigation.SelectedIndex > -1)
				ModuleInfoCollection.ShowModule(ModuleInfoCollection.Instance[lbNavigation.SelectedIndex], pnlWorkingArea);
		}

	}
	// Action class for .Net TooBar button
	public class ToolBarButtonAction : Action {
		public ToolBarButtonAction(ToolBarButton button): base(button) {
			// Use Tag property to bind Button object with action
			button.Tag = this;
		}
		public ToolBarButton Button { get { return Control as ToolBarButton; } }
		public  override bool Visible { get { return Button.Visible; } set { Button.Visible = value; } }
		public override bool Enabled { get { return Button.Enabled; } set { Button.Enabled = value; } }
		public override bool IsDown { get { return Button.Pushed; } set { Button.Pushed = value; } }
	}


}
