using System;

namespace UnityEditor.XCodeEditor
{
	public class PBXCopyFilesBuildPhase : PBXBuildPhase
	{
		public PBXCopyFilesBuildPhase(int buildActionMask)
		{
			base.Add("buildActionMask", buildActionMask);
			base.Add("dstPath", string.Empty);
			base.Add("dstSubfolderSpec", 10);
			base.Add("name", "Embed Frameworks");
			base.Add("runOnlyForDeploymentPostprocessing", 0);
		}

		public PBXCopyFilesBuildPhase(string guid, PBXDictionary dictionary) : base(guid, dictionary)
		{
		}
	}
}
