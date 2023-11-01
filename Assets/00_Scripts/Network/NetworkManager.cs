using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System;
using System.Threading;

public class NetworkManager : MonoBehaviour
{
	[SerializeField] int port = 23000;

	public static NetworkManager Me {get; private set;}
	public int Port { get {return port;} }

	private NetworkManagerState managerState;

	//Public Functions
	public bool StartHost ()
	{
		managerState = new NetworkManagerHostState();
		return managerState.Start();
	}

	public bool StartClient (string ip)
	{
		managerState = new NetworkManagerClientState(ip);
		return managerState.Start();
	}

	private void Awake()
	{
		if (Me == null)
		{
			Me = this;
			DontDestroyOnLoad (this);
		}
		else
			Destroy (this);
	}

	private void OnDestroy()
	{
		managerState?.ShutDown();
	}

	private void Update()
	{
		managerState?.Update();
	}
}
