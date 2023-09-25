using System;
using System.Collections.Generic;

namespace UnityEditor.XCodeEditor
{
	public class PBXSortedDictionary<T> : SortedDictionary<string, T> where T : PBXObject
	{
		public PBXSortedDictionary()
		{
		}

		public PBXSortedDictionary(PBXDictionary genericDictionary)
		{
			foreach (KeyValuePair<string, object> keyValuePair in genericDictionary)
			{
				if (((string)((PBXDictionary)keyValuePair.Value)["isa"]).CompareTo(typeof(T).Name) == 0)
				{
					T value = (T)((object)Activator.CreateInstance(typeof(T), new object[]
					{
						keyValuePair.Key,
						(PBXDictionary)keyValuePair.Value
					}));
					base.Add(keyValuePair.Key, value);
				}
			}
		}

		public void Add(T newObject)
		{
			base.Add(newObject.guid, newObject);
		}

		public void Append(PBXDictionary<T> dictionary)
		{
			foreach (KeyValuePair<string, T> keyValuePair in dictionary)
			{
				base.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}
}
