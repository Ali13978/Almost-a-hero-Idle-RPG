using System;

public class GK_Invite
{
	public GK_Invite(string inviteData)
	{
		string[] array = inviteData.Split(new char[]
		{
			'|'
		});
		this._Id = array[0];
		this._Sender = GameCenterManager.GetPlayerById(array[1]);
		this._PlayerGroup = Convert.ToInt32(array[2]);
		this._PlayerAttributes = Convert.ToInt32(array[3]);
	}

	public string Id
	{
		get
		{
			return this._Id;
		}
	}

	public GK_Player Sender
	{
		get
		{
			return this._Sender;
		}
	}

	public int PlayerGroup
	{
		get
		{
			return this._PlayerGroup;
		}
	}

	public int PlayerAttributes
	{
		get
		{
			return this._PlayerAttributes;
		}
	}

	private string _Id;

	private GK_Player _Sender;

	private int _PlayerGroup;

	private int _PlayerAttributes;
}
