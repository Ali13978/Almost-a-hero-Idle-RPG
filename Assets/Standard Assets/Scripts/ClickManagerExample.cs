using System;
using UnityEngine;

public class ClickManagerExample : MonoBehaviour
{
	private void Awake()
	{
		GameCenter_RTM.ActionDataReceived += this.HandleActionDataReceived;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			pos.z = 1f;
			PTPGameController.instance.createGreenSphere(pos);
			ObjectCreatePackage objectCreatePackage = new ObjectCreatePackage(pos.x, pos.y);
			objectCreatePackage.send();
		}
	}

	private void HandleActionDataReceived(GK_Player player, byte[] data)
	{
		ByteBuffer byteBuffer = new ByteBuffer(data);
		int num = byteBuffer.readInt();
		if (num != 1)
		{
			ISN_Logger.Log("Got pack wit id: " + num, LogType.Log);
		}
		else
		{
			ISN_Logger.Log("Sphere pack", LogType.Log);
			Vector3 pos = new Vector3(0f, 0f, 1f);
			pos.x = byteBuffer.readFloat();
			pos.y = byteBuffer.readFloat();
			PTPGameController.instance.createRedSphere(pos);
		}
	}
}
