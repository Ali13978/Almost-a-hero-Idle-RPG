using System;
using UnityEngine;

namespace FlyingWormConsole3
{
	public class ConsoleProRemoteServer : MonoBehaviour
	{
		public void Awake()
		{
			UnityEngine.Debug.Log("Console Pro Remote Server is disabled in release mode, please use a Development build or define DEBUG to use it");
		}

		public bool useNATPunch;

		public int port = 51000;
	}
}
