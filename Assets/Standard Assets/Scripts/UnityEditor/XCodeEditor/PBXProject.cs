using System;

namespace UnityEditor.XCodeEditor
{
	public class PBXProject : PBXObject
	{
		public PBXProject()
		{
		}

		public PBXProject(string guid, PBXDictionary dictionary) : base(guid, dictionary)
		{
		}

		public string mainGroupID
		{
			get
			{
				return (string)this._data[this.MAINGROUP_KEY];
			}
		}

		public PBXList knownRegions
		{
			get
			{
				return (PBXList)this._data[this.KNOWN_REGIONS_KEY];
			}
		}

		public void AddRegion(string region)
		{
			if (!this._clearedLoc)
			{
				this.knownRegions.Clear();
				this._clearedLoc = true;
			}
			this.knownRegions.Add(region);
		}

		protected string MAINGROUP_KEY = "mainGroup";

		protected string KNOWN_REGIONS_KEY = "knownRegions";

		protected bool _clearedLoc;
	}
}
