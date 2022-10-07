using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XML_Editor
{
	public partial class Form1 : Form
	{
		public XmlDocument XML_Document { get; set; }
		public string FileFullPath { get; set; }
		public string GamePath { get; set; }
		public bool IsDirty { get; set; } = false;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			string _RegActiveInstance = Util.WinReg_ReadKey("EDHM", "ActiveInstance").NVL("");
			if (_RegActiveInstance != null && _RegActiveInstance != string.Empty)
			{
				string GameInstances_JSON = Util.WinReg_ReadKey("EDHM", "GameInstances").NVL(string.Empty);
				if (!GameInstances_JSON.EmptyOrNull())
				{
					var GameInstancesEx = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(GameInstances_JSON);
					if (GameInstancesEx != null)
					{
						Console.WriteLine(GameInstancesEx[0].games[0].path);

						//Seleccionar la Instancia Activa:
						foreach (var instance in GameInstancesEx)
						{
							foreach (var game in instance.games)
							{
								if (game.game_id == _RegActiveInstance)
								{
									this.GamePath = game.path; break;
								}
							}
						}
					}
				}				
			}
		}
		private void Form1_Shown(object sender, EventArgs e)
		{
			if (!this.GamePath.EmptyOrNull())
			{
				if (MessageBox.Show("EDHM UI had been detected in you system.\r\nWould you like to load your ED GraphicsConfiguration.xml ?", "CONGRATULATIONS!",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					this.txtFilePath.Text = Path.Combine(this.GamePath, "GraphicsConfiguration.xml");
					LoadXMLDocument(Path.Combine(this.GamePath, "GraphicsConfiguration.xml"));
				}
			}
			else
			{
				MessageBox.Show("You doesn't seems to have EDHM UI installed in your system, you really should have.");
			}
		}
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.IsDirty)
			{
				if (MessageBox.Show("There are unsaved chnages, would you like to save them before leaving?", "Save Changes?", 
					MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					this.XML_Document.SaveBeautify(this.FileFullPath);
					this.IsDirty = false;
				}
			}
		}

		public void LoadXMLDocument(string FilePath)
		{
			try
			{
				if (FilePath != null && FilePath != string.Empty)
				{
					if (File.Exists(FilePath))
					{
						this.IsDirty = false;
						this.FileFullPath = FilePath;
						dynamic root = new ExpandoObject();
						this.XML_Document = new XmlDocument();
						this.XML_Document.Load(this.FileFullPath);

						//Abre el XML sin dejarlo 'en uso':
						using (var fs = new FileStream(this.FileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
						{
							XDocument xDoc = XDocument.Load(fs);
							//Convierte el XML en un Objeto Dinamico (root)
							XmlToDynamic.Parse(root, xDoc.Elements().First());
						}

						if (root != null)
						{
							//Carga el XML en el TreeView Recursivamente:
							treeView1.BeginUpdate();
							treeView1.Nodes.Clear();

							TreeNode Root_Node = treeView1.Nodes.Add("."); //<- Nodo Raiz
							foreach (var Data in root)
							{
								LoadTreeView(Data, ref Root_Node);
							}
							treeView1.EndUpdate();
						}

						treeView1.ExpandAll();
						treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
						SelectTreeNode(treeView1.Nodes[0].Nodes[0]);
						treeView1.Focus();
					}
				}				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>Recursive Method to Load all the Sections and Keys into the TreeView.</summary>
		/// <param name="RootLevel_Data">Data to Load</param>
		/// <param name="RootLevel_Node">Root Node in the Tree</param>
		public void LoadTreeView(dynamic RootLevel_Data, ref TreeNode RootLevel_Node)
		{
			try
			{
				CustomClass RootLevelLevel_Keys = new CustomClass();
				if (RootLevel_Node.Tag != null)
				{
					RootLevelLevel_Keys = RootLevel_Node.Tag as CustomClass;
				}

				if (RootLevel_Data.Value is ExpandoObject)
				{
					var child = new TreeNode(RootLevel_Data.Key);
					foreach (var SecondLevel in RootLevel_Data.Value)
					{
						LoadTreeView(SecondLevel, ref child);
					}
					RootLevel_Node.Nodes.Add(child);
				}
				else
				{
					RootLevelLevel_Keys.Add(new CustomProperty(RootLevel_Data.Key, RootLevel_Data.Value, RootLevel_Node.Text));
				}
				RootLevel_Node.Tag = RootLevelLevel_Keys;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void SelectTreeNode(TreeNode Node)
		{
			/*  Al seleccionar un item del arbol, cargamos sus Keys (si las hay) y mostramos el Path  */
			try
			{
				if (Node.Tag != null)
				{
					propertyGrid1.SelectedObject = (CustomClass)Node.Tag;
				}
				else
				{
					propertyGrid1.SelectedObject = null;
				}
				txtPath.Text = Node.FullPath;
				this.treeView1.SelectedNode = Node;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		public void FindXPath(string XPath)
		{
			/*  Busca el XPath indicado y lo Selecciona en el TreeView  */
			try
			{
				var _Result = this.treeView1.Nodes.FindTreeNodeByFullPath(XPath);
				if (_Result != null)
				{
					SelectTreeNode(_Result);
					this.treeView1.Focus();
				}
				else
				{
					MessageBox.Show("XPath not found!", "404", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		public void SearchElement(string Query)
		{
			/* Busca en el TreeView y en sus Keys la palabra (Query) indicada  */
			try
			{
				if (this.treeView1.Nodes != null)
				{
					this.txtSearchBox.DataSource = null;

					var FlatNodes = treeView1.FlattenTree();
					if (FlatNodes != null)
					{
						List<ComboboxItem> FoundItems = new List<ComboboxItem>();
						foreach (var node in FlatNodes)
						{
							if (node.Text.ToLowerInvariant().Contains(Query.ToLowerInvariant()))
							{
								FoundItems.Add(new ComboboxItem()
								{
									Value = node,
									Text = node.FullPath,
									Path = node.FullPath
								});
							}
							if (node.Tag != null)
							{
								if (node.Tag is CustomClass Keys)
								{
									foreach (CustomProperty key in Keys)
									{
										if (key.Name.ToLowerInvariant().Contains(Query.ToLowerInvariant()))
										{
											FoundItems.Add(new ComboboxItem()
											{
												Key = key.Name,
												Value = node,
												Text = node.FullPath + '/' + key.Name,
												Path = node.FullPath
											});
										}
									}
								}
							}
						}
						if (FoundItems != null && FoundItems.Count > 0)
						{
							this.txtSearchBox.DataSource = FoundItems;
							this.txtSearchBox.DroppedDown = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			/*  Al seleccionar un item del arbol, cargamos sus Keys (si las hay) y mostramos el Path  */
			SelectTreeNode(e.Node);
		}

		private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			//Este evento ocurre solo cuando el usuario cambia el valor, y la celda activa pierde el foco o se presiona ENTER

			var OldValue = e.OldValue;
			var NewValue = e.ChangedItem.Value;			

			var Category = e.ChangedItem.PropertyDescriptor.Category;
			var Key = e.ChangedItem.Label;

			var XPath = (this.txtPath.Text.Substring(0, 1) == ".") ? 
				this.txtPath.Text.Substring(1, this.txtPath.Text.Length - 1) : 
				this.txtPath.Text;

			//Aqui aplicamos el valor cambiado:
			this.XML_Document.SetValue(Path.Combine(XPath, Key), NewValue.ToString());
			this.IsDirty = true;
		}

		private void cmdSaveChanges_Click(object sender, EventArgs e)
		{
			this.XML_Document.SaveBeautify(this.FileFullPath);
			this.IsDirty = false;
		}

		private void cmdFindXPath_Click(object sender, EventArgs e)
		{
			FindXPath(this.txtPath.Text);
		}

		private void txtPath_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				//Se Presionó la Tecla ENTER
				FindXPath(this.txtPath.Text);
			}
		}

		private void cmdSearch_Click(object sender, EventArgs e)
		{
			SearchElement(this.txtSearchBox.Text);
		}
		private void txtSearchBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				//Se Presionó la Tecla ENTER
				SearchElement(this.txtSearchBox.Text);
			}
		}
		private void txtSearchBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			var Item = txtSearchBox.SelectedItem as ComboboxItem;
			FindXPath(Item.Path);

			if (Item.Key != null && Item.Key != string.Empty)
			{
				GridItem gi = propertyGrid1.EnumerateAllItems().First((item) =>
				   item.PropertyDescriptor != null &&
				   item.PropertyDescriptor.Name == Item.Key);

				// select it
				propertyGrid1.Focus();
				gi.Select();
			}			
		}

		private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
		{
			if (e.Node == null) return;

			// if treeview's HideSelection property is "True", 
			// this will always returns "False" on unfocused treeview
			var selected = (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected;
			var unfocused = !e.Node.TreeView.Focused;

			// we need to do owner drawing only on a selected node
			// and when the treeview is unfocused, else let the OS do it for us
			if (selected && unfocused)
			{
				var font = e.Node.NodeFont ?? e.Node.TreeView.Font;
				e.Graphics.FillRectangle(System.Drawing.SystemBrushes.Highlight, e.Bounds);
				TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, System.Drawing.SystemColors.HighlightText, TextFormatFlags.GlyphOverhangPadding);
			}
			else
			{
				e.DrawDefault = true;
			}
		}

		private void cmdSelectFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog OFDialog = new OpenFileDialog()
			{
				Filter = "XML Data|*.xml",
				FilterIndex = 0,
				DefaultExt = "xml",
				AddExtension = true,
				CheckPathExists = true,
				CheckFileExists = true,
				InitialDirectory = (!this.GamePath.EmptyOrNull()) ? this.GamePath : Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
			};
			if (OFDialog.ShowDialog() == DialogResult.OK)
			{
				this.txtFilePath.Text = OFDialog.FileName;
				LoadXMLDocument(OFDialog.FileName);
			}
		}
	}

	public class ComboboxItem
	{
		public string Text { get; set; }
		public object Value { get; set; }
		public string Path { get; set; }
		public string Key { get; set; }

		public override string ToString()
		{
			return Text;
		}
	}
}
