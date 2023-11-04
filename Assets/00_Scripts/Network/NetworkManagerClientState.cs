using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

class NetworkManagerClientState : NetworkManagerState
{
	private string ip;
	Socket socket = null;
	Thread listenerThread;

	public NetworkManagerClientState (string _ip) {ip = _ip;}
	~NetworkManagerClientState()
	{
		ShutDown();
	}

	public override bool Start()
	{
		//Create client Socket
		socket = GetClientSocket (ip);

		bool ok = socket != null && socket.Connected;

		if (ok)
		{
			//Start listener thread
			listenerThread = new Thread (ReceiveData);
			listenerThread.IsBackground = true;
			listenerThread.Start();
		}

		return ok;
	}

	public override void Update()
	{
		if (socket != null && socket.Connected)
			SendData();
	}

	public override void ShutDown()
	{
		CloseSocket (socket);
		listenerThread.Join();
	}

	void SendData()
	{
		NetworkPackage networkPackage = new NetworkPackage();

		foreach (NetworkObject i in NetworkManager.Me.NetworkObjects)
			networkPackage.AddValue (new NetworkPackageValue (i.GetSerializedData()));

		networkPackage.Send (socket);
	}

	void ReceiveData()
	{
		NetworkPackage networkPackage = new NetworkPackage();

		while (socket != null && socket.Connected)
		{
			networkPackage.Receive (socket);

			//For each NetworkObject in NetworkManager
			for (int i = 0; i < NetworkManager.Me.NetworkObjects.Count; ++i)
			{
				if (i < networkPackage.Count)
					NetworkManager.Me.NetworkObjects[i].DeserializeData (networkPackage.Value(i).GetBytes());
			}
		}
	}

	Socket GetClientSocket (string ip)
	{
		//Create TCP Socket
		Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

		//Connect with Host
		IPAddress iPAddress = null;
		try
		{
			//Cast input to IPAddress
			iPAddress = IPAddress.Parse (ip);
			clientSocket.Connect (iPAddress, NetworkManager.Me.Port);
			return clientSocket;
		}
		catch (Exception excp)
		{
			return null;
		}
	}

}

