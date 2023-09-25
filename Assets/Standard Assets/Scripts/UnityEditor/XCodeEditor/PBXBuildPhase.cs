using System;

namespace UnityEditor.XCodeEditor
{
	public class PBXBuildPhase : PBXObject
	{
		public PBXBuildPhase()
		{
		}

		public PBXBuildPhase(string guid, PBXDictionary dictionary) : base(guid, dictionary)
		{
		}

		public bool AddBuildFile(PBXBuildFile file)
		{
			if (!base.ContainsKey("files"))
			{
				base.Add("files", new PBXList());
			}
			((PBXList)this._data["files"]).Add(file.guid);
			return true;
		}

		public void RemoveBuildFile(string id)
		{
			if (!base.ContainsKey("files"))
			{
				base.Add("files", new PBXList());
				return;
			}
			((PBXList)this._data["files"]).Remove(id);
		}

		public bool HasBuildFile(string id)
		{
			if (!base.ContainsKey("files"))
			{
				base.Add("files", new PBXList());
				return false;
			}
			return PBXObject.IsGuid(id) && ((PBXList)this._data["files"]).Contains(id);
		}

		public PBXList files
		{
			get
			{
				if (!base.ContainsKey("files"))
				{
					base.Add("files", new PBXList());
				}
				return (PBXList)this._data["files"];
			}
		}

		protected const string FILES_KEY = "files";
	}
}
