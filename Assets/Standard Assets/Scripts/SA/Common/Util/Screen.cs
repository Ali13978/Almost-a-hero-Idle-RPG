using System;
using SA.Common.Models;
using UnityEngine;

namespace SA.Common.Util
{
	public static class Screen
	{
		public static void TakeScreenshot(Action<Texture2D> callback)
		{
			ScreenshotMaker screenshotMaker = ScreenshotMaker.Create();
			ScreenshotMaker screenshotMaker2 = screenshotMaker;
			screenshotMaker2.OnScreenshotReady = (Action<Texture2D>)Delegate.Combine(screenshotMaker2.OnScreenshotReady, callback);
			screenshotMaker.GetScreenshot();
		}
	}
}
