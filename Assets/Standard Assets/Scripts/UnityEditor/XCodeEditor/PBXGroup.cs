using System;

namespace UnityEditor.XCodeEditor
{
	public class PBXGroup : PBXObject
	{
		public PBXGroup(string name, string path = null, string tree = "SOURCE_ROOT")
		{
			base.Add("children", new PBXList());
			base.Add("name", name);
			if (path != null)
			{
				base.Add("path", path);
				base.Add("sourceTree", tree);
			}
			else
			{
				base.Add("sourceTree", "<group>");
			}
		}

		public PBXGroup(string guid, PBXDictionary dictionary) : base(guid, dictionary)
		{
		}

		public PBXList children
		{
			get
			{
				if (!base.ContainsKey("children"))
				{
					base.Add("children", new PBXList());
				}
				return (PBXList)this._data["children"];
			}
		}

		public string name
		{
			get
			{
				if (!base.ContainsKey("name"))
				{
					return null;
				}
				return (string)this._data["name"];
			}
		}

		public string path
		{
			get
			{
				if (!base.ContainsKey("path"))
				{
					return null;
				}
				return (string)this._data["path"];
			}
		}

		public string sourceTree
		{
			get
			{
				return (string)this._data["sourceTree"];
			}
		}

		public string AddChild(PBXObject child)
		{
			if (child is PBXFileReference || child is PBXGroup)
			{
				this.children.Add(child.guid);
				return child.guid;
			}
			return null;
		}

		public void RemoveChild(string id)
		{
			if (!PBXObject.IsGuid(id))
			{
				return;
			}
			this.children.Remove(id);
		}

		public bool HasChild(string id)
		{
			if (!base.ContainsKey("children"))
			{
				base.Add("children", new PBXList());
				return false;
			}
			return PBXObject.IsGuid(id) && ((PBXList)this._data["children"]).Contains(id);
		}

		public string GetName()
		{
			return (string)this._data["name"];
		}

		protected const string NAME_KEY = "name";

		protected const string CHILDREN_KEY = "children";

		protected const string PATH_KEY = "path";

		protected const string SOURCETREE_KEY = "sourceTree";
	}
}
