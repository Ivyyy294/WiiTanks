using UnityEngine;

public class NetworkManager : MonoBehaviour
{
	[SerializeField] int port = 23000;

	//Host + 3 clients = 4  players
	[SerializeField] int maxClients = 3;

	public static NetworkManager Me {get; private set;}
	public int Port { get {return port;} }
	public int MaxClients {get {return maxClients;}}

	private NetworkManagerState managerState;

	//Public Functions
	public bool StartHost ()
	{
		Debug.Log("Started Host Session");
		managerState = new NetworkManagerHostState();
		return managerState.Start();
	}

	public bool StartClient (string ip)
	{
		Debug.Log("Started Client Session");
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

	private void Start()
	{
		StartClient("127.0.0.1");
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
