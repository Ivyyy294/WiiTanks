using System.Net;
using System.Net.Sockets;


class NetworkManagerHostState : NetworkManagerState
{
	Socket listenerSocket = null;

	public override bool Start()
	{
		listenerSocket = GetHostSocket();

		//ToDo: Start accept thread

		return listenerSocket != null;
	}
	public override void Update()
	{

	}
	public override void ShutDown()
	{
		CloseSocket (listenerSocket);
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
		while (listenerSocket != null)
		{
			Socket clientSocket = listenerSocket.Accept();
		}
	}
}

