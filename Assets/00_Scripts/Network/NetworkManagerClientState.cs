using System;
using System.Net;
using System.Net.Sockets;


class NetworkManagerClientState : NetworkManagerState
{
	private string ip;
	Socket socket = null;

	public NetworkManagerClientState (string _ip) {ip = _ip;}

	public override bool Start()
	{
		socket = GetClientSocket (ip);
		return socket != null && socket.Connected;
	}

	public override void Update()
	{

	}

	public override void ShutDown()
	{
		CloseSocket (socket);
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
			clientSocket.Connect (iPAddress, NetworkManager.Me.Port);
			return clientSocket;
		}
		catch (Exception excp)
		{
			return null;
		}
	}
}

