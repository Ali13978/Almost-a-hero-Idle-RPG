using System;

namespace UnityEditor.XCodeEditor
{
	public class PBXBuildFile : PBXObject
	{
		public PBXBuildFile(PBXFileReference fileRef, bool weak = false)
		{
			base.Add("fileRef", fileRef.guid);
			this.SetWeakLink(weak);
			if (!string.IsNullOrEmpty(fileRef.compilerFlags))
			{
				foreach (string flag in fileRef.compilerFlags.Split(new char[]
				{
					','
				}))
				{
					this.AddCompilerFlag(flag);
				}
			}
		}

		public PBXBuildFile(string guid, PBXDictionary dictionary) : base(guid, dictionary)
		{
		}

		public string fileRef
		{
			get
			{
				return (string)this._data["fileRef"];
			}
		}

		public bool SetWeakLink(bool weak = false)
		{
			PBXDictionary pbxdictionary;
			if (!this._data.ContainsKey("settings"))
			{
				if (weak)
				{
					PBXList pbxlist = new PBXList();
					pbxlist.Add("Weak");
					pbxdictionary = new PBXDictionary();
					pbxdictionary.Add("ATTRIBUTES", pbxlist);
					this._data.Add("settings", pbxdictionary);
				}
				return true;
			}
			pbxdictionary = (this._data["settings"] as PBXDictionary);
			if (pbxdictionary.ContainsKey("ATTRIBUTES"))
			{
				PBXList pbxlist = pbxdictionary["ATTRIBUTES"] as PBXList;
				if (weak)
				{
					pbxlist.Add("Weak");
				}
				else
				{
					pbxlist.Remove("Weak");
				}
				pbxdictionary.Add("ATTRIBUTES", pbxlist);
				base.Add("settings", pbxdictionary);
				return true;
			}
			if (weak)
			{
				pbxdictionary.Add("ATTRIBUTES", new PBXList
				{
					"Weak"
				});
				return true;
			}
			return false;
		}

		public bool AddCodeSignOnCopy()
		{
			if (!this._data.ContainsKey("settings"))
			{
				this._data["settings"] = new PBXDictionary();
			}
			PBXDictionary pbxdictionary = this._data["settings"] as PBXDictionary;
			if (!pbxdictionary.ContainsKey("ATTRIBUTES"))
			{
				pbxdictionary.Add("ATTRIBUTES", new PBXList
				{
					"CodeSignOnCopy",
					"RemoveHeadersOnCopy"
				});
			}
			else
			{
				PBXList pbxlist = pbxdictionary["ATTRIBUTES"] as PBXList;
				pbxlist.Add("CodeSignOnCopy");
				pbxlist.Add("RemoveHeadersOnCopy");
			}
			return true;
		}

		public bool AddCompilerFlag(string flag)
		{
			if (!this._data.ContainsKey("settings"))
			{
				this._data["settings"] = new PBXDictionary();
			}
			if (!((PBXDictionary)this._data["settings"]).ContainsKey("COMPILER_FLAGS"))
			{
				((PBXDictionary)this._data["settings"]).Add("COMPILER_FLAGS", flag);
				return true;
			}
			string[] array = ((string)((PBXDictionary)this._data["settings"])["COMPILER_FLAGS"]).Split(new char[]
			{
				' '
			});
			foreach (string text in array)
			{
				if (text.CompareTo(flag) == 0)
				{
					return false;
				}
			}
			((PBXDictionary)this._data["settings"])["COMPILER_FLAGS"] = string.Join(" ", array) + " " + flag;
			return true;
		}

		private const string FILE_REF_KEY = "fileRef";

		private const string SETTINGS_KEY = "settings";

		private const string ATTRIBUTES_KEY = "ATTRIBUTES";

		private const string WEAK_VALUE = "Weak";

		private const string COMPILER_FLAGS_KEY = "COMPILER_FLAGS";
	}
}
