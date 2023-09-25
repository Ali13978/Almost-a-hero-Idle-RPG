using System;
using System.Collections;
using UnityEngine;

namespace UnityEditor.XCodeEditor
{
	public class XCBuildConfiguration : PBXObject
	{
		public XCBuildConfiguration(string guid, PBXDictionary dictionary) : base(guid, dictionary)
		{
		}

		public PBXSortedDictionary buildSettings
		{
			get
			{
				if (!base.ContainsKey("buildSettings"))
				{
					return null;
				}
				if (this._data["buildSettings"].GetType() == typeof(PBXDictionary))
				{
					PBXSortedDictionary pbxsortedDictionary = new PBXSortedDictionary();
					pbxsortedDictionary.Append((PBXDictionary)this._data["buildSettings"]);
					return pbxsortedDictionary;
				}
				return (PBXSortedDictionary)this._data["buildSettings"];
			}
		}

		protected bool AddSearchPaths(string path, string key, bool recursive = true)
		{
			return this.AddSearchPaths(new PBXList
			{
				path
			}, key, recursive, false);
		}

		protected bool AddSearchPaths(PBXList paths, string key, bool recursive = true, bool quoted = false)
		{
			bool result = false;
			if (!base.ContainsKey("buildSettings"))
			{
				base.Add("buildSettings", new PBXSortedDictionary());
			}
			IEnumerator enumerator = paths.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					string text = (string)obj;
					string text2 = text;
					if (!((PBXDictionary)this._data["buildSettings"]).ContainsKey(key))
					{
						((PBXDictionary)this._data["buildSettings"]).Add(key, new PBXList());
					}
					else if (((PBXDictionary)this._data["buildSettings"])[key] is string)
					{
						PBXList pbxlist = new PBXList();
						pbxlist.Add(((PBXDictionary)this._data["buildSettings"])[key]);
						((PBXDictionary)this._data["buildSettings"])[key] = pbxlist;
					}
					if (text2.Contains(" "))
					{
						quoted = true;
					}
					if (quoted)
					{
						if (text2.EndsWith("/**"))
						{
							text2 = "\\\"" + text2.Replace("/**", "\\\"/**");
						}
						else
						{
							text2 = "\\\"" + text2 + "\\\"";
						}
					}
					if (!((PBXList)((PBXDictionary)this._data["buildSettings"])[key]).Contains(text2))
					{
						((PBXList)((PBXDictionary)this._data["buildSettings"])[key]).Add(text2);
						result = true;
					}
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
			return result;
		}

		public bool AddHeaderSearchPaths(PBXList paths, bool recursive = true)
		{
			return this.AddSearchPaths(paths, "HEADER_SEARCH_PATHS", recursive, false);
		}

		public bool AddLibrarySearchPaths(PBXList paths, bool recursive = true)
		{
			UnityEngine.Debug.Log("AddLibrarySearchPaths " + paths);
			return this.AddSearchPaths(paths, "LIBRARY_SEARCH_PATHS", recursive, false);
		}

		public bool AddFrameworkSearchPaths(PBXList paths, bool recursive = true)
		{
			return this.AddSearchPaths(paths, "FRAMEWORK_SEARCH_PATHS", recursive, false);
		}

		public bool AddOtherCFlags(string flag)
		{
			return this.AddOtherCFlags(new PBXList
			{
				flag
			});
		}

