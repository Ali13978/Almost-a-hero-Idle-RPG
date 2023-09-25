using System;

public class HelloPackage : BasePackage
{
	public HelloPackage()
	{
		base.initWriter();
		base.writeFloat(1.1f);
	}

	public override int getId()
	{
		return 2;
	}
}
