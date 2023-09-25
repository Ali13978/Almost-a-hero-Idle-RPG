using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace PlistCS
{
	public static class Plist
	{
		public static object readPlist(string path)
		{
			object result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				result = Plist.readPlist(fileStream, plistType.Auto);
			}
			return result;
		}

		public static object readPlistSource(string source)
		{
			return Plist.readPlist(Encoding.UTF8.GetBytes(source));
		}

		public static object readPlist(byte[] data)
		{
			return Plist.readPlist(new MemoryStream(data), plistType.Auto);
		}

		public static plistType getPlistType(Stream stream)
		{
			byte[] array = new byte[8];
			stream.Read(array, 0, 8);
			if (BitConverter.ToInt64(array, 0) == 3472403351741427810L)
			{
				return plistType.Binary;
			}
			return plistType.Xml;
		}

		public static object readPlist(Stream stream, plistType type)
		{
			if (type == plistType.Auto)
			{
				type = Plist.getPlistType(stream);
				stream.Seek(0L, SeekOrigin.Begin);
			}
			if (type == plistType.Binary)
			{
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					byte[] data = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
					return Plist.readBinary(data);
				}
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(stream);
			return Plist.readXml(xmlDocument);
		}

		public static void writeXml(object value, string path)
		{
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				streamWriter.Write(Plist.writeXml(value));
			}
		}

		public static void writeXml(object value, Stream stream)
		{
			using (StreamWriter streamWriter = new StreamWriter(stream))
			{
				streamWriter.Write(Plist.writeXml(value));
			}
		}

		public static string writeXml(object value)
		{
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings
				{
					Encoding = new UTF8Encoding(false),
					ConformanceLevel = ConformanceLevel.Document,
					Indent = true
				}))
				{
					xmlWriter.WriteStartDocument();
					xmlWriter.WriteStartElement("plist");
					xmlWriter.WriteAttributeString("version", "1.0");
					Plist.compose(value, xmlWriter);
					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndDocument();
					xmlWriter.Flush();
					xmlWriter.Close();
					@string = Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			return @string;
		}

		public static void writeBinary(object value, string path)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(new FileStream(path, FileMode.Create)))
			{
				binaryWriter.Write(Plist.writeBinary(value));
			}
		}

		public static void writeBinary(object value, Stream stream)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(stream))
			{
				binaryWriter.Write(Plist.writeBinary(value));
			}
		}

		public static byte[] writeBinary(object value)
		{
			Plist.offsetTable.Clear();
			Plist.objectTable.Clear();
			Plist.refCount = 0;
			Plist.objRefSize = 0;
			Plist.offsetByteSize = 0;
			Plist.offsetTableOffset = 0L;
			int num = Plist.countObject(value) - 1;
			Plist.refCount = num;
			Plist.objRefSize = Plist.RegulateNullBytes(BitConverter.GetBytes(Plist.refCount)).Length;
			Plist.composeBinary(value);
			Plist.writeBinaryString("bplist00", false);
			Plist.offsetTableOffset = (long)Plist.objectTable.Count;
			Plist.offsetTable.Add(Plist.objectTable.Count - 8);
			Plist.offsetByteSize = Plist.RegulateNullBytes(BitConverter.GetBytes(Plist.offsetTable[Plist.offsetTable.Count - 1])).Length;
			List<byte> list = new List<byte>();
			Plist.offsetTable.Reverse();
			for (int i = 0; i < Plist.offsetTable.Count; i++)
			{
				Plist.offsetTable[i] = Plist.objectTable.Count - Plist.offsetTable[i];
				byte[] array = Plist.RegulateNullBytes(BitConverter.GetBytes(Plist.offsetTable[i]), Plist.offsetByteSize);
				Array.Reverse(array);
				list.AddRange(array);
			}
			Plist.objectTable.AddRange(list);
			Plist.objectTable.AddRange(new byte[6]);
			Plist.objectTable.Add(Convert.ToByte(Plist.offsetByteSize));
			Plist.objectTable.Add(Convert.ToByte(Plist.objRefSize));
			byte[] bytes = BitConverter.GetBytes((long)num + 1L);
			Array.Reverse(bytes);
			Plist.objectTable.AddRange(bytes);
			Plist.objectTable.AddRange(BitConverter.GetBytes(0L));
			bytes = BitConverter.GetBytes(Plist.offsetTableOffset);
			Array.Reverse(bytes);
			Plist.objectTable.AddRange(bytes);
			return Plist.objectTable.ToArray();
		}

		private static object readXml(XmlDocument xml)
		{
			XmlNode node = xml.DocumentElement.ChildNodes[0];
			return Plist.parse(node);
		}

		private static object readBinary(byte[] data)
		{
			Plist.offsetTable.Clear();
			List<byte> offsetTableBytes = new List<byte>();
			Plist.objectTable.Clear();
			Plist.refCount = 0;
			Plist.objRefSize = 0;
			Plist.offsetByteSize = 0;
			Plist.offsetTableOffset = 0L;
			List<byte> list = new List<byte>(data);
			List<byte> range = list.GetRange(list.Count - 32, 32);
			Plist.parseTrailer(range);
			Plist.objectTable = list.GetRange(0, (int)Plist.offsetTableOffset);
			offsetTableBytes = list.GetRange((int)Plist.offsetTableOffset, list.Count - (int)Plist.offsetTableOffset - 32);
			Plist.parseOffsetTable(offsetTableBytes);
			return Plist.parseBinary(0);
		}

		private static Dictionary<string, object> parseDictionary(XmlNode node)
		{
			XmlNodeList childNodes = node.ChildNodes;
			if (childNodes.Count % 2 != 0)
			{
				throw new DataMisalignedException("Dictionary elements must have an even number of child nodes");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			for (int i = 0; i < childNodes.Count; i += 2)
			{
				XmlNode xmlNode = childNodes[i];
				XmlNode node2 = childNodes[i + 1];
				if (xmlNode.Name != "key")
				{
					throw new ApplicationException("expected a key node");
				}
				object obj = Plist.parse(node2);
				if (obj != null)
				{
					dictionary.Add(xmlNode.InnerText, obj);
				}
			}
			return dictionary;
		}

		private static List<object> parseArray(XmlNode node)
		{
			List<object> list = new List<object>();
			IEnumerator enumerator = node.ChildNodes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					XmlNode node2 = (XmlNode)obj;
					object obj2 = Plist.parse(node2);
					if (obj2 != null)
					{
						list.Add(obj2);
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return list;
		}

		private static void composeArray(List<object> value, XmlWriter writer)
		{
			writer.WriteStartElement("array");
			foreach (object value2 in value)
			{
				Plist.compose(value2, writer);
			}
			writer.WriteEndElement();
		}

		private static object parse(XmlNode node)
		{
			string name = node.Name;
			switch (name)
			{
			case "dict":
				return Plist.parseDictionary(node);
			case "array":
				return Plist.parseArray(node);
			case "string":
				return node.InnerText;
			case "integer":
				return Convert.ToInt32(node.InnerText, NumberFormatInfo.InvariantInfo);
			case "real":
				return Convert.ToDouble(node.InnerText, NumberFormatInfo.InvariantInfo);
			case "false":
				return false;
			case "true":
				return true;
			case "null":
				return null;
			case "date":
				return XmlConvert.ToDateTime(node.InnerText, XmlDateTimeSerializationMode.Utc);
			case "data":
				return Convert.FromBase64String(node.InnerText);
			}
			throw new ApplicationException(string.Format("Plist Node `{0}' is not supported", node.Name));
		}

		private static void compose(object value, XmlWriter writer)
		{
			if (value == null || value is string)
			{
				writer.WriteElementString("string", value as string);
			}
			else if (value is int || value is long)
			{
				writer.WriteElementString("integer", ((int)value).ToString(NumberFormatInfo.InvariantInfo));
			}
			else if (value is Dictionary<string, object> || value.GetType().ToString().StartsWith("System.Collections.Generic.Dictionary`2[System.String"))
			{
				Dictionary<string, object> dictionary = value as Dictionary<string, object>;
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>();
					IDictionary dictionary2 = (IDictionary)value;
					IEnumerator enumerator = dictionary2.Keys.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							dictionary.Add(obj.ToString(), dictionary2[obj]);
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = (enumerator as IDisposable)) != null)
						{
							disposable.Dispose();
						}
					}
				}
				Plist.writeDictionaryValues(dictionary, writer);
			}
			else if (value is List<object>)
			{
				Plist.composeArray((List<object>)value, writer);
			}
			else if (value is byte[])
			{
				writer.WriteElementString("data", Convert.ToBase64String((byte[])value));
			}
			else if (value is float || value is double)
			{
				writer.WriteElementString("real", ((double)value).ToString(NumberFormatInfo.InvariantInfo));
			}
			else if (value is DateTime)
			{
				DateTime value2 = (DateTime)value;
				string value3 = XmlConvert.ToString(value2, XmlDateTimeSerializationMode.Utc);
				writer.WriteElementString("date", value3);
			}
			else
			{
				if (!(value is bool))
				{
					throw new Exception(string.Format("Value type '{0}' is unhandled", value.GetType().ToString()));
				}
				writer.WriteElementString(value.ToString().ToLower(), string.Empty);
			}
		}

		private static void writeDictionaryValues(Dictionary<string, object> dictionary, XmlWriter writer)
		{
			writer.WriteStartElement("dict");
			foreach (string text in dictionary.Keys)
			{
				object value = dictionary[text];
				writer.WriteElementString("key", text);
				Plist.compose(value, writer);
			}
			writer.WriteEndElement();
		}

		private static int countObject(object value)
		{
			int num = 0;
			string text = value.GetType().ToString();
			if (text != null)
			{
				if (text == "System.Collections.Generic.Dictionary`2[System.String,System.Object]")
				{
					Dictionary<string, object> dictionary = (Dictionary<string, object>)value;
					foreach (string key in dictionary.Keys)
					{
						num += Plist.countObject(dictionary[key]);
					}
					num += dictionary.Keys.Count;
					return num + 1;
				}
				if (text == "System.Collections.Generic.List`1[System.Object]")
				{
					List<object> list = (List<object>)value;
					foreach (object value2 in list)
					{
						num += Plist.countObject(value2);
					}
					return num + 1;
				}
			}
			num++;
			return num;
		}

		private static byte[] writeBinaryDictionary(Dictionary<string, object> dictionary)
		{
			List<byte> list = new List<byte>();
			List<byte> list2 = new List<byte>();
			List<int> list3 = new List<int>();
			for (int i = dictionary.Count - 1; i >= 0; i--)
			{
				object[] array = new object[dictionary.Count];
				dictionary.Values.CopyTo(array, 0);
				Plist.composeBinary(array[i]);
				Plist.offsetTable.Add(Plist.objectTable.Count);
				list3.Add(Plist.refCount);
				Plist.refCount--;
			}
			for (int j = dictionary.Count - 1; j >= 0; j--)
			{
				string[] array2 = new string[dictionary.Count];
				dictionary.Keys.CopyTo(array2, 0);
				Plist.composeBinary(array2[j]);
				Plist.offsetTable.Add(Plist.objectTable.Count);
				list3.Add(Plist.refCount);
				Plist.refCount--;
			}
			if (dictionary.Count < 15)
			{
				list2.Add(Convert.ToByte((int)(208 | Convert.ToByte(dictionary.Count))));
			}
			else
			{
				list2.Add(223);
				list2.AddRange(Plist.writeBinaryInteger(dictionary.Count, false));
			}
			foreach (int value in list3)
			{
				byte[] array3 = Plist.RegulateNullBytes(BitConverter.GetBytes(value), Plist.objRefSize);
				Array.Reverse(array3);
				list.InsertRange(0, array3);
			}
			list.InsertRange(0, list2);
			Plist.objectTable.InsertRange(0, list);
			return list.ToArray();
		}

		private static byte[] composeBinaryArray(List<object> objects)
		{
			List<byte> list = new List<byte>();
			List<byte> list2 = new List<byte>();
			List<int> list3 = new List<int>();
			for (int i = objects.Count - 1; i >= 0; i--)
			{
				Plist.composeBinary(objects[i]);
				Plist.offsetTable.Add(Plist.objectTable.Count);
				list3.Add(Plist.refCount);
				Plist.refCount--;
			}
			if (objects.Count < 15)
			{
				list2.Add(Convert.ToByte((int)(160 | Convert.ToByte(objects.Count))));
			}
			else
			{
				list2.Add(175);
				list2.AddRange(Plist.writeBinaryInteger(objects.Count, false));
			}
			foreach (int value in list3)
			{
				byte[] array = Plist.RegulateNullBytes(BitConverter.GetBytes(value), Plist.objRefSize);
				Array.Reverse(array);
				list.InsertRange(0, array);
			}
			list.InsertRange(0, list2);
			Plist.objectTable.InsertRange(0, list);
			return list.ToArray();
		}

		private static byte[] composeBinary(object obj)
		{
			string text = obj.GetType().ToString();
			switch (text)
			{
			case "System.Collections.Generic.Dictionary`2[System.String,System.Object]":
				return Plist.writeBinaryDictionary((Dictionary<string, object>)obj);
			case "System.Collections.Generic.List`1[System.Object]":
				return Plist.composeBinaryArray((List<object>)obj);
			case "System.Byte[]":
				return Plist.writeBinaryByteArray((byte[])obj);
			case "System.Double":
				return Plist.writeBinaryDouble((double)obj);
			case "System.Int32":
				return Plist.writeBinaryInteger((int)obj, true);
			case "System.String":
				return Plist.writeBinaryString((string)obj, true);
			case "System.DateTime":
				return Plist.writeBinaryDate((DateTime)obj);
			case "System.Boolean":
				return Plist.writeBinaryBool((bool)obj);
			}
			return new byte[0];
		}

		public static byte[] writeBinaryDate(DateTime obj)
		{
			List<byte> list = new List<byte>(Plist.RegulateNullBytes(BitConverter.GetBytes(PlistDateConverter.ConvertToAppleTimeStamp(obj)), 8));
			list.Reverse();
			list.Insert(0, 51);
			Plist.objectTable.InsertRange(0, list);
			return list.ToArray();
		}

		public static byte[] writeBinaryBool(bool obj)
		{
			List<byte> list = new List<byte>(new byte[]
			{
				(!obj) ? (byte)8 : (byte)9
			});
			Plist.objectTable.InsertRange(0, list);
			return list.ToArray();
		}

		private static byte[] writeBinaryInteger(int value, bool write)
		{
			List<byte> list = new List<byte>(BitConverter.GetBytes((long)value));
			list = new List<byte>(Plist.RegulateNullBytes(list.ToArray()));
			while ((double)list.Count != Math.Pow(2.0, Math.Log((double)list.Count) / Math.Log(2.0)))
			{
				list.Add(0);
			}
			int value2 = 16 | (int)(Math.Log((double)list.Count) / Math.Log(2.0));
			list.Reverse();
			list.Insert(0, Convert.ToByte(value2));
			if (write)
			{
				Plist.objectTable.InsertRange(0, list);
			}
			return list.ToArray();
		}

		private static byte[] writeBinaryDouble(double value)
		{
			List<byte> list = new List<byte>(Plist.RegulateNullBytes(BitConverter.GetBytes(value), 4));
			while ((double)list.Count != Math.Pow(2.0, Math.Log((double)list.Count) / Math.Log(2.0)))
			{
				list.Add(0);
			}
			int value2 = 32 | (int)(Math.Log((double)list.Count) / Math.Log(2.0));
			list.Reverse();
			list.Insert(0, Convert.ToByte(value2));
			Plist.objectTable.InsertRange(0, list);
			return list.ToArray();
		}

		private static byte[] writeBinaryByteArray(byte[] value)
		{
			List<byte> list = new List<byte>(value);
			List<byte> list2 = new List<byte>();
			if (value.Length < 15)
			{
				list2.Add(Convert.ToByte((int)(64 | Convert.ToByte(value.Length))));
			}
			else
			{
				list2.Add(79);
				list2.AddRange(Plist.writeBinaryInteger(list.Count, false));
			}
			list.InsertRange(0, list2);
			Plist.objectTable.InsertRange(0, list);
			return list.ToArray();
		}

		private static byte[] writeBinaryString(string value, bool head)
		{
			List<byte> list = new List<byte>();
			List<byte> list2 = new List<byte>();
			foreach (char value2 in value.ToCharArray())
			{
				list.Add(Convert.ToByte(value2));
			}
			if (head)
			{
				if (value.Length < 15)
				{
					list2.Add(Convert.ToByte((int)(80 | Convert.ToByte(value.Length))));
				}
				else
				{
					list2.Add(95);
					list2.AddRange(Plist.writeBinaryInteger(list.Count, false));
				}
			}
			list.InsertRange(0, list2);
			Plist.objectTable.InsertRange(0, list);
			return list.ToArray();
		}

		private static byte[] RegulateNullBytes(byte[] value)
		{
			return Plist.RegulateNullBytes(value, 1);
		}

		private static byte[] RegulateNullBytes(byte[] value, int minBytes)
		{
			Array.Reverse(value);
			List<byte> list = new List<byte>(value);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] != 0 || list.Count <= minBytes)
				{
					break;
				}
				list.Remove(list[i]);
				i--;
			}
			if (list.Count < minBytes)
			{
				int num = minBytes - list.Count;
				for (int j = 0; j < num; j++)
				{
					list.Insert(0, 0);
				}
			}
			value = list.ToArray();
			Array.Reverse(value);
			return value;
		}

		private static void parseTrailer(List<byte> trailer)
		{
			Plist.offsetByteSize = BitConverter.ToInt32(Plist.RegulateNullBytes(trailer.GetRange(6, 1).ToArray(), 4), 0);
			Plist.objRefSize = BitConverter.ToInt32(Plist.RegulateNullBytes(trailer.GetRange(7, 1).ToArray(), 4), 0);
			byte[] array = trailer.GetRange(12, 4).ToArray();
			Array.Reverse(array);
			Plist.refCount = BitConverter.ToInt32(array, 0);
			byte[] array2 = trailer.GetRange(24, 8).ToArray();
			Array.Reverse(array2);
			Plist.offsetTableOffset = BitConverter.ToInt64(array2, 0);
		}

		private static void parseOffsetTable(List<byte> offsetTableBytes)
		{
			for (int i = 0; i < offsetTableBytes.Count; i += Plist.offsetByteSize)
			{
				byte[] array = offsetTableBytes.GetRange(i, Plist.offsetByteSize).ToArray();
				Array.Reverse(array);
				Plist.offsetTable.Add(BitConverter.ToInt32(Plist.RegulateNullBytes(array, 4), 0));
			}
		}

		private static object parseBinaryDictionary(int objRef)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			List<int> list = new List<int>();
			int num;
			int count = Plist.getCount(Plist.offsetTable[objRef], out num);
			if (count < 15)
			{
				num = Plist.offsetTable[objRef] + 1;
			}
			else
			{
				num = Plist.offsetTable[objRef] + 2 + Plist.RegulateNullBytes(BitConverter.GetBytes(count), 1).Length;
			}
			for (int i = num; i < num + count * 2 * Plist.objRefSize; i += Plist.objRefSize)
			{
				byte[] array = Plist.objectTable.GetRange(i, Plist.objRefSize).ToArray();
				Array.Reverse(array);
				list.Add(BitConverter.ToInt32(Plist.RegulateNullBytes(array, 4), 0));
			}
			for (int j = 0; j < count; j++)
			{
				dictionary.Add((string)Plist.parseBinary(list[j]), Plist.parseBinary(list[j + count]));
			}
			return dictionary;
		}

		private static object parseBinaryArray(int objRef)
		{
			List<object> list = new List<object>();
			List<int> list2 = new List<int>();
			int num;
			int count = Plist.getCount(Plist.offsetTable[objRef], out num);
			if (count < 15)
			{
				num = Plist.offsetTable[objRef] + 1;
			}
			else
			{
				num = Plist.offsetTable[objRef] + 2 + Plist.RegulateNullBytes(BitConverter.GetBytes(count), 1).Length;
			}
			for (int i = num; i < num + count * Plist.objRefSize; i += Plist.objRefSize)
			{
				byte[] array = Plist.objectTable.GetRange(i, Plist.objRefSize).ToArray();
				Array.Reverse(array);
				list2.Add(BitConverter.ToInt32(Plist.RegulateNullBytes(array, 4), 0));
			}
			for (int j = 0; j < count; j++)
			{
				list.Add(Plist.parseBinary(list2[j]));
			}
			return list;
		}

		private static int getCount(int bytePosition, out int newBytePosition)
		{
			byte b = Plist.objectTable[bytePosition];
			byte b2 = Convert.ToByte((int)(b & 15));
			int result;
			if (b2 < 15)
			{
				result = (int)b2;
				newBytePosition = bytePosition + 1;
			}
			else
			{
				result = (int)Plist.parseBinaryInt(bytePosition + 1, out newBytePosition);
			}
			return result;
		}

		private static object parseBinary(int objRef)
		{
			byte b = Plist.objectTable[Plist.offsetTable[objRef]];
			int num = (int)(b & 240);
			if (num == 0)
			{
				return (Plist.objectTable[Plist.offsetTable[objRef]] != 0) ? (Plist.objectTable[Plist.offsetTable[objRef]] == 9) : (object)null;
			}
			if (num == 16)
			{
				return Plist.parseBinaryInt(Plist.offsetTable[objRef]);
			}
			if (num == 32)
			{
				return Plist.parseBinaryReal(Plist.offsetTable[objRef]);
			}
			if (num == 48)
			{
				return Plist.parseBinaryDate(Plist.offsetTable[objRef]);
			}
			if (num == 64)
			{
				return Plist.parseBinaryByteArray(Plist.offsetTable[objRef]);
			}
			if (num == 80)
			{
				return Plist.parseBinaryAsciiString(Plist.offsetTable[objRef]);
			}
			if (num == 96)
			{
				return Plist.parseBinaryUnicodeString(Plist.offsetTable[objRef]);
			}
			if (num == 160)
			{
				return Plist.parseBinaryArray(objRef);
			}
			if (num != 208)
			{
				throw new Exception("This type is not supported");
			}
			return Plist.parseBinaryDictionary(objRef);
		}

		public static object parseBinaryDate(int headerPosition)
		{
			byte[] array = Plist.objectTable.GetRange(headerPosition + 1, 8).ToArray();
			Array.Reverse(array);
			double timestamp = BitConverter.ToDouble(array, 0);
			DateTime dateTime = PlistDateConverter.ConvertFromAppleTimeStamp(timestamp);
			return dateTime;
		}

		private static object parseBinaryInt(int headerPosition)
		{
			int num;
			return Plist.parseBinaryInt(headerPosition, out num);
		}

		private static object parseBinaryInt(int headerPosition, out int newHeaderPosition)
		{
			byte b = Plist.objectTable[headerPosition];
			int num = (int)Math.Pow(2.0, (double)(b & 15));
			byte[] array = Plist.objectTable.GetRange(headerPosition + 1, num).ToArray();
			Array.Reverse(array);
			newHeaderPosition = headerPosition + num + 1;
			return BitConverter.ToInt32(Plist.RegulateNullBytes(array, 4), 0);
		}

		private static object parseBinaryReal(int headerPosition)
		{
			byte b = Plist.objectTable[headerPosition];
			int count = (int)Math.Pow(2.0, (double)(b & 15));
			byte[] array = Plist.objectTable.GetRange(headerPosition + 1, count).ToArray();
			Array.Reverse(array);
			return BitConverter.ToDouble(Plist.RegulateNullBytes(array, 8), 0);
		}

		private static object parseBinaryAsciiString(int headerPosition)
		{
			int index;
			int count = Plist.getCount(headerPosition, out index);
			List<byte> range = Plist.objectTable.GetRange(index, count);
			return (range.Count <= 0) ? string.Empty : Encoding.ASCII.GetString(range.ToArray());
		}

		private static object parseBinaryUnicodeString(int headerPosition)
		{
			int num2;
			int num = Plist.getCount(headerPosition, out num2);
			num *= 2;
			byte[] array = new byte[num];
			for (int i = 0; i < num; i += 2)
			{
				byte b = Plist.objectTable.GetRange(num2 + i, 1)[0];
				byte b2 = Plist.objectTable.GetRange(num2 + i + 1, 1)[0];
				if (BitConverter.IsLittleEndian)
				{
					array[i] = b2;
					array[i + 1] = b;
				}
				else
				{
					array[i] = b;
					array[i + 1] = b2;
				}
			}
			return Encoding.Unicode.GetString(array);
		}

		private static object parseBinaryByteArray(int headerPosition)
		{
			int index;
			int count = Plist.getCount(headerPosition, out index);
			return Plist.objectTable.GetRange(index, count).ToArray();
		}

		private static List<int> offsetTable = new List<int>();

		private static List<byte> objectTable = new List<byte>();

		private static int refCount;

		private static int objRefSize;

		private static int offsetByteSize;

		private static long offsetTableOffset;
	}
}
