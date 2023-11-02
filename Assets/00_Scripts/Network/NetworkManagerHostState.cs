using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

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
	}

	public override void ShutDown()
	{
		CloseSocket (clientAcceptSocket);

		//Wait for Threads to finish
		clientAcceptThread.Join();
	}

	Socket GetHostSocket ()
	{
		//Create TCP Socket
		Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
			
		//Accepts connection fromm any IP address
		IPAddress iPAddress = IPAddress.Any;

		//Listens to port 23000
		IPEndPoint iPEndPoint = new IPEndPoint (iPAddress, NetworkManager.Me.Port);
		socket.Bind (iPEndPoint);

		//Allows up to 5 incomming connections
		socket.Listen (5);

		return socket;
	}

	void AcceptClients ()
	{
		while (clientAcceptSocket != null)
		{
			Socket client = clientAcceptSocket.Accept();
			Debug.Log ("Client connected. " + client.ToString()
					+ ", IPEndpoint: " + client.RemoteEndPoint.ToString());

			clientList.Add (client);
		}
	}
}