		public bool AddOtherCFlags(PBXList flags)
		{
			bool result = false;
			if (!base.ContainsKey("buildSettings"))
			{
				base.Add("buildSettings", new PBXSortedDictionary());
			}
			IEnumerator enumerator = flags.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					string text = (string)obj;
					if (!((PBXDictionary)this._data["buildSettings"]).ContainsKey("OTHER_CFLAGS"))
					{
						((PBXDictionary)this._data["buildSettings"]).Add("OTHER_CFLAGS", new PBXList());
					}
					else if (((PBXDictionary)this._data["buildSettings"])["OTHER_CFLAGS"] is string)
					{
						string value = (string)((PBXDictionary)this._data["buildSettings"])["OTHER_CFLAGS"];
						((PBXDictionary)this._data["buildSettings"])["OTHER_CFLAGS"] = new PBXList();
						((PBXList)((PBXDictionary)this._data["buildSettings"])["OTHER_CFLAGS"]).Add(value);
					}
					if (!((PBXList)((PBXDictionary)this._data["buildSettings"])["OTHER_CFLAGS"]).Contains(text))
					{
						((PBXList)((PBXDictionary)this._data["buildSettings"])["OTHER_CFLAGS"]).Add(text);
						result = true;
					}
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
			return result;
		}

		public bool AddOtherLinkerFlags(string flag)
		{
			return this.AddOtherLinkerFlags(new PBXList
			{
				flag
			});
		}

		public bool AddOtherLinkerFlags(PBXList flags)
		{
			bool result = false;
			if (!base.ContainsKey("buildSettings"))
			{
				base.Add("buildSettings", new PBXSortedDictionary());
			}
			IEnumerator enumerator = flags.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					string text = (string)obj;
					if (!((PBXDictionary)this._data["buildSettings"]).ContainsKey("OTHER_LDFLAGS"))
					{
						((PBXDictionary)this._data["buildSettings"]).Add("OTHER_LDFLAGS", new PBXList());
					}
					else if (((PBXDictionary)this._data["buildSettings"])["OTHER_LDFLAGS"] is string)
					{
						string value = (string)((PBXDictionary)this._data["buildSettings"])["OTHER_LDFLAGS"];
						((PBXDictionary)this._data["buildSettings"])["OTHER_LDFLAGS"] = new PBXList();
						if (!string.IsNullOrEmpty(value))
						{
							((PBXList)((PBXDictionary)this._data["buildSettings"])["OTHER_LDFLAGS"]).Add(value);
						}
					}
					if (!((PBXList)((PBXDictionary)this._data["buildSettings"])["OTHER_LDFLAGS"]).Contains(text))
					{
						((PBXList)((PBXDictionary)this._data["buildSettings"])["OTHER_LDFLAGS"]).Add(text);
						result = true;
					}
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
			return result;
		}

		public bool overwriteBuildSetting(string settingName, string settingValue)
		{
			UnityEngine.Debug.Log("overwriteBuildSetting " + settingName + " " + settingValue);
			bool result = false;
			if (!base.ContainsKey("buildSettings"))
			{
				UnityEngine.Debug.Log("creating key buildSettings");
				base.Add("buildSettings", new PBXSortedDictionary());
			}
			if (!((PBXDictionary)this._data["buildSettings"]).ContainsKey(settingName))
			{
				UnityEngine.Debug.Log("adding key " + settingName);
				((PBXDictionary)this._data["buildSettings"]).Add(settingName, new PBXList());
			}
			else if (((PBXDictionary)this._data["buildSettings"])[settingName] is string)
			{
				((PBXDictionary)this._data["buildSettings"])[settingName] = new PBXList();
			}
			if (!((PBXList)((PBXDictionary)this._data["buildSettings"])[settingName]).Contains(settingValue))
			{
				UnityEngine.Debug.Log("setting " + settingName + " to " + settingValue);
				((PBXList)((PBXDictionary)this._data["buildSettings"])[settingName]).Add(settingValue);
				result = true;
			}
			return result;
		}

		protected const string BUILDSETTINGS_KEY = "buildSettings";

		protected const string HEADER_SEARCH_PATHS_KEY = "HEADER_SEARCH_PATHS";

		protected const string LIBRARY_SEARCH_PATHS_KEY = "LIBRARY_SEARCH_PATHS";

		protected const string FRAMEWORK_SEARCH_PATHS_KEY = "FRAMEWORK_SEARCH_PATHS";

		protected const string OTHER_C_FLAGS_KEY = "OTHER_CFLAGS";

		protected const string OTHER_LDFLAGS_KEY = "OTHER_LDFLAGS";
	}
}
