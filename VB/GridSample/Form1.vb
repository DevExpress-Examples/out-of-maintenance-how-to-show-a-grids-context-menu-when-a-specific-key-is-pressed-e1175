Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Menu

Namespace GridSample
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Public Class Form1
		Inherits System.Windows.Forms.Form
		Private gridControl1 As DevExpress.XtraGrid.GridControl
		Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()

			'
			' TODO: Add any constructor code after InitializeComponent call
			'
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
			Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' gridControl1
			' 
			Me.gridControl1.Location = New System.Drawing.Point(40, 40)
			Me.gridControl1.MainView = Me.gridView1
			Me.gridControl1.Name = "gridControl1"
			Me.gridControl1.Size = New System.Drawing.Size(672, 304)
			Me.gridControl1.TabIndex = 0
			Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1})
			' 
			' gridView1
			' 
			Me.gridView1.GridControl = Me.gridControl1
			Me.gridView1.Name = "gridView1"
'			Me.gridView1.KeyDown += New System.Windows.Forms.KeyEventHandler(Me.gridView1_KeyDown);
			' 
			' Form1
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(776, 422)
			Me.Controls.Add(Me.gridControl1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub
		#End Region

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread> _
		Shared Sub Main()
			Application.Run(New Form1())
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
			InitData()
			FillData()
			InitGridColumns()
		End Sub

		'<gridControl1>
		Private data As DataTable
		Private Sub InitData()
			data = New DataTable("ColumnsTable")
			data.BeginInit()
			AddColumn(data, "ID", System.Type.GetType("System.Int32"), True)
			AddColumn(data, "First Name", System.Type.GetType("System.String"))
			AddColumn(data, "Last Name", System.Type.GetType("System.String"))
			AddColumn(data, "Payment Type", System.Type.GetType("System.String"))
			AddColumn(data, "Customer", System.Type.GetType("System.Boolean"))
			AddColumn(data, "Payment Amount", System.Type.GetType("System.Single"))
			data.EndInit()
		End Sub
		'</gridControl1>

		Private Sub AddColumn(ByVal data As DataTable, ByVal name As String, ByVal type As System.Type)
			AddColumn(data, name, type, False)
		End Sub
		Private Sub AddColumn(ByVal data As DataTable, ByVal name As String, ByVal type As System.Type, ByVal ro As Boolean)
			Dim col As DataColumn
			col = New DataColumn(name, type)
			col.Caption = name
			col.ReadOnly = ro
			data.Columns.Add(col)
		End Sub

		Private Sub FillData()
			Dim sNames(,) As String = { { "Elizabeth", "Lincoln"}, {"Yang", "Wang"}, { "Patricio", "Simpson"}, {"Francisco", "Chang"}, { "Ann", "Devon"}, {"Roland", "Mendel"}, { "Paolo", "Accorti"}, {"Diego", "Roel"}}
			Dim sType() As String = {"Visa", "Master", "Cash"}
			data.Clear()
			Dim rnd As New Random()
			For i As Integer = 0 To sNames.GetUpperBound(0)
				data.Rows.Add(New Object() {i + 1, sNames(i,0), sNames(i,1), sType(i Mod 3), rnd.Next(-1, 1), rnd.Next(10000) * 0.01})
			Next i
		End Sub

		'<gridControl1>
		Private Sub InitGridColumns()
			gridControl1.DataSource = data
			'The line bellow creates columns on the fly.
			gridControl1.DefaultView.PopulateColumns()
			gridView1.Columns("Payment Amount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
			gridView1.Columns("Payment Amount").DisplayFormat.FormatString = "c"
			gridView1.BestFitColumns()
		End Sub

		Private Sub gridView1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles gridView1.KeyDown
			If e.KeyCode = Keys.Apps Then
				Dim vi As GridViewInfo = TryCast((TryCast(sender, GridView)).GetViewInfo(), GridViewInfo)
				Dim r As Rectangle = vi.ColumnsInfo((TryCast(sender, GridView)).FocusedColumn).Bounds
				Dim p As New Point(r.Left, r.Bottom)
				Dim menu As New GridViewColumnMenu(TryCast(sender, GridView))
				menu.Init((TryCast(sender, GridView)).FocusedColumn)
				menu.Show(p)
			End If

		End Sub
	End Class
End Namespace
