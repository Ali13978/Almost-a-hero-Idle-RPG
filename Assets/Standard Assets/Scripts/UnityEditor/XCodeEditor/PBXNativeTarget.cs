using System;

namespace UnityEditor.XCodeEditor
{
	public class PBXNativeTarget : PBXObject
	{
		public PBXNativeTarget()
		{
		}

		public PBXNativeTarget(string guid, PBXDictionary dictionary) : base(guid, dictionary)
		{
		}
	}
}
