using System;

namespace DG.Tweening.Plugins.Options
{
	public struct StringOptions : IPlugOptions
	{
		public void Reset()
		{
			this.richTextEnabled = false;
			this.scrambleMode = ScrambleMode.None;
			this.scrambledChars = null;
			this.startValueStrippedLength = (this.changeValueStrippedLength = 0);
		}

		public bool richTextEnabled;

		public ScrambleMode scrambleMode;

		public char[] scrambledChars;

		internal int startValueStrippedLength;

		internal int changeValueStrippedLength;
	}
}
