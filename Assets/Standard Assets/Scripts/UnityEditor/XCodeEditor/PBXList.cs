using System;
using System.Collections;

namespace UnityEditor.XCodeEditor
{
	public class PBXList : ArrayList
	{
		public PBXList()
		{
		}

		public PBXList(object firstValue)
		{
			this.Add(firstValue);
		}

		public static implicit operator bool(PBXList x)
		{
			return x != null && x.Count == 0;
		}

		public string ToCSV()
		{
			string text = string.Empty;
			IEnumerator enumerator = this.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					string str = (string)obj;
					text += "\"";
					text += str;
					text += "\", ";
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return text;
		}

		public override string ToString()
		{
			return "{" + this.ToCSV() + "} ";
		}
	}
}
