using System;
using System.Reflection;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class InspectButtonAttribute : Attribute
{
	public InspectButtonAttribute()
	{
		this.buttonName = "NONE";
		this.visibityMode = InspectButtonAttribute.VisibityMode.All;
	}

	public InspectButtonAttribute(string buttonName)
	{
		this.buttonName = buttonName;
		this.visibityMode = InspectButtonAttribute.VisibityMode.All;
	}

	public InspectButtonAttribute(string buttonName, InspectButtonAttribute.VisibityMode visibityMode)
	{
		this.buttonName = buttonName;
		this.visibityMode = visibityMode;
	}

	public const string DefaultButtonNameKey = "NONE";

	public MethodInfo methodInfo;

	public object[] parmeters;

	public string buttonName;

	public InspectButtonAttribute.VisibityMode visibityMode;

	public enum VisibityMode
	{
		All,
		EditModeOnly,
		PlayModeOnly
	}
}
