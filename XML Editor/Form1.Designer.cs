namespace XML_Editor
{
	partial class Form1
	{
		/// <summary>
		/// Variable del diseñador necesaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén usando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido de este método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdSelectFile = new System.Windows.Forms.Button();
			this.txtFilePath = new System.Windows.Forms.TextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.cmdFindXPath = new System.Windows.Forms.Button();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cmdSaveChanges = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.panel3 = new System.Windows.Forms.Panel();
			this.txtSearchBox = new System.Windows.Forms.ComboBox();
			this.cmdSearch = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.cmdSelectFile);
			this.panel1.Controls.Add(this.txtFilePath);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(857, 34);
			this.panel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "XML File:";
			// 
			// cmdSelectFile
			// 
			this.cmdSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSelectFile.Location = new System.Drawing.Point(776, 5);
			this.cmdSelectFile.Name = "cmdSelectFile";
			this.cmdSelectFile.Size = new System.Drawing.Size(75, 23);
			this.cmdSelectFile.TabIndex = 1;
			this.cmdSelectFile.Text = "&Select..";
			this.cmdSelectFile.UseVisualStyleBackColor = true;
			this.cmdSelectFile.Click += new System.EventHandler(this.cmdSelectFile_Click);
			// 
			// txtFilePath
			// 
			this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFilePath.Location = new System.Drawing.Point(70, 6);
			this.txtFilePath.Name = "txtFilePath";
			this.txtFilePath.Size = new System.Drawing.Size(699, 20);
			this.txtFilePath.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.cmdFindXPath);
			this.panel2.Controls.Add(this.txtPath);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.cmdSaveChanges);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 619);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(857, 34);
			this.panel2.TabIndex = 1;
			// 
			// cmdFindXPath
			// 
			this.cmdFindXPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdFindXPath.Location = new System.Drawing.Point(656, 5);
			this.cmdFindXPath.Name = "cmdFindXPath";
			this.cmdFindXPath.Size = new System.Drawing.Size(31, 23);
			this.cmdFindXPath.TabIndex = 3;
			this.cmdFindXPath.Text = "Go";
			this.cmdFindXPath.UseVisualStyleBackColor = true;
			this.cmdFindXPath.Click += new System.EventHandler(this.cmdFindXPath_Click);
			// 
			// txtPath
			// 
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPath.Location = new System.Drawing.Point(37, 6);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(613, 20);
			this.txtPath.TabIndex = 1;
			this.txtPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPath_KeyPress);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Path:";
			// 
			// cmdSaveChanges
			// 
			this.cmdSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSaveChanges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.cmdSaveChanges.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cmdSaveChanges.ForeColor = System.Drawing.Color.Black;
			this.cmdSaveChanges.Location = new System.Drawing.Point(755, 5);
			this.cmdSaveChanges.Name = "cmdSaveChanges";
			this.cmdSaveChanges.Size = new System.Drawing.Size(90, 23);
			this.cmdSaveChanges.TabIndex = 0;
			this.cmdSaveChanges.Text = "&Save Changes";
			this.cmdSaveChanges.UseVisualStyleBackColor = false;
			this.cmdSaveChanges.Click += new System.EventHandler(this.cmdSaveChanges_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 34);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
			this.splitContainer1.Panel2.Controls.Add(this.panel3);
			this.splitContainer1.Size = new System.Drawing.Size(857, 585);
			this.splitContainer1.SplitterDistance = 284;
			this.splitContainer1.TabIndex = 2;
			// 
			// treeView1
			// 
			this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
			this.treeView1.HideSelection = false;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.PathSeparator = "/";
			this.treeView1.Size = new System.Drawing.Size(284, 585);
			this.treeView1.TabIndex = 0;
			this.treeView1.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeView1_DrawNode);
			this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid1.Location = new System.Drawing.Point(0, 38);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this.propertyGrid1.Size = new System.Drawing.Size(569, 547);
			this.propertyGrid1.TabIndex = 0;
			this.propertyGrid1.ToolbarVisible = false;
			this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.txtSearchBox);
			this.panel3.Controls.Add(this.cmdSearch);
			this.panel3.Controls.Add(this.label2);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(569, 38);
			this.panel3.TabIndex = 1;
			// 
			// txtSearchBox
			// 
			this.txtSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearchBox.FormattingEnabled = true;
			this.txtSearchBox.Location = new System.Drawing.Point(54, 9);
			this.txtSearchBox.Name = "txtSearchBox";
			this.txtSearchBox.Size = new System.Drawing.Size(427, 21);
			this.txtSearchBox.TabIndex = 4;
			this.txtSearchBox.SelectedIndexChanged += new System.EventHandler(this.txtSearchBox_SelectedIndexChanged);
			this.txtSearchBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchBox_KeyPress);
			// 
			// cmdSearch
			// 
			this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSearch.Location = new System.Drawing.Point(488, 8);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Size = new System.Drawing.Size(75, 23);
			this.cmdSearch.TabIndex = 2;
			this.cmdSearch.Text = "Find";
			this.cmdSearch.UseVisualStyleBackColor = true;
			this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Search:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(857, 653);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Name = "Form1";
			this.Text = "Blue\'s XML Editor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdSelectFile;
		private System.Windows.Forms.TextBox txtFilePath;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button cmdSearch;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cmdSaveChanges;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button cmdFindXPath;
		private System.Windows.Forms.ComboBox txtSearchBox;
	}
}

