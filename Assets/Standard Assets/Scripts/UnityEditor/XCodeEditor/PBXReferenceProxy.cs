using System;

namespace UnityEditor.XCodeEditor
{
	public class PBXReferenceProxy : PBXObject
	{
		public PBXReferenceProxy()
		{
		}

		public PBXReferenceProxy(string guid, PBXDictionary dictionary) : base(guid, dictionary)
		{
		}
	}
}
