using System.Net.Sockets;

public abstract class NetworkManagerState
{
	public abstract bool Start();
	public abstract void Update();
	public abstract void ShutDown();

	protected void CloseSocket (Socket socket)
	{
		if (socket != null)
		{
			if (socket.Connected) 
				socket.Shutdown(SocketShutdown.Both);

			socket.Close();
			socket.Dispose();
			socket = null;
		}
	}
}
