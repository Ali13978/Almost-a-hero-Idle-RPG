using System;

public class ISN_Device
{
	public ISN_Device()
	{
	}

	public ISN_Device(string deviceData)
	{
		string[] array = deviceData.Split(new char[]
		{
			'|'
		});
		this._Name = array[0];
		this._SystemName = array[1];
		this._Model = array[2];
		this._LocalizedModel = array[3];
		this._SystemVersion = array[4];
		this._MajorSystemVersion = Convert.ToInt32(array[5]);
		this._InterfaceIdiom = (ISN_InterfaceIdiom)Convert.ToInt32(array[6]);
		this._GUID = new ISN_DeviceGUID(array[7]);
		this._PreferredLanguage_ISO639_1 = array[8];
	}

	public string Name
	{
		get
		{
			return this._Name;
		}
	}

	public string SystemName
	{
		get
		{
			return this._SystemName;
		}
	}

	public string Model
	{
		get
		{
			return this._Model;
		}
	}

	public string LocalizedModel
	{
		get
		{
			return this._LocalizedModel;
		}
	}

	public string SystemVersion
	{
		get
		{
			return this._SystemVersion;
		}
	}

	public int MajorSystemVersion
	{
		get
		{
			return this._MajorSystemVersion;
		}
	}

	public ISN_InterfaceIdiom InterfaceIdiom
	{
		get
		{
			return this._InterfaceIdiom;
		}
	}

	public ISN_DeviceGUID GUID
	{
		get
		{
			return this._GUID;
		}
	}

	public string PreferredLanguageCode
	{
		get
		{
			return this.PreferredLanguage_ISO639_1.Substring(0, 2);
		}
	}

	public string PreferredLanguage_ISO639_1
	{
		get
		{
			return this._PreferredLanguage_ISO639_1;
		}
	}

	public string AdvertisingIdentifier
	{
		get
		{
			return "00000000-0000-0000-0000-000000000000";
		}
	}

	public bool AdvertisingTrackingEnabled
	{
		get
		{
			return false;
		}
	}

	public static ISN_Device CurrentDevice
	{
		get
		{
			if (ISN_Device._CurrentDevice == null)
			{
				ISN_Device._CurrentDevice = new ISN_Device();
			}
			return ISN_Device._CurrentDevice;
		}
	}

	private static ISN_Device _CurrentDevice;

	private string _Name = "Test Name";

	private string _SystemName = "iPhone OS";

	private string _Model = "iPhone";

	private string _LocalizedModel = "iPhone";

	private string _SystemVersion = "9.0.0";

	private int _MajorSystemVersion = 9;

	private string _PreferredLanguage_ISO639_1 = "en-US";

	private ISN_InterfaceIdiom _InterfaceIdiom;

	private ISN_DeviceGUID _GUID = new ISN_DeviceGUID(string.Empty);
}
