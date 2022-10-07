using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XML_Editor
{
	public static class Util
	{
		#region Generic Methods

		/// <summary>Devuelve un Valor por Defecto si la cadena es Null o Vacia.</summary>
		/// <param name="pTexto">Cadena de Texto</param>
		/// <param name="defaultValue">Valor x Defecto</param>
		/// <param name="considerWhiteSpaceIsEmpty">Los Espacios se consideran como Vacio?</param>
		public static string NVL(this object pTexto, string defaultValue, bool considerWhiteSpaceIsEmpty = true)
		{
			string _ret = defaultValue;
			if (pTexto != null)
			{
				if (considerWhiteSpaceIsEmpty)
				{
					if (pTexto.ToString().Trim() != string.Empty)
					{
						_ret = pTexto.ToString().Trim();
					}
				}
				else
				{
					_ret = pTexto.ToString();
				}
			}
			return _ret;
		}

		/// <summary>Devuelve 'true' si la Cadena es Nula o Vacia.</summary>
		/// <param name="pValor">Cadena de Texto</param>
		public static bool EmptyOrNull(this string Value, bool Default = true)
		{
			bool _ret = Default;
			if (Value != null && Value != string.Empty)
			{
				_ret = false;
			}
			return _ret;
		}

		#endregion

		#region Windows Registry

		/// <summary>Lee una Clave del Registro de Windows para el Usuario Actual.
		/// Las Claves en este caso siempre se Leen desde 'HKEY_CURRENT_USER\Software\Elte Dangerous\Mods\'.</summary>
		/// <param name="Sistema">Nombre del Sistema que guarda las Claves, ejem: RRHH, Contaduria, CutcsaPagos, etc.</param>
		/// <param name="KeyName">Nombre de la Clave a Leer</param>
		/// <returns>Devuelve NULL si la clave no existe</returns>
		public static object WinReg_ReadKey(string Sistema, string KeyName)
		{
			Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.CurrentUser;
			Microsoft.Win32.RegistryKey sk1 = rk.OpenSubKey(@"Software\Elte Dangerous\Mods\" + Sistema);

			// Si la Clave no existe u ocurre un error al leerla, devuelve NULL
			if (sk1 == null)
			{
				return null;
			}
			else
			{
				try { return sk1.GetValue(KeyName); }
				catch { return null; }
			}
		}

		/// <summary>Escribe un Valor en una Clave del Registro de Windows para el Usuario Actual.
		/// Las Claves en este caso se Guardan siempre en 'HKEY_CURRENT_USER\Software\Cutcsa\DXCutcsa'.</summary>
		/// <param name="Sistema">Nombre del Sistema que guarda las Claves, ejem: RRHH, Contaduria, CutcsaPagos, etc.</param>
		/// <param name="KeyName">Nombre de la Clave a guardar, Si no existe se crea.</param>
		/// <param name="Value">Valor a Guardar</param>
		/// <returns>Devuelve TRUE si se guardo el valor Correctamente</returns>
		public static bool WinReg_WriteKey(string Sistema, string KeyName, object Value)
		{
			try
			{
				Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.CurrentUser;
				Microsoft.Win32.RegistryKey sk1 = rk.CreateSubKey(@"Software\Elte Dangerous\Mods\" + Sistema);
				sk1.SetValue(KeyName, Value);

				return true; //<-La Clave se Guardo Exitosamente!
			}
			catch { return false; }
		}

		#endregion

		#region Serializacion XML

		//using System.Xml;
		//using System.Xml.Linq;
		//private XmlDocument _XmlReader = new XmlDocument();
		//_XmlReader.Load(@"C:\Miarchivo.xml");

		/// <summary>Serializa un Objeto a XML.</summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="serializableObject"></param>
		/// <param name="fileName"></param>
		public static void Serialize_ToXML<T>(T serializableObject, string fileName)
		{
			if (serializableObject == null) { return; }

			try
			{
				System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
				System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(serializableObject.GetType());
				using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
				{
					serializer.Serialize(stream, serializableObject);
					stream.Position = 0;
					xmlDocument.Load(stream);
					xmlDocument.Save(fileName);
				}
			}
			catch (Exception ex)
			{
				//Log exception here
			}
		}

		/// <summary>
		/// Deserializes an xml file into an object list
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static T DeSerialize_FromXML<T>(string fileName)
		{
			if (string.IsNullOrEmpty(fileName)) { return default(T); }

			T objectOut = default(T);

			try
			{
				System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
				xmlDocument.Load(fileName);
				string xmlString = xmlDocument.OuterXml;

				using (System.IO.StringReader read = new System.IO.StringReader(xmlString))
				{
					Type outType = typeof(T);

					System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(outType);
					using (System.Xml.XmlReader reader = new System.Xml.XmlTextReader(read))
					{
						objectOut = (T)serializer.Deserialize(reader);
					}
				}
			}
			catch (Exception ex)
			{
				//Log exception here
			}

			return objectOut;
		}

		/// <summary>Gets the value of an XML Element or Attribute using an XPath to identify it.</summary>
		/// <param name="xmlFile">XML Document to write in</param>
		/// <param name="Path">XPath string, ex: '/GraphicsConfig/GalaxyMap[@Something='Server']' /</param>
		/// <param name="Key">Name of the Key which value we want</param>
		/// <param name="DefaultValue">Default value to return if the key or value is missing.</param>
		public static string GetValue(this XmlDocument xmlFile, string Path, string Key, string DefaultValue = "")
		{
			string _ret = DefaultValue;
			try
			{
				if (xmlFile != null && Path != string.Empty)
				{
					var a = xmlFile.SelectSingleNode(Path + "/" + Key);
					if (a != null)
					{
						_ret = a.InnerText;
					}
				}

				//string s = root.Attributes["success"].Value;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return _ret;
		}

		/// <summary>Sets the value of an XML Element or Attribute using an XPath to identify it.</summary>
		/// <param name="xmlFile">XML Document to write in</param>
		/// <param name="Path">XPath string, ex: '/GraphicsConfig/GalaxyMap[@Something='Server']' /</param>
		/// <param name="Value">Value to Write</param>
		public static void SetValue(this XmlDocument xmlFile, string Path, string Value)
		{
			if (xmlFile == null) throw new ArgumentNullException("doc no loaded");
			if (string.IsNullOrEmpty(Path)) throw new ArgumentNullException("xpath is null");

			Path = Path.Replace(@"\", @"/");

			XmlNodeList nodes = xmlFile.SelectNodes(Path);
			if (nodes.Count > 1)
			{
				throw new Exception("Xpath '" + Path + "' was not found multiple times!");
			}
			else if (nodes.Count == 0)
			{
				createXPath(xmlFile, Path).InnerText = Value;
			}
			else
			{
				nodes[0].InnerText = Value;
			}
		}

		/// <summary>Save the changes in the XML file, preserving indentation and Encoding.</summary>
		/// <param name="doc">XML Document to SAve</param>
		/// <param name="FilePath">Full File Path</param>
		public static void SaveBeautify(this XmlDocument doc, string FilePath)
		{
			var settings = new XmlWriterSettings
			{
				Indent = true,                                      //<- Preserve Indentation
				IndentChars = "\t",                                 //<- Uses TAB to indent
				NewLineChars = Environment.NewLine,                 //<- Uses Windows linebreaks
				NewLineHandling = NewLineHandling.Replace,
				Encoding = new System.Text.UTF8Encoding(false)      //<- UTF8 no BOM
			};
			using (XmlWriter writer = XmlWriter.Create(FilePath, settings))
			{
				doc.Save(writer);
			}
		}

		private static IEnumerable<XElement> GetXMLQuery(System.Xml.Linq.XDocument xmlFile, string[] sectionPath)
		{
			if (xmlFile != null && sectionPath != null)
			{
				var query = from Config in xmlFile.Elements("root") select Config;

				switch (sectionPath.Length)
				{
					case 1:
						query = from Config in xmlFile.Elements(sectionPath[0]) select Config;
						break;

					case 2:
						query = from Config in xmlFile.Elements(sectionPath[0]).Elements(sectionPath[1]) select Config;
						break;

					case 3:
						query = from Config in xmlFile.Elements(sectionPath[0]).Elements(sectionPath[1]).Elements(sectionPath[2]) select Config;
						break;

					case 4:
						query = from Config in xmlFile.Elements(sectionPath[0]).Elements(sectionPath[1]).Elements(sectionPath[2]).Elements(sectionPath[3]) select Config;
						break;

					case 5:
						query = from Config in xmlFile.Elements(sectionPath[0]).Elements(sectionPath[1]).Elements(sectionPath[2]).Elements(sectionPath[3]).Elements(sectionPath[4]) select Config;
						break;

					default:
						break;
				}

				return query;
			}
			else
			{
				return null;
			}
		}
		private static XmlNode createXPath(XmlDocument doc, string xpath)
		{
			XmlNode node = doc;
			foreach (string part in xpath.Substring(1).Split('/'))
			{
				XmlNodeList nodes = node.SelectNodes(part);
				if (nodes.Count > 1)
				{
					throw new Exception("Xpath '" + xpath + "' was not found multiple times!");
				}
				else if (nodes.Count == 1) { node = nodes[0]; continue; }

				if (part.StartsWith("@")) //<- es un Atributo
				{
					var anode = doc.CreateAttribute(part.Substring(1));
					node.Attributes.Append(anode);
					node = anode;
				}
				else //<- es un Nodo
				{
					string elName, attrib = null;
					if (part.Contains("["))
					{
						part.SplitOnce("[", out elName, out attrib);
						if (!attrib.EndsWith("]"))
						{
							throw new Exception("Unsupported XPath (missing ]): " + part);
						}

						attrib = attrib.Substring(0, attrib.Length - 1);
					}
					else
					{
						elName = part;
					}

					try
					{
						XmlNode next = doc.CreateElement(elName);
						node.AppendChild(next);
						node = next;
					}
					catch { }

					if (attrib != null)
					{
						if (!attrib.StartsWith("@"))
						{
							throw new Exception("Unsupported XPath attrib (missing @): " + part);
						}

						string name, value;
						attrib.Substring(1).SplitOnce("='", out name, out value);
						if (string.IsNullOrEmpty(value) || !value.EndsWith("'"))
						{
							throw new Exception("Unsupported XPath attrib: " + part);
						}

						value = value.Substring(0, value.Length - 1);
						var anode = doc.CreateAttribute(name);
						anode.Value = value;
						node.Attributes.Append(anode);
					}
				}
			}
			return node;
		}
		private static void SplitOnce(this string value, string separator, out string part1, out string part2)
		{
			if (value != null)
			{
				int idx = value.IndexOf(separator);
				if (idx >= 0)
				{
					part1 = value.Substring(0, idx);
					part2 = value.Substring(idx + separator.Length);
				}
				else
				{
					part1 = value;
					part2 = null;
				}
			}
			else
			{
				part1 = "";
				part2 = null;
			}
		}

		#endregion

		#region Serializacion JSON

		/// <summary>Serializa y escribe el objeto indicado en una cadena JSON.
		/// <para>El objeto (Clase) debe tener un Constructor sin Parametros definido.</para>
		/// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
		/// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [JsonIgnore] attribute.</para>
		/// </summary>
		/// <typeparam name="T">Tipo de Objeto al cual queremos convertir.</typeparam>
		/// <param name="objectToWrite">Instancia del Objeto que se va a Serializar.</param>
		public static string Serialize_ToJSON<T>(T objectToWrite) where T : new()
		{
			/* EJEMPLO:  string _JsonString = Util.Serialize_ToJSON(_Inventario);  */
			string _ret = string.Empty;
			try
			{
				_ret = Newtonsoft.Json.JsonConvert.SerializeObject(objectToWrite);
			}
			catch { }
			return _ret;
		}

		public static T DeSerialize_FromJSON<T>(string filePath) where T : new()
		{
			/* EJEMPLO:  inventario _JSON = Util.DeSerialize_FromJSON_String<inventario>(Inventario_JSON);  */
			/* List<string> videogames = JsonConvert.DeserializeObject<List<string>>(json); */

			var _ret = default(T);
			try
			{
				if (!filePath.EmptyOrNull())
				{
					if (System.IO.File.Exists(filePath))
					{
						//Carga el JSON sin dejar el archivo 'en uso':
						using (TextReader reader = new StreamReader(filePath))
						{
							var fileContents = reader.ReadToEnd(); reader.Close();
							_ret = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(fileContents);
						}
					}
				}
			}
			finally { }
			return _ret;
		}

		/// <summary>Serializa y escribe el objeto indicado en un archivo JSON.
		/// <para>La Clase a Serializar DEBE tener un Constructor sin parametros.</para>
		/// <para>Only Public properties and variables will be written to the file. These can be any type, even other classes.</para>
		/// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [JsonIgnore] attribute.</para>
		/// </summary>
		/// <typeparam name="T">El tipo de Objeto a guardar en el Archivo.</typeparam>
		/// <param name="filePath">Ruta completa al archivo donde se guardará el JSON.</param>
		/// <param name="objectToWrite">Instancia del Objeto a Serializar</param>
		/// <param name="append">'false'=Sobre-Escribe el Archivo, 'true'=Añade datos al final del archivo.</param>
		public static string Serialize_ToJSON<T>(string filePath, T objectToWrite, bool append = false) where T : new()
		{
			/* EJEMPLO:  string _JsonString = Util.Serialize_ToJSON(System.IO.Path.Combine(file_path,_file_name), _Inventario);  */

			string _ret = string.Empty;
			try
			{
				if (!filePath.EmptyOrNull())
				{
					_ret = Newtonsoft.Json.JsonConvert.SerializeObject(objectToWrite);
					using (System.IO.TextWriter writer = new System.IO.StreamWriter(filePath, append))
					{
						writer.Write(_ret);
						writer.Close();
					};
				}
			}
			catch { }
			return _ret;
		}

		/// <summary>Crea una instancia de un Objeto leyendo sus datos desde una cadena JSON.
		/// <para>El objeto (Clase) debe tener un Constructor sin Parametros definido.</para></summary>
		/// <typeparam name="T">Tipo de Objeto al cual queremos convertir.</typeparam>
		/// <param name="JSONstring">Texto con formato JSON</param>
		public static T DeSerialize_FromJSON_String<T>(string JSONstring) where T : new()
		{
			/* EJEMPLO:  inventario _JSON = Util.DeSerialize_FromJSON_String<inventario>(Inventario_JSON);  */

			if (!JSONstring.EmptyOrNull())
			{
				return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(JSONstring);
			}
			else
			{
				return default(T); //<- Si me pasan un JSON vacio, les devuelvo un Objeto Vacio.
			}
		}

		#endregion

		#region TreeViewExtension Methods

		public static TreeNode FindTreeNodeByFullPath(this TreeNodeCollection collection, string fullPath, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
		{
			var foundNode = collection.Cast<TreeNode>().FirstOrDefault(tn => string.Equals(tn.FullPath, fullPath, comparison));
			if (null == foundNode)
			{
				foreach (var childNode in collection.Cast<TreeNode>())
				{
					var foundChildNode = FindTreeNodeByFullPath(childNode.Nodes, fullPath, comparison);
					if (null != foundChildNode)
					{
						return foundChildNode;
					}
				}
			}
			return foundNode;
		}
		public static IEnumerable<TreeNode> FlattenTree(this TreeView tv)
		{
			return FlattenTree(tv.Nodes);
		}
		public static IEnumerable<TreeNode> FlattenTree(this TreeNodeCollection coll)
		{
			return coll.Cast<TreeNode>()
						.Concat(coll.Cast<TreeNode>()
									.SelectMany(x => FlattenTree(x.Nodes)));
		}
		#endregion

		#region PropertyGrid Extension Methods

		public static IEnumerable<GridItem> EnumerateAllItems(this PropertyGrid grid)
		{
			if (grid == null)
				yield break;

			// get to root item
			GridItem start = grid.SelectedGridItem;
			while (start.Parent != null)
			{
				start = start.Parent;
			}

			foreach (GridItem item in start.EnumerateAllItems())
			{
				yield return item;
			}
		}

		public static IEnumerable<GridItem> EnumerateAllItems(this GridItem item)
		{
			if (item == null)
				yield break;

			yield return item;
			foreach (GridItem child in item.GridItems)
			{
				foreach (GridItem gc in child.EnumerateAllItems())
				{
					yield return gc;
				}
			}
		}

		#endregion
	}
	public static class XmlToDynamic
	{
		public static void Parse(dynamic parent, XElement node)
		{
			if (node.HasElements)
			{
				if (node.Elements(node.Elements().First().Name.LocalName).Count() > 1)
				{
					//list
					var item = new ExpandoObject();
					var list = new List<dynamic>();
					foreach (var element in node.Elements())
					{
						Parse(list, element);
					}

					AddProperty(item, node.Elements().First().Name.LocalName, list);
					AddProperty(parent, node.Name.ToString(), item);
				}
				else
				{
					var item = new ExpandoObject();

					foreach (var attribute in node.Attributes())
					{
						AddProperty(item, attribute.Name.ToString(), attribute.Value.Trim());
					}

					//element
					foreach (var element in node.Elements())
					{
						Parse(item, element);
					}

					AddProperty(parent, node.Name.ToString(), item);
				}
			}
			else
			{
				AddProperty(parent, node.Name.ToString(), node.Value.Trim());
			}
		}

		private static void AddProperty(dynamic parent, string name, object value)
		{
			if (parent is List<dynamic>)
			{
				(parent as List<dynamic>).Add(value);
			}
			else
			{
				(parent as IDictionary<String, object>)[name] = value;
			}
		}
	}

	public class ExpandoTypeDescriptor : ICustomTypeDescriptor
	{
		private readonly ExpandoObject _expando;

		public ExpandoTypeDescriptor(ExpandoObject expando)
		{
			_expando = expando;
		}

		// Just use the default behavior from TypeDescriptor for most of these
		// This might need some tweaking to work correctly for ExpandoObjects though...

		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		EventDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return _expando;
		}

		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		public PropertyDescriptor GetDefaultProperty()
		{
			return null;
		}

		// This is where the GetProperties() calls are
		// Ignore the Attribute for now, if it's needed support will have to be implemented
		// Should be enough for simple usage...

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(new Attribute[0]);
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			// This just casts the ExpandoObject to an IDictionary<string, object> to get the keys
			return new PropertyDescriptorCollection(
				((IDictionary<string, object>)_expando).Keys
				.Select(x => new ExpandoPropertyDescriptor(((IDictionary<string, object>)_expando), x))
				.ToArray());
		}

		// A nested PropertyDescriptor class that can get and set properties of the
		// ExpandoObject dynamically at run time
		private class ExpandoPropertyDescriptor : PropertyDescriptor
		{
			private readonly IDictionary<string, object> _expando;
			private readonly string _name;

			public ExpandoPropertyDescriptor(IDictionary<string, object> expando, string name)
				: base(name, null)
			{
				_expando = expando;
				_name = name;
			}

			public override Type PropertyType
			{
				get { return _expando[_name].GetType(); }
			}

			public override void SetValue(object component, object value)
			{
				_expando[_name] = value;
			}

			public override object GetValue(object component)
			{
				return _expando[_name];
			}

			public override bool IsReadOnly
			{
				get
				{
					// You might be able to implement some better logic here
					return false;
				}
			}

			public override Type ComponentType
			{
				get { return null; }
			}

			public override bool CanResetValue(object component)
			{
				return false;
			}

			public override void ResetValue(object component)
			{
			}

			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			public override string Category
			{
				get { return string.Empty; }
			}

			public override string Description
			{
				get { return string.Empty; }
			}
		}
	}
	public class ExpandoObjectTypeDescriptionProvider : TypeDescriptionProvider
	{
		private static readonly TypeDescriptionProvider m_Default = TypeDescriptor.GetProvider(typeof(ExpandoObject));

		public ExpandoObjectTypeDescriptionProvider()
			: base(m_Default)
		{
		}

		public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			var defaultDescriptor = base.GetTypeDescriptor(objectType, instance);

			return instance == null ? defaultDescriptor :
				   new ExpandoTypeDescriptor((ExpandoObject)instance);
		}
	}
}
