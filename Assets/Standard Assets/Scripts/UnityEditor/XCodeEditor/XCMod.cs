using System;
using System.Collections;
using System.IO;
using UnityEngine;
using XUPorterJSON;

namespace UnityEditor.XCodeEditor
{
	public class XCMod
	{
		public XCMod(string filename)
		{
			FileInfo fileInfo = new FileInfo(filename);
			if (!fileInfo.Exists)
			{
				UnityEngine.Debug.LogWarning("File does not exist.");
			}
			this.name = Path.GetFileNameWithoutExtension(filename);
			this.path = Path.GetDirectoryName(filename);
			string text = fileInfo.OpenText().ReadToEnd();
			UnityEngine.Debug.Log(text);
			this._datastore = (Hashtable)MiniJSON.jsonDecode(text);
			if (this._datastore == null || this._datastore.Count == 0)
			{
				UnityEngine.Debug.Log(text);
				throw new UnityException("Parse error in file " + Path.GetFileName(filename) + "! Check for typos such as unbalanced quotation marks, etc.");
			}
		}

		public string name { get; private set; }

		public string path { get; private set; }

		public string group
		{
			get
			{
				if (this._datastore != null && this._datastore.Contains("group"))
				{
					return (string)this._datastore["group"];
				}
				return string.Empty;
			}
		}

		public ArrayList patches
		{
			get
			{
				return (ArrayList)this._datastore["patches"];
			}
		}

		public ArrayList libs
		{
			get
			{
				if (this._libs == null)
				{
					this._libs = new ArrayList(((ArrayList)this._datastore["libs"]).Count);
					IEnumerator enumerator = ((ArrayList)this._datastore["libs"]).GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							string text = (string)obj;
							UnityEngine.Debug.Log("Adding to Libs: " + text);
							this._libs.Add(new XCModFile(text));
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
				}
				return this._libs;
			}
		}

		public ArrayList frameworks
		{
			get
			{
				return (ArrayList)this._datastore["frameworks"];
			}
		}

		public ArrayList headerpaths
		{
			get
			{
				return (ArrayList)this._datastore["headerpaths"];
			}
		}

		public ArrayList files
		{
			get
			{
				return (ArrayList)this._datastore["files"];
			}
		}

		public ArrayList folders
		{
			get
			{
				return (ArrayList)this._datastore["folders"];
			}
		}

		public ArrayList excludes
		{
			get
			{
				return (ArrayList)this._datastore["excludes"];
			}
		}

		public ArrayList compiler_flags
		{
			get
			{
				return (ArrayList)this._datastore["compiler_flags"];
			}
		}

		public ArrayList linker_flags
		{
			get
			{
				return (ArrayList)this._datastore["linker_flags"];
			}
		}

		public ArrayList embed_binaries
		{
			get
			{
				return (ArrayList)this._datastore["embed_binaries"];
			}
		}

		public Hashtable plist
		{
			get
			{
				return (Hashtable)this._datastore["plist"];
			}
		}

		private Hashtable _datastore = new Hashtable();

		private ArrayList _libs;
	}
}
