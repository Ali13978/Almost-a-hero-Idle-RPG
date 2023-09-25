using System;

public class ByteBuffer
{
	public ByteBuffer(byte[] buf)
	{
		this.buffer = buf;
	}

	public int readInt()
	{
		int result = BitConverter.ToInt32(this.buffer, this.pointer);
		this.pointer += 4;
		return result;
	}

	public float readFloat()
	{
		float result = BitConverter.ToSingle(this.buffer, this.pointer);
		this.pointer += 4;
		return result;
	}

	private byte[] buffer;

	public int pointer;
}
