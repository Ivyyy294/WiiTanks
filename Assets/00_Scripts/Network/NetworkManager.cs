using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
	[SerializeField] int port = 23000;

	//Host + 3 clients = 4  players
	[SerializeField] int maxClients = 3;
	[SerializeField] List <NetworkObject> networkObjects;
	[SerializeField] int tickRate = 30;
	[SerializeField] bool host = false;

	public static NetworkManager Me {get; private set;}
	public int Port { get {return port;} }
	public int MaxClients {get {return maxClients;}}
	public bool Host { get {return isHost;} }
	public List <NetworkObject> NetworkObjects {get {return networkObjects;}}

	private NetworkManagerState managerState;
	private bool isHost = false;
	private float timer;

	//Public Functions
	public bool StartHost (/*port*/)
	{
		isHost = true;
		Debug.Log("Started Host Session");
		managerState = new NetworkManagerHostState();
		return managerState.Start();
	}

	public bool StartClient (string ip /*port*/)
	{
		isHost = false;
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
		if (host)
		{
			networkObjects[0].Owner = true;
			networkObjects[1].Owner = false;
			StartHost();
		}
		else
		{
			networkObjects[0].Owner = false;
			networkObjects[1].Owner = true;
			StartClient("127.0.0.1");
		}
	}

	private void OnDestroy()
	{
		managerState?.ShutDown();
	}

	private void Update()
	{
		if (timer < (1f / tickRate))
			timer += Time.deltaTime;
		else
		{
			timer = 0f;
			managerState?.Update();
		}
	}
}
