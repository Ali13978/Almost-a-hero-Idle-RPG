using System;
using System.Collections.Generic;

namespace UnityEditor.XCodeEditor
{
	public class PBXDictionary : Dictionary<string, object>
	{
		public void Append(PBXDictionary dictionary)
		{
			foreach (KeyValuePair<string, object> keyValuePair in dictionary)
			{
				base.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		public void Append<T>(PBXDictionary<T> dictionary) where T : PBXObject
		{
			foreach (KeyValuePair<string, T> keyValuePair in dictionary)
			{
				base.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		public void Append(PBXSortedDictionary dictionary)
		{
			foreach (KeyValuePair<string, object> keyValuePair in dictionary)
			{
				base.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		public void Append<T>(PBXSortedDictionary<T> dictionary) where T : PBXObject
		{
			foreach (KeyValuePair<string, T> keyValuePair in dictionary)
			{
				base.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		public static implicit operator bool(PBXDictionary x)
		{
			return x != null && x.Count == 0;
		}

		public string ToCSV()
		{
			string text = string.Empty;
			foreach (KeyValuePair<string, object> keyValuePair in this)
			{
				text += "<";
				text += keyValuePair.Key;
				text += ", ";
				text += keyValuePair.Value;
				text += ">, ";
			}
			return text;
		}

		public override string ToString()
		{
			return "{" + this.ToCSV() + "}";
		}
	}
}
