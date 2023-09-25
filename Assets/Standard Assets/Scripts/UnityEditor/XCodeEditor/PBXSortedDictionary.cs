using System;
using System.Collections.Generic;

namespace UnityEditor.XCodeEditor
{
	public class PBXSortedDictionary : SortedDictionary<string, object>
	{
		public void Append(PBXDictionary dictionary)
		{
			foreach (KeyValuePair<string, object> keyValuePair in dictionary)
			{
				base.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		public void Append<T>(PBXDictionary<T> dictionary) where T : PBXObject
		{
			foreach (KeyValuePair<string, T> keyValuePair in dictionary)
			{
				base.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}
}
