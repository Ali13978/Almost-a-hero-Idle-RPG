using System;
using System.Collections.Generic;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.IOSNative.Privacy
{
	public static class PermissionsManager
	{
		public static PermissionStatus CheckPermissions(Permission permission)
		{
			return PermissionStatus.NotDetermined;
		}

		public static void RequestPermission(Permission permission, Action<PermissionStatus> callback)
		{
			if (Singleton<NativeReceiver>.Instance == null)
			{
				Singleton<NativeReceiver>.Instance.Init();
			}
			PermissionsManager.OnResponseDictionary[permission.ToString()] = callback;
		}

		internal static void PermissionRequestResponse(string permissionData)
		{
			string[] array = permissionData.Split(new string[]
			{
				"|%|"
			}, StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "endofline")
				{
					break;
				}
			}
			if (array.Length > 0)
			{
				string key = array[0];
				Action<PermissionStatus> action = PermissionsManager.OnResponseDictionary[key];
				if (action != null)
				{
					string text = array[1];
					if (text != null)
					{
						try
						{
							int num = int.Parse(text);
							PermissionStatus obj = (PermissionStatus)num;
							action(obj);
						}
						catch (FormatException ex)
						{
							ISN_Logger.Log(ex.ToString(), LogType.Log);
						}
					}
				}
			}
		}

		private static Dictionary<string, Action<PermissionStatus>> OnResponseDictionary = new Dictionary<string, Action<PermissionStatus>>();
	}
}
