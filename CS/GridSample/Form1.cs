using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Menu;

namespace GridSample
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(40, 40);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(672, 304);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(776, 422);
            this.Controls.Add(this.gridControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

        private void Form1_Load(object sender, System.EventArgs e) {
            InitData();
            FillData();
            InitGridColumns();
        }

        //<gridControl1>
        private DataTable data;
        private void InitData() {
            data = new DataTable("ColumnsTable");
            data.BeginInit();
            AddColumn(data, "ID", System.Type.GetType("System.Int32"), true);
            AddColumn(data, "First Name", System.Type.GetType("System.String"));
            AddColumn(data, "Last Name", System.Type.GetType("System.String"));
            AddColumn(data, "Payment Type", System.Type.GetType("System.String"));
            AddColumn(data, "Customer", System.Type.GetType("System.Boolean"));
            AddColumn(data, "Payment Amount", System.Type.GetType("System.Single"));
            data.EndInit();
        }
        //</gridControl1>

        private void AddColumn(DataTable data, string name, System.Type type) { AddColumn(data, name, type, false); } 
        private void AddColumn(DataTable data, string name, System.Type type, bool ro) {
            DataColumn col;
            col = new DataColumn(name, type);
            col.Caption = name;
            col.ReadOnly = ro;
            data.Columns.Add(col);
        }

        private void FillData() {
            string[,] sNames = new string[,] { {
                                                   "Elizabeth", "Lincoln"}, {"Yang", "Wang"}, { 
                                                                                                  "Patricio", "Simpson"}, {"Francisco", "Chang"}, { 
                                                                                                                                                      "Ann", "Devon"}, {"Roland", "Mendel"}, { 
                                                                                                                                                                                                 "Paolo", "Accorti"}, {"Diego", "Roel"}}; 
            string[] sType = new string[] {"Visa", "Master", "Cash"};
            data.Clear();
            Random rnd = new Random();
            for(int i = 0; i <= sNames.GetUpperBound(0); i++) 
                data.Rows.Add(new object[] {i + 1, sNames[i,0], sNames[i,1], sType[i % 3], rnd.Next(-1, 1), rnd.Next(10000) * 0.01});
        }

        //<gridControl1>
        private void InitGridColumns() {
            gridControl1.DataSource = data;
            //The line bellow creates columns on the fly.
            gridControl1.DefaultView.PopulateColumns();
            gridView1.Columns["Payment Amount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns["Payment Amount"].DisplayFormat.FormatString = "c";
            gridView1.BestFitColumns();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Apps) {
                GridViewInfo vi = (sender as GridView).GetViewInfo() as GridViewInfo;
                Rectangle r = vi.ColumnsInfo[(sender as GridView).FocusedColumn].Bounds;
                Point p = new Point(r.Left, r.Bottom);
                GridViewColumnMenu menu = new GridViewColumnMenu(sender as GridView);
                menu.Init((sender as GridView).FocusedColumn);
                menu.Show(p);
            }

        }
    }
}
