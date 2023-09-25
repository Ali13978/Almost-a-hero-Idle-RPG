using System;
using System.Text;

namespace Static
{
	public static class StringExtension
	{
		public static StringBuilder StringBuilder
		{
			get
			{
				StringExtension.stringBuilder.Length = 0;
				return StringExtension.stringBuilder;
			}
		}

		public static string ConcatAll(params object[] param)
		{
			StringBuilder stringBuilder = StringExtension.StringBuilder;
			for (int i = 0; i < param.Length; i++)
			{
				stringBuilder.Append(param[i]);
			}
			return stringBuilder.ToString();
		}

		public static string Concat(string string0, string string1)
		{
			return StringExtension.StringBuilder.Append(string0).Append(string1).ToString();
		}

		public static string Concat(string string0, string string1, string string2)
		{
			return StringExtension.StringBuilder.Append(string0).Append(string1).Append(string2).ToString();
		}

		public static string Concat(string string0, string string1, string string2, string string3)
		{
			return StringExtension.StringBuilder.Append(string0).Append(string1).Append(string2).Append(string3).ToString();
		}

		public static string Concat(string string0, string string1, string string2, string string3, string string4)
		{
			return StringExtension.StringBuilder.Append(string0).Append(string1).Append(string2).Append(string3).Append(string4).ToString();
		}

		private static StringBuilder stringBuilder = new StringBuilder();
	}
}
