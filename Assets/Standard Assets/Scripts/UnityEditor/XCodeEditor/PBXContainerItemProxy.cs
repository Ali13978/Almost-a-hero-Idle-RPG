using System;

namespace UnityEditor.XCodeEditor
{
	public class PBXContainerItemProxy : PBXObject
	{
		public PBXContainerItemProxy()
		{
		}

		public PBXContainerItemProxy(string guid, PBXDictionary dictionary) : base(guid, dictionary)
		{
		}
	}
}
