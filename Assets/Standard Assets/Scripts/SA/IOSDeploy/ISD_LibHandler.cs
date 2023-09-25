using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

namespace SA.IOSDeploy
{
	public class ISD_LibHandler : MonoBehaviour
	{
		public static List<Lib> AvailableLibraries
		{
			get
			{
				List<Lib> list = new List<Lib>();
				List<string> list2 = new List<string>(Enum.GetNames(typeof(iOSLibrary)));
				foreach (Lib lib in ISD_Settings.Instance.Libraries)
				{
					if (list2.Contains(lib.Name))
					{
						list2.Remove(lib.Name);
					}
				}
				IEnumerator enumerator2 = Enum.GetValues(typeof(iOSLibrary)).GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						object obj = enumerator2.Current;
						iOSLibrary lib2 = (iOSLibrary)obj;
						if (list2.Contains(lib2.ToString()))
						{
							list.Add(new Lib(lib2));
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator2 as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				return list;
			}
		}

		public static string[] BaseLibrariesArray()
		{
			List<string> list = new List<string>(ISD_LibHandler.AvailableLibraries.Capacity);
			foreach (Lib lib in ISD_LibHandler.AvailableLibraries)
			{
				list.Add(lib.Name);
			}
			return list.ToArray();
		}

		public static string stringValueOf(iOSLibrary value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (array.Length > 0)
			{
				return array[0].Description;
			}
			return value.ToString();
		}

		public static object enumValueOf(string value)
		{
			Type typeFromHandle = typeof(iOSLibrary);
			string[] names = Enum.GetNames(typeFromHandle);
			foreach (string value2 in names)
			{
				if (ISD_LibHandler.stringValueOf((iOSLibrary)Enum.Parse(typeFromHandle, value2)).Equals(value))
				{
					return Enum.Parse(typeFromHandle, value2);
				}
			}
			throw new ArgumentException("The string is not a description or value of the specified enum...\n " + value + " is not right enum");
		}
	}
}
