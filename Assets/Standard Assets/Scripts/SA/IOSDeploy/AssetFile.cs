using System;
using System.IO;
using UnityEngine;

namespace SA.IOSDeploy
{
	[Serializable]
	public class AssetFile
	{
		public string FileName
		{
			get
			{
				return string.Empty;
			}
		}

		public string FilePath
		{
			get
			{
				return string.Empty;
			}
		}

		public string XCodeRelativePath
		{
			get
			{
				return this.XCodePath + this.FileName;
			}
		}

		public bool IsDirectory
		{
			get
			{
				FileAttributes attributes = File.GetAttributes(this.FilePath);
				return (attributes & FileAttributes.Directory) == FileAttributes.Directory;
			}
		}

		public bool IsOpen;

		public string XCodePath = string.Empty;

		public UnityEngine.Object Asset;
	}
}
