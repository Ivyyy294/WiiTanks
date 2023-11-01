using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System;

public class NetworkManager : MonoBehaviour
{
	[SerializeField] int port = 23000;

	public NetworkManager Me {get; private set;}
	public bool Host {get; private set;}

	private Socket socket = null;

	//Public Functions
	public bool StartHost ()
	{
		Host = true;
		socket = GetHostSocket ();

		//ToDo: Start Client accepting thread

		return socket != null;
	}

	public bool StartClient (string ip)
	{
		//Create TCP Socket
		socket = GetClientSocket(ip);

		//ToDo: Start listener thread

		return socket != null && socket.Connected;
	}

	//Private Functions
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
		CloseSocket();
	}

	Socket GetHostSocket ()
	{
		//Create TCP Socket
		Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
			
		//Accepts connection fromm any IP address
		IPAddress iPAddress = IPAddress.Any;

		//Listens to port 23000
		IPEndPoint iPEndPoint = new IPEndPoint (iPAddress, port);
		listenerSocket.Bind (iPEndPoint);

		//Allows up to 5 incomming connections
		listenerSocket.Listen (5);

		return listenerSocket;
	}

	Socket GetClientSocket (string ip)
	{
		//Create TCP Socket
		Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);

		//Connect with Host
		IPAddress iPAddress = null;
		try
		{
			//Cast input to IPAddress
			iPAddress = IPAddress.Parse (ip);
			clientSocket.Connect (iPAddress, port);
			return clientSocket;
		}
		catch (Exception excp)
		{
			return null;
		}
	}

	void CloseSocket()
	{
		if (socket != null)
		{
			if (socket.Connected) 
				socket.Shutdown(SocketShutdown.Both);

			socket.Close();
			socket.Dispose();
		}
	}
}
