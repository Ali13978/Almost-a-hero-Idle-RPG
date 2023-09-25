using System;
using System.IO;

public abstract class BasePackage
{
	public void initWriter()
	{
		this.writer = new BinaryWriter(this.buffer);
		this.writeInt(this.getId());
	}

	public abstract int getId();

	public byte[] getBytes()
	{
		return this.buffer.ToArray();
	}

	public void send()
	{
		NetworkManagerExample.send(this);
	}

	public void writeInt(int val)
	{
		this.writer.Write(val);
	}

	public void writeString(string val)
	{
	}

	public void writeFloat(float val)
	{
		this.writer.Write(val);
	}

	protected MemoryStream buffer = new MemoryStream();

	protected BinaryWriter writer;
}
