using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using SA.Common.Models;
using UnityEngine;

namespace SA.Common.Util
{
	public static class General
	{
		public static void Invoke(float time, Action callback, string name = "")
		{
			Invoker invoker = Invoker.Create(name);
			invoker.StartInvoke(callback, time);
		}

		public static T ParseEnum<T>(string value)
		{
			T result;
			try
			{
				T t = (T)((object)Enum.Parse(typeof(T), value, true));
				result = t;
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("Enum Parsing failed: " + ex.Message);
				result = default(T);
			}
			return result;
		}

		public static int CurrentTimeStamp
		{
			get
			{
				return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
			}
		}

		public static string DateTimeToRfc3339(DateTime dateTime)
		{
			if (dateTime == DateTime.MinValue)
			{
				return "0001-01-01T00:00:00Z";
			}
			return dateTime.ToString("yyyy-MM-dd'T'HH:mm:ssK", DateTimeFormatInfo.InvariantInfo);
		}

		public static DateTime ConvertFromUnixTimestamp(long timestamp)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			return dateTime.AddSeconds((double)timestamp);
		}

		public static long ConvertToUnixTimestamp(DateTime date)
		{
			DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			return (long)(date.ToUniversalTime() - d).TotalSeconds;
		}

		public static string[] DateTimePatterns
		{
			get
			{
				if (General._rfc3339Formats.Length > 0)
				{
					return General._rfc3339Formats;
				}
				General._rfc3339Formats = new string[11];
				General._rfc3339Formats[0] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";
				General._rfc3339Formats[1] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffffK";
				General._rfc3339Formats[2] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffK";
				General._rfc3339Formats[3] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffK";
				General._rfc3339Formats[4] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK";
				General._rfc3339Formats[5] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffK";
				General._rfc3339Formats[6] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fK";
				General._rfc3339Formats[7] = "yyyy'-'MM'-'dd'T'HH':'mm':'ssK";
				General._rfc3339Formats[8] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";
				General._rfc3339Formats[9] = DateTimeFormatInfo.InvariantInfo.UniversalSortableDateTimePattern;
				General._rfc3339Formats[10] = DateTimeFormatInfo.InvariantInfo.SortableDateTimePattern;
				return General._rfc3339Formats;
			}
		}

		public static bool TryParseRfc3339(string s, out DateTime result)
		{
			bool result2 = false;
			result = DateTime.Now;
			DateTime value;
			if (!string.IsNullOrEmpty(s) && DateTime.TryParseExact(s, General.DateTimePatterns, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal, out value))
			{
				result = DateTime.SpecifyKind(value, DateTimeKind.Utc);
				result = result.ToLocalTime();
				result2 = true;
			}
			return result2;
		}

		public static string HMAC(string key, string data)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(key);
			string result;
			using (HMACSHA256 hmacsha = new HMACSHA256(bytes))
			{
				hmacsha.ComputeHash(Encoding.UTF8.GetBytes(data));
				byte[] hash = hmacsha.Hash;
				string text = string.Empty;
				for (int i = 0; i < hash.Length; i++)
				{
					text += hash[i].ToString("X2");
				}
				result = text.ToLower();
			}
			return result;
		}

		public static void CleanupInstallation()
		{
		}

		private static string[] _rfc3339Formats = new string[0];

		private const string Rfc3339Format = "yyyy-MM-dd'T'HH:mm:ssK";

		private const string MinRfc339Value = "0001-01-01T00:00:00Z";
	}
}
