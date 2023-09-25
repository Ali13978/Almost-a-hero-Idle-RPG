using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace UnityEditor.XCodeEditor
{
	public class PBXParser
	{
		public PBXDictionary Decode(string data)
		{
			if (!data.StartsWith("// !$*UTF8*$!\n"))
			{
				UnityEngine.Debug.Log("Wrong file format.");
				return null;
			}
			data = data.Substring(13);
			this.data = data.ToCharArray();
			this.index = 0;
			return (PBXDictionary)this.ParseValue();
		}

		public string Encode(PBXDictionary pbxData, bool readable = false)
		{
			this.resolver = new PBXResolver(pbxData);
			StringBuilder stringBuilder = new StringBuilder("// !$*UTF8*$!\n", 20000);
			bool flag = this.SerializeValue(pbxData, stringBuilder, readable, 0);
			this.resolver = null;
			stringBuilder.Append("\n");
			return (!flag) ? null : stringBuilder.ToString();
		}

		private void Indent(StringBuilder builder, int deep)
		{
			builder.Append(string.Empty.PadLeft(deep, '\t'));
		}

		private void Endline(StringBuilder builder, bool useSpace = false)
		{
			builder.Append((!useSpace) ? "\n" : " ");
		}

		private void MarkSection(StringBuilder builder, string name)
		{
			if (this.marker == null && name == null)
			{
				return;
			}
			if (this.marker != null && name != this.marker)
			{
				builder.Append(string.Format("/* End {0} section */\n", this.marker));
			}
			if (name != null && name != this.marker)
			{
				builder.Append(string.Format("\n/* Begin {0} section */\n", name));
			}
			this.marker = name;
		}

		private bool GUIDComment(string guid, StringBuilder builder)
		{
			string text = this.resolver.ResolveName(guid);
			string text2 = this.resolver.ResolveBuildPhaseNameForFile(guid);
			if (text != null)
			{
				if (text2 != null)
				{
					builder.Append(string.Format(" /* {0} in {1} */", text, text2));
				}
				else
				{
					builder.Append(string.Format(" /* {0} */", text));
				}
				return true;
			}
			UnityEngine.Debug.Log("GUIDComment " + guid + " [no filename]");
			return false;
		}

		private char NextToken()
		{
			this.SkipWhitespaces();
			return this.StepForeward(1);
		}

		private string Peek(int step = 1)
		{
			string text = string.Empty;
			for (int i = 1; i <= step; i++)
			{
				if (this.data.Length - 1 < this.index + i)
				{
					break;
				}
				text += this.data[this.index + i];
			}
			return text;
		}

		private bool SkipWhitespaces()
		{
			bool result = false;
			while (Regex.IsMatch(this.StepForeward(1).ToString(), "\\s"))
			{
				result = true;
			}
			this.StepBackward(1);
			if (this.SkipComments())
			{
				result = true;
				this.SkipWhitespaces();
			}
			return result;
		}

		private bool SkipComments()
		{
			string arg = string.Empty;
			string text = this.Peek(2);
			if (text != null)
			{
				if (!(text == "/*"))
				{
					if (!(text == "//"))
					{
						return false;
					}
					while (!Regex.IsMatch(this.StepForeward(1).ToString(), "\\n"))
					{
					}
				}
				else
				{
					while (this.Peek(2).CompareTo("*/") != 0)
					{
						arg += this.StepForeward(1);
					}
					arg += this.StepForeward(2);
				}
				return true;
			}
			return false;
		}

		private char StepForeward(int step = 1)
		{
			this.index = Math.Min(this.data.Length, this.index + step);
			return this.data[this.index];
		}

		private char StepBackward(int step = 1)
		{
			this.index = Math.Max(0, this.index - step);
			return this.data[this.index];
		}

		private object ParseValue()
		{
			char c = this.NextToken();
			if (c == '\u001a')
			{
				UnityEngine.Debug.Log("End of file");
				return null;
			}
			if (c == '"')
			{
				return this.ParseString();
			}
			if (c == '(')
			{
				return this.ParseArray();
			}
			if (c != '{')
			{
				this.StepBackward(1);
				return this.ParseEntity();
			}
			return this.ParseDictionary();
		}

		private PBXDictionary ParseDictionary()
		{
			this.SkipWhitespaces();
			PBXDictionary pbxdictionary = new PBXDictionary();
			string key = string.Empty;
			bool flag = false;
			while (!flag)
			{
				char c = this.NextToken();
				switch (c)
				{
				case ';':
					key = string.Empty;
					break;
				default:
					if (c != '\u001a')
					{
						if (c != '}')
						{
							this.StepBackward(1);
							key = (this.ParseValue() as string);
						}
						else
						{
							key = string.Empty;
							flag = true;
						}
					}
					else
					{
						UnityEngine.Debug.Log("Error: reached end of file inside a dictionary: " + this.index);
						flag = true;
					}
					break;
				case '=':
				{
					object value = this.ParseValue();
					if (!pbxdictionary.ContainsKey(key))
					{
						pbxdictionary.Add(key, value);
					}
					break;
				}
				}
			}
			return pbxdictionary;
		}

		private PBXList ParseArray()
		{
			PBXList pbxlist = new PBXList();
			bool flag = false;
			while (!flag)
			{
				char c = this.NextToken();
				switch (c)
				{
				case ')':
					flag = true;
					break;
				default:
					if (c != '\u001a')
					{
						this.StepBackward(1);
						pbxlist.Add(this.ParseValue());
					}
					else
					{
						UnityEngine.Debug.Log("Error: Reached end of file inside a list: " + pbxlist);
						flag = true;
					}
					break;
				case ',':
					break;
				}
			}
			return pbxlist;
		}

		private object ParseString()
		{
			string text = string.Empty;
			for (char c = this.StepForeward(1); c != '"'; c = this.StepForeward(1))
			{
				text += c;
				if (c == '\\')
				{
					text += this.StepForeward(1);
				}
			}
			return text;
		}

		private object ParseEntity()
		{
			string text = string.Empty;
			while (!Regex.IsMatch(this.Peek(1), "[;,\\s=]"))
			{
				text += this.StepForeward(1);
			}
			if (text.Length != 24 && Regex.IsMatch(text, "^\\d+$"))
			{
				return int.Parse(text);
			}
			return text;
		}

		private bool SerializeValue(object value, StringBuilder builder, bool readable = false, int indent = 0)
		{
			if (value == null)
			{
				builder.Append("null");
			}
			else if (value is PBXObject)
			{
				this.SerializeDictionary(((PBXObject)value).data, builder, readable, indent);
			}
			else if (value is Dictionary<string, object>)
			{
				this.SerializeDictionary((Dictionary<string, object>)value, builder, readable, indent);
			}
			else if (value.GetType().IsArray)
			{
				this.SerializeArray(new ArrayList((ICollection)value), builder, readable, indent);
			}
			else if (value is ArrayList)
			{
				this.SerializeArray((ArrayList)value, builder, readable, indent);
			}
			else if (value is string)
			{
				this.SerializeString((string)value, builder, false, readable);
			}
			else if (value is char)
			{
				this.SerializeString(Convert.ToString((char)value), builder, false, readable);
			}
			else if (value is bool)
			{
				builder.Append(Convert.ToInt32(value).ToString());
			}
			else
			{
				if (!value.GetType().IsPrimitive)
				{
					UnityEngine.Debug.LogWarning("Error: unknown object of type " + value.GetType().Name);
					return false;
				}
				builder.Append(Convert.ToString(value));
			}
			return true;
		}

		private bool SerializeDictionary(Dictionary<string, object> dictionary, StringBuilder builder, bool readable = false, int indent = 0)
		{
			builder.Append('{');
			if (readable)
			{
				this.Endline(builder, false);
			}
			foreach (KeyValuePair<string, object> keyValuePair in dictionary)
			{
				if (readable && indent == 1)
				{
					this.MarkSection(builder, keyValuePair.Value.GetType().Name);
				}
				if (readable)
				{
					this.Indent(builder, indent + 1);
				}
				this.SerializeString(keyValuePair.Key, builder, false, readable);
				builder.Append(string.Format(" {0} ", '='));
				this.SerializeValue(keyValuePair.Value, builder, readable && keyValuePair.Value.GetType() != typeof(PBXBuildFile) && keyValuePair.Value.GetType() != typeof(PBXFileReference), indent + 1);
				builder.Append(';');
				this.Endline(builder, !readable);
			}
			if (readable && indent == 1)
			{
				this.MarkSection(builder, null);
			}
			if (readable)
			{
				this.Indent(builder, indent);
			}
			builder.Append('}');
			return true;
		}

		private bool SerializeArray(ArrayList anArray, StringBuilder builder, bool readable = false, int indent = 0)
		{
			builder.Append('(');
			if (readable)
			{
				this.Endline(builder, false);
			}
			for (int i = 0; i < anArray.Count; i++)
			{
				object value = anArray[i];
				if (readable)
				{
					this.Indent(builder, indent + 1);
				}
				if (!this.SerializeValue(value, builder, readable, indent + 1))
				{
					return false;
				}
				builder.Append(',');
				this.Endline(builder, !readable);
			}
			if (readable)
			{
				this.Indent(builder, indent);
			}
			builder.Append(')');
			return true;
		}

		private bool SerializeString(string aString, StringBuilder builder, bool useQuotes = false, bool readable = false)
		{
			if (Regex.IsMatch(aString, "^[A-Fa-f0-9]{24}$"))
			{
				builder.Append(aString);
				this.GUIDComment(aString, builder);
				return true;
			}
			if (string.IsNullOrEmpty(aString))
			{
				builder.Append('"');
				builder.Append('"');
				return true;
			}
			if (!Regex.IsMatch(aString, "^[A-Za-z0-9_./-]+$"))
			{
				useQuotes = true;
			}
			if (useQuotes)
			{
				builder.Append('"');
			}
			builder.Append(aString);
			if (useQuotes)
			{
				builder.Append('"');
			}
			return true;
		}

		public const string PBX_HEADER_TOKEN = "// !$*UTF8*$!\n";

		public const char WHITESPACE_SPACE = ' ';

		public const char WHITESPACE_TAB = '\t';

		public const char WHITESPACE_NEWLINE = '\n';

		public const char WHITESPACE_CARRIAGE_RETURN = '\r';

		public const char ARRAY_BEGIN_TOKEN = '(';

		public const char ARRAY_END_TOKEN = ')';

		public const char ARRAY_ITEM_DELIMITER_TOKEN = ',';

		public const char DICTIONARY_BEGIN_TOKEN = '{';

		public const char DICTIONARY_END_TOKEN = '}';

		public const char DICTIONARY_ASSIGN_TOKEN = '=';

		public const char DICTIONARY_ITEM_DELIMITER_TOKEN = ';';

		public const char QUOTEDSTRING_BEGIN_TOKEN = '"';

		public const char QUOTEDSTRING_END_TOKEN = '"';

		public const char QUOTEDSTRING_ESCAPE_TOKEN = '\\';

		public const char END_OF_FILE = '\u001a';

		public const string COMMENT_BEGIN_TOKEN = "/*";

		public const string COMMENT_END_TOKEN = "*/";

		public const string COMMENT_LINE_TOKEN = "//";

		private const int BUILDER_CAPACITY = 20000;

		private char[] data;

		private int index;

		private PBXResolver resolver;

		private string marker;
	}
}
