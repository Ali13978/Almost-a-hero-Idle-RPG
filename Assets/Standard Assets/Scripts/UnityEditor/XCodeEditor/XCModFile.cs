using System;

namespace UnityEditor.XCodeEditor
{
	public class XCModFile
	{
		public XCModFile(string inputString)
		{
			this.isWeak = false;
			if (inputString.Contains(":"))
			{
				string[] array = inputString.Split(new char[]
				{
					':'
				});
				this.filePath = array[0];
				this.isWeak = (array[1].CompareTo("weak") == 0);
			}
			else
			{
				this.filePath = inputString;
			}
		}

		public string filePath { get; private set; }

		public bool isWeak { get; private set; }
	}
}
