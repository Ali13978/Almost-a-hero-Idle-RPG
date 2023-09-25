using System;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.IOSNative.Privacy
{
	public class NativeReceiver : Singleton<NativeReceiver>
	{
		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void Init()
		{
		}

		private void PermissionRequestResponseReceived(string permissionData)
		{
			PermissionsManager.PermissionRequestResponse(permissionData);
		}
	}
}
