using System;
using System.Linq;
using UnityEngine;

namespace SA.Common.Util
{
	public class IdFactory
	{
		public static int NextId
		{
			get
			{
				int num = 1;
				if (PlayerPrefs.HasKey("SA.Common.Util.IdFactory_Key"))
				{
					num = PlayerPrefs.GetInt("SA.Common.Util.IdFactory_Key");
					num++;
				}
				PlayerPrefs.SetInt("SA.Common.Util.IdFactory_Key", num);
				return num;
			}
		}

		public static string RandomString
		{
			get
			{
				string element = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
				System.Random random = new System.Random();
				return new string((from s in Enumerable.Repeat<string>(element, 8)
				select s[random.Next(s.Length)]).ToArray<char>());
			}
		}

		private const string PP_ID_KEY = "SA.Common.Util.IdFactory_Key";
	}
}
