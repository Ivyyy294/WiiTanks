using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerConfigurationManager : NetworkObject
{
	public static PlayerConfigurationManager Me {get; private set;}
	public int PlayerCount { get;set;}

	//Id of the local player
	//Don't get synced!
	//Will be set once after establishing the connection
	public int LocalPlayerId { get; set;}

	void Awake()
    {
        if (Me == null)
		{
			Me = this;
			DontDestroyOnLoad (this);
		}
		else
		{
			Destroy (this);
		}
    }

	void Start()
	{
		NetworkManager.Me.onClientConnected = OnClientConnected;
		NetworkManager.Me.onConnectedToHost = OnConnectedToHost;
	}

	protected override void SetPackageData()
    {
        networkPackage.AddValue(new NetworkPackageValue(PlayerCount));
    }

    // Update is called once per frame
    void Update()
    {
		//The Host is owner of the PlayerConfigManager
        if (NetworkManager.Me.Host)
		{
			Owner = true;
			LocalPlayerId = 0;
		}

		//Update player count und clients
		if (!Owner && networkPackage.Count > 0)
			PlayerCount = networkPackage.Value(0).GetInt32();
    }

	void OnClientConnected(int clientNumber, Socket socket)
	{
		//Send client player index to client
		socket.Send (BitConverter.GetBytes (clientNumber));
	}

	void OnConnectedToHost(Socket socket)
	{
		//Get local player index from host
		byte[] buffer = new byte[sizeof (int)];
		int bysteCount = socket.Receive (buffer);
		LocalPlayerId = BitConverter.ToInt32 (buffer, 0);
	}
}
