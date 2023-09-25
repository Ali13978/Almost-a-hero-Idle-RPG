using System;
using System.Collections;
using System.Collections.Generic;
using PlistCS;
using UnityEngine;

namespace UnityEditor.XCodeEditor
{
	public class XCPlist
	{
		public XCPlist(string plistPath)
		{
			this.plistPath = plistPath;
		}

		public void Process(Hashtable plist)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Plist.readPlist(this.plistPath);
			IDictionaryEnumerator enumerator = plist.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					this.AddPlistItems((string)dictionaryEntry.Key, dictionaryEntry.Value, dictionary);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			if (this.plistModified)
			{
				Plist.writeXml(dictionary, this.plistPath);
			}
		}

		public static Dictionary<K, V> HashtableToDictionary<K, V>(Hashtable table)
		{
			Dictionary<K, V> dictionary = new Dictionary<K, V>();
			IDictionaryEnumerator enumerator = table.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					dictionary.Add((K)((object)dictionaryEntry.Key), (V)((object)dictionaryEntry.Value));
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return dictionary;
		}

		public void AddPlistItems(string key, object value, Dictionary<string, object> dict)
		{
			UnityEngine.Debug.Log("AddPlistItems: key=" + key);
			if (key.CompareTo("urltype") == 0)
			{
				this.processUrlTypes((ArrayList)value, dict);
			}
			else
			{
				dict[key] = XCPlist.HashtableToDictionary<string, object>((Hashtable)value);
				this.plistModified = true;
			}
		}

		private void processUrlTypes(ArrayList urltypes, Dictionary<string, object> dict)
		{
			List<object> list;
			if (dict.ContainsKey("CFBundleURLTypes"))
			{
				list = (List<object>)dict["CFBundleURLTypes"];
			}
			else
			{
				list = new List<object>();
			}
			IEnumerator enumerator = urltypes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Hashtable hashtable = (Hashtable)obj;
					string value = (string)hashtable["role"];
					if (string.IsNullOrEmpty(value))
					{
						value = "Editor";
					}
					string text = (string)hashtable["name"];
					ArrayList arrayList = (ArrayList)hashtable["schemes"];
					List<object> list2 = new List<object>();
					IEnumerator enumerator2 = arrayList.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							string item = (string)obj2;
							list2.Add(item);
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = (enumerator2 as IDisposable)) != null)
						{
							disposable.Dispose();
						}
					}
					Dictionary<string, object> dictionary = this.findUrlTypeByName(list, text);
					if (dictionary == null)
					{
						dictionary = new Dictionary<string, object>();
						dictionary["CFBundleTypeRole"] = value;
						dictionary["CFBundleURLName"] = text;
						dictionary["CFBundleURLSchemes"] = list2;
						list.Add(dictionary);
					}
					else
					{
						dictionary["CFBundleTypeRole"] = value;
						dictionary["CFBundleURLSchemes"] = list2;
					}
					this.plistModified = true;
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = (enumerator as IDisposable)) != null)
				{
					disposable2.Dispose();
				}
			}
			dict["CFBundleURLTypes"] = list;
		}

		private Dictionary<string, object> findUrlTypeByName(List<object> bundleUrlTypes, string name)
		{
			if (bundleUrlTypes == null || bundleUrlTypes.Count == 0)
			{
				return null;
			}
			foreach (object obj in bundleUrlTypes)
			{
				Dictionary<string, object> dictionary = (Dictionary<string, object>)obj;
				string strA = (string)dictionary["CFBundleURLName"];
				if (string.Compare(strA, name) == 0)
				{
					return dictionary;
				}
			}
			return null;
		}

		private string plistPath;

		private bool plistModified;

		private const string BundleUrlTypes = "CFBundleURLTypes";

		private const string BundleTypeRole = "CFBundleTypeRole";

		private const string BundleUrlName = "CFBundleURLName";

		private const string BundleUrlSchemes = "CFBundleURLSchemes";

		private const string PlistUrlType = "urltype";

		private const string PlistRole = "role";

		private const string PlistEditor = "Editor";

		private const string PlistName = "name";

		private const string PlistSchemes = "schemes";
	}
}
