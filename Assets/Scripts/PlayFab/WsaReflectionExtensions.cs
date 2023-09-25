using System;
using System.Reflection;

namespace PlayFab
{
	public static class WsaReflectionExtensions
	{
		public static Delegate CreateDelegate(this MethodInfo methodInfo, Type delegateType, object instance)
		{
			return Delegate.CreateDelegate(delegateType, instance, methodInfo);
		}

		public static Type GetTypeInfo(this Type type)
		{
			return type;
		}

		public static Type AsType(this Type type)
		{
			return type;
		}

		public static string GetDelegateName(this Delegate delegateInstance)
		{
			return delegateInstance.Method.Name;
		}
	}
}
