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
	public bool Host { get {return host;} }
	public List <NetworkObject> NetworkObjects {get {return networkObjects;}}

	private NetworkManagerState managerState;
	private float timer = 0f;

	//Public Functions
	public bool StartHost (int _port)
	{
		if (managerState == null)
		{
			port = _port;
			host = true;
			Debug.Log("Started Host Session");
			managerState = new NetworkManagerHostState();
			InitNetworkObjects();
			return managerState.Start();
		}
		else
			return false;
	}

	public bool StartClient (string ip, int _port)
	{
		if (managerState == null)
		{
			port = _port;
			host = false;
			Debug.Log("Started Client Session");
			managerState = new NetworkManagerClientState(ip);
			InitNetworkObjects();
			return managerState.Start();
		}
		else
			return false;
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
			managerState?.Update();
			timer = 0f;
		}
	}

	//Sets the Rigidbody of all not owned NetworkObject to kinematic
	private void InitNetworkObjects()
	{
		foreach (NetworkObject i in networkObjects)
		{
			if (!i.Owner)
			{
				Rigidbody rb = i.GetComponent<Rigidbody>();

				if (rb != null)
					rb.isKinematic = true;
			}
		}
	}
}
