using System;
using System.Collections.Generic;

namespace Tapdaq
{
	public static class TDExtensionMethods
	{
		public static int ParseInt(this string str, int defaultValue)
		{
			int result;
			if (int.TryParse(str, out result))
			{
				return result;
			}
			return defaultValue;
		}

		public static float ParseFloat(this string str, float defaultValue)
		{
			float result;
			if (float.TryParse(str, out result))
			{
				return result;
			}
			return defaultValue;
		}

		public static TV GetValue<TK, TV>(this Dictionary<TK, TV> dict, TK key, TV defaultValue = default(TV))
		{
			if (!dict.ContainsKey(key))
			{
				return defaultValue;
			}
			return dict[key];
		}

		public static T GetValueOrDefault<T>(this List<T> list, int index, T def = default(T))
		{
			if (index >= list.Count)
			{
				return def;
			}
			return list[index];
		}
	}
}
