using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace SA.IOSDeploy
{
	public class ISD_FrameworkHandler : MonoBehaviour
	{
		public static List<Framework> AvailableFrameworks
		{
			get
			{
				List<Framework> list = new List<Framework>();
				List<string> list2 = new List<string>(Enum.GetNames(typeof(iOSFramework)));
				foreach (Framework framework in ISD_Settings.Instance.Frameworks)
				{
					if (list2.Contains(framework.Type.ToString()))
					{
						list2.Remove(framework.Type.ToString());
					}
				}
				foreach (Framework framework2 in ISD_FrameworkHandler.DefaultFrameworks)
				{
					if (list2.Contains(framework2.Type.ToString()))
					{
						list2.Remove(framework2.Type.ToString());
					}
				}
				IEnumerator enumerator3 = Enum.GetValues(typeof(iOSFramework)).GetEnumerator();
				try
				{
					while (enumerator3.MoveNext())
					{
						object obj = enumerator3.Current;
						iOSFramework type = (iOSFramework)obj;
						if (list2.Contains(type.ToString()))
						{
							list.Add(new Framework(type, false));
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator3 as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				return list;
			}
		}

		public static List<string> GetImportedFrameworks()
		{
			List<string> list = new List<string>();
			DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
			string[] dirrExtensions = new string[]
			{
				".framework"
			};
			FileInfo[] array = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
			DirectoryInfo[] array2 = directoryInfo.GetDirectories("*", SearchOption.AllDirectories);
			array = (from f in array
			where dirrExtensions.Contains(f.Extension.ToLower())
			select f).ToArray<FileInfo>();
			array2 = (from f in array2
			where dirrExtensions.Contains(f.Extension.ToLower())
			select f).ToArray<DirectoryInfo>();
			foreach (FileInfo fileInfo in array)
			{
				string name = fileInfo.Name;
				list.Add(name);
			}
			foreach (DirectoryInfo directoryInfo2 in array2)
			{
				string name2 = directoryInfo2.Name;
				list.Add(name2);
			}
			return list;
		}

		public static List<string> GetImportedLibraries()
		{
			List<string> list = new List<string>();
			DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
			string[] fileExtensions = new string[]
			{
				".a",
				".dylib"
			};
			FileInfo[] array = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
			array = (from f in array
			where fileExtensions.Contains(f.Extension.ToLower())
			select f).ToArray<FileInfo>();
			foreach (FileInfo fileInfo in array)
			{
				string name = fileInfo.Name;
				list.Add(name);
			}
			return list;
		}

		public static List<Framework> DefaultFrameworks
		{
			get
			{
				if (ISD_FrameworkHandler._DefaultFrameworks == null)
				{
					ISD_FrameworkHandler._DefaultFrameworks = new List<Framework>();
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.CoreText, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.AudioToolbox, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.AVFoundation, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.CFNetwork, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.CoreGraphics, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.CoreLocation, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.CoreMedia, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.CoreMotion, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.CoreVideo, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.Foundation, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.iAd, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.MediaPlayer, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.OpenAL, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.OpenGLES, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.QuartzCore, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.SystemConfiguration, false));
					ISD_FrameworkHandler._DefaultFrameworks.Add(new Framework(iOSFramework.UIKit, false));
				}
				return ISD_FrameworkHandler._DefaultFrameworks;
			}
		}

		public static string[] BaseFrameworksArray()
		{
			List<string> list = new List<string>(ISD_FrameworkHandler.AvailableFrameworks.Capacity);
			foreach (Framework framework in ISD_FrameworkHandler.AvailableFrameworks)
			{
				list.Add(framework.Type.ToString());
			}
			return list.ToArray();
		}

		private static List<Framework> _DefaultFrameworks;
	}
}
