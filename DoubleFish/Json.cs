using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

using System.Data;
using System.Data.Linq.Mapping;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace DoubleFish
{
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class Json
	{
		/// <summary>
		/// 
		/// </summary>
		public static readonly Json Empty = new Json();
		
		/// <summary>
		/// Fields
		/// </summary>
		private Dictionary<object, Dictionary<string, object>> _EP;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public object this[object key, string name]
		{
			get
			{
				if (!object.ReferenceEquals(this, Empty))
				{
					if (key == null)
					{
						return null;
					}
					if (this._EP == null)
					{
						return null;
					}
					if (!this._EP.ContainsKey(key))
					{
						return null;
					}
					if (this._EP[key].ContainsKey(name))
					{
						return this._EP[key][name];
					}
				}
				return null;
			}
			set
			{
				if (object.ReferenceEquals(this, Empty))
				{
					throw new ArgumentOutOfRangeException();
				}
				if (key != null)
				{
					if (this._EP == null)
					{
						this._EP = new Dictionary<object, Dictionary<string, object>>();
					}
					if (!this._EP.ContainsKey(key))
					{
						this._EP[key] = new Dictionary<string, object>();
					}
					this._EP[key][name] = value;
				}
			}
		}

		/// <summary>
		/// Properties
		/// </summary>
		public string ClassName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		private object _Graph;
		/// <summary>
		/// 
		/// </summary>
		public object Graph
		{
			get
			{
				return this._Graph;
			}
			set
			{
				if (object.ReferenceEquals(this, Empty))
				{
					throw new ArgumentOutOfRangeException();
				}
				this._Graph = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IgnoreError { get; set; }

		// Methods
		public Json ()
		{
			this._Graph = null;
			this._EP = null;
		}

		public Json (object graph)
		{
			this._Graph = graph;
			this._EP = null;
		}

		public void AppendEP (object key, string name, object value)
		{
			if (object.ReferenceEquals(this, Empty))
			{
				throw new ArgumentOutOfRangeException();
			}
			if (key != null)
			{
				if (this._EP == null)
				{
					this._EP = new Dictionary<object, Dictionary<string, object>>();
				}
				if (!this._EP.ContainsKey(key))
				{
					this._EP[key] = new Dictionary<string, object>();
				}
				this._EP[key][name] = value;
			}
		}

		private object BaseGet (string name)
		{
			if (this._Graph == null)
			{
				return null;
			}
			Type type = this._Graph.GetType();
			MethodBase currentMethod = MethodBase.GetCurrentMethod();
			ParameterInfo[] parameters = currentMethod.GetParameters();
			Type[] types = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				types[i] = parameters[i].ParameterType;
			}
			currentMethod = type.GetMethod(currentMethod.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, types, null);
			if (currentMethod == null)
			{
				return null;
			}
			return currentMethod.Invoke(this._Graph, new object[] { name });
		}

		public static string JsEncode (string input)
		{
			return JsEncode(input, false);
		}

		public static string JsEncode (string input, bool invertedComma)
		{
			if (invertedComma)
			{
				return JsEncode(input, "'");
			}
			return JsEncode(input, (string)null);
		}

		public static string JsEncode (string input, char stringDivide)
		{
			return JsEncode(input, stringDivide + string.Empty);
		}

		public static string JsEncode (string input, string stringDivide)
		{
			if (string.IsNullOrEmpty(input))
				return input;

			if (string.IsNullOrEmpty(stringDivide))
				stringDivide = "\"";

			input = input.Replace(@"\", @"\\");
			input = input.Replace("\r", @"\r");
			input = input.Replace("\n", @"\n");
			input = input.Replace("\t", @"\t");
			input = input.Replace(stringDivide, @"\" + stringDivide);

			return input;
		}

		public void RemoveEP ()
		{
			this.RemoveEP(null, null);
		}

		public void RemoveEP (object key)
		{
			this.RemoveEP(key, null);
		}

		public void RemoveEP (object key, string name)
		{
			if (this._EP == null)
			{
				return;
			}
			if (key == null)
			{
				this._EP = null;
			}
			else if (this._EP.ContainsKey(key))
			{
				if (name == null)
				{
					this._EP.Remove(key);
				}
				else if (this._EP[key].ContainsKey(name))
				{
					this._EP[key].Remove(name);
				}
			}
		}

		public string ToJsonString ()
		{
			return ToJsonString(this._Graph, this, BindingFlags.Public | BindingFlags.Instance, null);
		}

		public static string ToJsonString (object graph)
		{
			return ToJsonString(graph, null, BindingFlags.Public | BindingFlags.Instance, null);
		}

		public static string ToJsonString (object graph, Json json)
		{
			return ToJsonString(graph, json, BindingFlags.Public | BindingFlags.Instance, null);
		}

		protected static string ToJsonString (object graph, Json json, BindingFlags bindingFlags, StringBuilder builder)
		{
			#region (graph == null))
			if ((graph == null))
			{
				return "null";
			}
			#endregion

			#region graph is bool
			if (graph is bool)
			{
				if (graph.Equals(true))
				{
					return "true";
				}
				else
				{
					return "false";
				}
			}
			#endregion

			#region graph is sbyte || graph is byte || graph is short || graph is ushort || graph is int || graph is uint || graph is long || graph is ulong || graph is float || graph is double || graph is decimal
			if (graph is sbyte || graph is byte || graph is short || graph is ushort || graph is int || graph is uint || graph is long || graph is ulong || graph is float || graph is double || graph is decimal)
			{
				return string.Concat(new object[] { "\"", graph, "\"" });
			}
			#endregion

			#region graph is char || graph is string || graph is IntPtr || graph is Guid || graph is TimeSpan || graph is StringBuilder || graph is IPAddress
			if (graph is char || graph is string || graph is IntPtr || graph is Guid || graph is TimeSpan || graph is StringBuilder || graph is IPAddress)
			{
				return "\"" + JsEncode(graph + string.Empty) + "\"";
			}
			#endregion

			#region graph is DateTime
			if (graph is DateTime)
			{
				DateTime time = (DateTime)graph;
				return "\"" + time.ToString("yyyy-MM-dd HH:mm:ss") + "\"";
			}
			#endregion

			if (builder == null)
				builder = new StringBuilder();

			IEnumerable enumerable = graph as IEnumerable;
			if (enumerable != null)
			{
				builder.Append(ToJsonString(enumerable, null));
				return builder.ToString();
			}

			DataTable table = graph as DataTable;
			if (table != null)
			{
				builder.Append(ToJsonString(table, null));
				return builder.ToString();
			}

			int length = builder.Length;

			Type type = graph.GetType();

			if (json != null && json.ClassName != null)
			{
				builder.Append("{ \"type\": \"" + JsEncode(json.ClassName) + "\", ");
			}
			else
			{
				builder.Append("{ \"type\": \"" + JsEncode(type.FullName) + "\", ");
			}

			if (json != null && json._EP != null)
			{
				builder.Append(ToJsonString(json._EP, null));
			}

			PropertyInfo[] properties = graph.GetType().GetProperties(bindingFlags);
			if (properties != null && properties.Length > 0)
			{
				builder.Append(ToJsonString(properties, null, graph));
			}
			else
			{
				builder.Remove(builder.Length - 2, 2);
			}
			builder.Append(" }");
			return builder.ToString();
		}

		private static StringBuilder ToJsonString (PropertyInfo[] properties, StringBuilder builder, object graph)
		{
			if (builder == null)
				builder = new StringBuilder();

			foreach (PropertyInfo info in properties)
			{
				if (info.GetIndexParameters().Length > 0 || info.GetGetMethod() == null || info.GetCustomAttributes(typeof(JsonAttribute), true).Length > 0 || info.GetCustomAttributes(typeof(XmlIgnoreAttribute), true).Length > 0 || info.GetCustomAttributes(typeof(AssociationAttribute), true).Length > 0)
					continue;

				builder.Append("\"" + JsEncode(info.Name) + "\": " + ToJsonString(info.GetValue(graph, null)) + ", ");
			}
			if (properties.Length > 0)
			{
				builder.Remove(builder.Length - 2, 2);
			}
			return builder;
		}

		private static StringBuilder ToJsonString (XmlNode key, StringBuilder builder)
		{
			if (builder == null)
				builder = new StringBuilder();

			builder.Append("[ ");
			foreach (XmlAttribute attribute in key.Attributes)
			{
				builder.Append("\"@" + JsEncode(attribute.Prefix + ": " + attribute.Name) + "\": \"" + JsEncode(attribute.Value) + "\", ");
			}
			if (builder.Length > 2)
			{
				builder.Remove(builder.Length - 2, 2);
			}
			builder.Append(" ]");
			return builder;
		}

		private static StringBuilder ToJsonString (NameObjectCollectionBase collection, Json json, StringBuilder builder)
		{
			if (builder == null)
				builder = new StringBuilder();

			builder.Append("[");
			foreach (object obj in collection.Keys)
			{
				builder.Append(ToJsonString(obj) + ": " + ToJsonString(json.BaseGet(obj + string.Empty)) + ", ");
			}
			if (builder.Length > 2)
			{
				builder.Remove(builder.Length - 2, 2);
			}
			builder.Append(" ]");
			return builder;
		}

		private static StringBuilder ToJsonString (IDictionary dictionary, StringBuilder builder)
		{
			if (builder == null)
				builder = new StringBuilder();

			//builder.Append("[ ");
			foreach (object key in dictionary.Keys)
			{
				builder.Append(ToJsonString(key) + ": " + ToJsonString(dictionary[key]) + ", ");
			}
			if (builder.Length > 2)
			{
				builder.Remove(builder.Length - 2, 2);
			}
			//builder.Append(" ]");
			return builder;
		}

		private static StringBuilder ToJsonString (DataTable dt, StringBuilder builder)
		{
			if (builder == null)
				builder = new StringBuilder();

			builder.Append("[ ");
			for (int i = 0; i < dt.Rows.Count; i++ )
			{
				builder.Append("{ ");
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					builder.Append("\"" + JsEncode(dt.Columns[j].ColumnName) + "\": " + ToJsonString(dt.Rows[i][j]) + ", ");
				}
				if (dt.Columns.Count > 0)
				{
					builder.Remove(builder.Length - 2, 2);
				}
				builder.Append(" }, ");
			}
			if (builder.Length > 2)
			{
				builder.Remove(builder.Length - 2, 2);
			}
			builder.Append(" ]");
			return builder;
		}

		private static StringBuilder ToJsonString (IDataReader dataReader, StringBuilder builder)
		{
			if (builder == null)
				builder = new StringBuilder();

			builder.Append("[ ");
			while (dataReader.Read())
			{
				builder.Append("{ ");
				for (int i = 0; i < dataReader.FieldCount; i++)
				{
					builder.Append("\"" + JsEncode(dataReader.GetName(i)) + "\":" + ToJsonString(dataReader[i]) + ", ");
				}
				if (dataReader.FieldCount > 0)
				{
					builder.Remove(builder.Length - 2, 2);
				}
				builder.Append(" }, ");
			}
			if (builder.Length > 2)
			{
				builder.Remove(builder.Length - 2, 2);
			}
			builder.Append(" ]");
			return builder;
		}

		private static StringBuilder ToJsonString (IEnumerable array, StringBuilder builder)
		{
			if (builder == null)
				builder = new StringBuilder();

			builder.Append("[ ");
			foreach (object obj in array)
			{
				builder.Append(ToJsonString(obj) + ", ");
			}
			if (builder.Length > 2)
			{
				builder.Remove(builder.Length - 2, 2);
			}
			builder.Append(" ]");
			return builder;
		}
	}
}


