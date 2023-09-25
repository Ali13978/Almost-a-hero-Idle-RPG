using System;
using System.Collections;

namespace Tapdaq
{
	public static class TDEnumHelper
	{
		public static string FixAndroidAdapterName(string adapterName)
		{
			if (adapterName == Enum.GetName(typeof(TapdaqAdapter), TapdaqAdapter.FANAdapter))
			{
				return "FacebookAdapter";
			}
			return adapterName;
		}

		public static T GetEnumFromString<T>(string enumString, T defaultValue = default(T))
		{
			Array array = null;
			try
			{
				array = Enum.GetValues(typeof(T));
			}
			catch (Exception ex)
			{
				TDDebugLogger.LogError("Can't GetEnumFromString: " + enumString);
				return defaultValue;
			}
			if (array == null)
			{
				return defaultValue;
			}
			IEnumerator enumerator = array.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					if (obj.ToString().ToLower() == enumString.ToLower())
					{
						return (T)((object)obj);
					}
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
			return defaultValue;
		}
	}
}
