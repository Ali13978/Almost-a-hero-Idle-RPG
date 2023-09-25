using System;
using UnityEngine;

public class TvOSInAppsExample : MonoBehaviour
{
	public void Init()
	{
		UnityEngine.Debug.Log("Init");
		PaymentManagerExample.init();
	}

	public void Buy()
	{
		PaymentManagerExample.buyItem("your.product.id1.here");
	}
}
