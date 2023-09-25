using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace UnityEditor.XCodeEditor
{
	public class PBXObject
	{
		public PBXObject()
		{
			this._data = new PBXDictionary();
			this._data["isa"] = base.GetType().Name;
			this._guid = PBXObject.GenerateGuid();
		}

		public PBXObject(string guid) : this()
		{
			if (PBXObject.IsGuid(guid))
			{
				this._guid = guid;
			}
		}

		public PBXObject(string guid, PBXDictionary dictionary) : this(guid)
		{
			if (!dictionary.ContainsKey("isa") || ((string)dictionary["isa"]).CompareTo(base.GetType().Name) != 0)
			{
				UnityEngine.Debug.LogError("PBXDictionary is not a valid ISA object");
			}
			foreach (KeyValuePair<string, object> keyValuePair in dictionary)
			{
				this._data[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		public string guid
		{
			get
			{
				if (string.IsNullOrEmpty(this._guid))
				{
					this._guid = PBXObject.GenerateGuid();
				}
				return this._guid;
			}
		}

		public PBXDictionary data
		{
			get
			{
				if (this._data == null)
				{
					this._data = new PBXDictionary();
				}
				return this._data;
			}
		}

		public static bool IsGuid(string aString)
		{
			return Regex.IsMatch(aString, "^[A-Fa-f0-9]{24}$");
		}

		public static string GenerateGuid()
		{
			return Guid.NewGuid().ToString("N").Substring(8).ToUpper();
		}

		public void Add(string key, object obj)
		{
			this._data.Add(key, obj);
		}

		public bool Remove(string key)
		{
			return this._data.Remove(key);
		}

		public bool ContainsKey(string key)
		{
			return this._data.ContainsKey(key);
		}

		public static implicit operator bool(PBXObject x)
		{
			return x != null && x.data.Count == 0;
		}

		public string ToCSV()
		{
			return "\"" + this.data + "\", ";
		}

		public override string ToString()
		{
			return "{" + this.ToCSV() + "} ";
		}

		protected const string ISA_KEY = "isa";

		protected string _guid;

		protected PBXDictionary _data;
	}
}
