using System;

public class ObjectCreatePackage : BasePackage
{
	public ObjectCreatePackage(float x, float y)
	{
		base.initWriter();
		base.writeFloat(x);
		base.writeFloat(y);
	}

	public override int getId()
	{
		return 1;
	}
}
