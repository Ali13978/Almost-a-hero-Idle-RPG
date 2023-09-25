using System;

namespace UnityEditor.XCodeEditor
{
	public class PBXVariantGroup : PBXGroup
	{
		public PBXVariantGroup(string name, string path = null, string tree = "SOURCE_ROOT") : base(name, path, tree)
		{
		}

		public PBXVariantGroup(string guid, PBXDictionary dictionary) : base(guid, dictionary)
		{
		}
	}
}
