using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
		if (socket != null && socket.Connected)
		{
			NetworkPackage networkPackage = new NetworkPackage();
			NetworkObject obj = NetworkObject.Head;

			while (obj != null)
			{
				networkPackage.AddValue (new NetworkPackageValue (obj.GetSerializedData()));
				obj = obj.Previous;
			}

			networkPackage.Send (socket);
		}
		//ToDo send data each Tick
	}

	public override void ShutDown()
	{
		CloseSocket (socket);
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

