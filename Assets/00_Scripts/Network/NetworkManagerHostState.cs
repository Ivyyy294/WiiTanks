using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

class HandleClient
{
	public Socket socket;
	Thread thread;

	public void Run()
	{
		thread = new Thread (RunIntern);
		thread.IsBackground = true;
		thread.Start();
	}

	void RunIntern()
	{
		NetworkPackage container = new NetworkPackage();

		while (socket != null && socket.Connected)
		{
			container.Receive (socket);

			//For each value in NetworkPackage
			for (int i = 0; i < container.Count; ++i)
				NetworkManagerState.SetNetObjectFromValue (container.Value(i));
		}
	}
}


class NetworkManagerHostState : NetworkManagerState
{
	List <Socket> clientList = new List<Socket>();
	Socket clientAcceptSocket = null;
	Thread clientAcceptThread;

	public override bool Start()
	{
		clientAcceptSocket = GetHostSocket();

		//Start accept thread
		clientAcceptThread = new Thread (AcceptClients);
		clientAcceptThread.Start();

		return clientAcceptSocket != null;
	}

	public override void Update()
	{
		NetworkPackage networkPackage = new NetworkPackage();

		//Create combined NetworkPackage of all NetworkObjects
		for (int i = 0; i < NetworkManager.Me.NetworkObjects.Count; ++i)
			networkPackage.AddValue (GetNetObjectAsValue (i, NetworkManager.Me.NetworkObjects[i]));

		//Sent the data of all NetworkObjects to all clients
		foreach (Socket client in clientList)
			networkPackage.Send (client);
	}

	public override void ShutDown()
	{
		//Close accept socket
		CloseSocket (clientAcceptSocket);

		//Close all client sockets
		foreach (Socket client in clientList)
			CloseSocket (client);

		//Wait for Threads to finish
		clientAcceptThread.Join();
	}


	//Private Methods

	Socket GetHostSocket ()
	{
		//Create TCP Socket
		Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			
		//Accepts connection fromm any IP address
		IPAddress iPAddress = IPAddress.Any;

		//Listens to port 23000
		IPEndPoint iPEndPoint = new IPEndPoint (iPAddress, NetworkManager.Me.Port);
		socket.Bind (iPEndPoint);

		//Allows up to 5 incomming connections
		socket.Listen (5);

		return socket;
	}

	//Method of clientAcceptThread
	//Creates a new HandleClient Thread for each Client
	void AcceptClients ()
	{
		while (clientAcceptSocket != null)
		{
			Socket client = clientAcceptSocket.Accept();
			Debug.Log ("Client connected. " + client.ToString()
					+ ", IPEndpoint: " + client.RemoteEndPoint.ToString());

			clientList.Add (client);

			HandleClient handleClient = new HandleClient();
			handleClient.socket = client;
			handleClient.Run();
		}
	}
}

