using System;

namespace SA.Common.Pattern
{
	public abstract class NonMonoSingleton<T> where T : NonMonoSingleton<T>, new()
	{
		public static T Instance
		{
			get
			{
				if (NonMonoSingleton<T>._Instance == null)
				{
					NonMonoSingleton<T>._Instance = Activator.CreateInstance<T>();
				}
				return NonMonoSingleton<T>._Instance;
			}
		}

		private static T _Instance;
	}
}
