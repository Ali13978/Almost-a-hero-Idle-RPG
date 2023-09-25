using System;
using System.Globalization;
using PlayFab.Internal;

namespace PlayFab.Json
{
	public class SimpleJsonInstance : ISerializerPlugin, IPlayFabPlugin
	{
		public T DeserializeObject<T>(string json)
		{
			return PlayFabSimpleJson.DeserializeObject<T>(json, SimpleJsonInstance.ApiSerializerStrategy);
		}

		public T DeserializeObject<T>(string json, object jsonSerializerStrategy)
		{
			return PlayFabSimpleJson.DeserializeObject<T>(json, (IJsonSerializerStrategy)jsonSerializerStrategy);
		}

		public object DeserializeObject(string json)
		{
			return PlayFabSimpleJson.DeserializeObject(json, typeof(object), SimpleJsonInstance.ApiSerializerStrategy);
		}

		public string SerializeObject(object json)
		{
			return PlayFabSimpleJson.SerializeObject(json, SimpleJsonInstance.ApiSerializerStrategy);
		}

		public string SerializeObject(object json, object jsonSerializerStrategy)
		{
			return PlayFabSimpleJson.SerializeObject(json, (IJsonSerializerStrategy)jsonSerializerStrategy);
		}

		public static SimpleJsonInstance.PlayFabSimpleJsonCuztomization ApiSerializerStrategy = new SimpleJsonInstance.PlayFabSimpleJsonCuztomization();

		public class PlayFabSimpleJsonCuztomization : PocoJsonSerializerStrategy
		{
			public override object DeserializeObject(object value, Type type)
			{
				string text = value as string;
				if (text == null)
				{
					return base.DeserializeObject(value, type);
				}
				Type underlyingType = Nullable.GetUnderlyingType(type);
				if (underlyingType != null)
				{
					return this.DeserializeObject(value, underlyingType);
				}
				if (type.GetTypeInfo().IsEnum)
				{
					return Enum.Parse(type, (string)value, true);
				}
				double value2;
				if (type == typeof(DateTime))
				{
					DateTime dateTime;
					bool flag = DateTime.TryParseExact(text, PlayFabUtil._defaultDateTimeFormats, CultureInfo.InvariantCulture, PlayFabUtil.DateTimeStyles, out dateTime);
					if (flag)
					{
						return dateTime;
					}
				}
				else if (type == typeof(DateTimeOffset))
				{
					DateTimeOffset dateTimeOffset;
					bool flag2 = DateTimeOffset.TryParseExact(text, PlayFabUtil._defaultDateTimeFormats, CultureInfo.InvariantCulture, PlayFabUtil.DateTimeStyles, out dateTimeOffset);
					if (flag2)
					{
						return dateTimeOffset;
					}
				}
				else if (type == typeof(TimeSpan) && double.TryParse(text, out value2))
				{
					return TimeSpan.FromSeconds(value2);
				}
				return base.DeserializeObject(value, type);
			}

			protected override bool TrySerializeKnownTypes(object input, out object output)
			{
				if (input.GetType().GetTypeInfo().IsEnum)
				{
					output = input.ToString();
					return true;
				}
				if (input is DateTime)
				{
					output = ((DateTime)input).ToString(PlayFabUtil._defaultDateTimeFormats[2], CultureInfo.InvariantCulture);
					return true;
				}
				if (input is DateTimeOffset)
				{
					output = ((DateTimeOffset)input).ToString(PlayFabUtil._defaultDateTimeFormats[2], CultureInfo.InvariantCulture);
					return true;
				}
				if (input is TimeSpan)
				{
					output = ((TimeSpan)input).TotalSeconds;
					return true;
				}
				return base.TrySerializeKnownTypes(input, out output);
			}
		}
	}
}
