using System;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.IOSNative.UIKit
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

		private void CalendarPickerClosed(string time)
		{
			Calendar.DatePicked(time);
		}

		private void DateTimePickerClosed(string time)
		{
			DateTimePicker.PickerClosed(time);
		}

		private void DateTimePickerDateChanged(string time)
		{
			DateTimePicker.DateChangedEvent(time);
		}
	}
}
