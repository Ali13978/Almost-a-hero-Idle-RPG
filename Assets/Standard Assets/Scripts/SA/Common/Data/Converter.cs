using System;
using System.Collections.Generic;

namespace SA.Common.Data
{
	public class Converter
	{
		public static string SerializeArray(List<string> array, string splitter = "%%%")
		{
			return Converter.SerializeArray(array.ToArray(), splitter);
		}

		public static string SerializeArray(string[] array, string splitter = "%%%")
		{
			if (array == null)
			{
				return string.Empty;
			}
			if (array.Length == 0)
			{
				return string.Empty;
			}
			string text = string.Empty;
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				if (i != 0)
				{
					text += splitter;
				}
				text += array[i];
			}
			return text;
		}

		public static string[] ParseArray(string arrayData, string splitter = "%%%")
		{
			List<string> list = new List<string>();
			string[] array = arrayData.Split(new string[]
			{
				splitter
			}, StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "endofline")
				{
					break;
				}
				list.Add(array[i]);
			}
			return list.ToArray();
		}

		public const char DATA_SPLITTER = '|';

		public const string DATA_SPLITTER2 = "|%|";

		public const string ARRAY_SPLITTER = "%%%";

		public const string DATA_EOF = "endofline";
	}
}
