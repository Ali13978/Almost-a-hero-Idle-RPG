using System;
using SA.Common.Pattern;

public class NetworkManagerExample
{
	public static void send(BasePackage pack)
	{
		Singleton<GameCenter_RTM>.Instance.SendDataToAll(pack.getBytes(), GK_MatchSendDataMode.RELIABLE);
	}
}
