using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfigurationManager : NetworkObject
{
	public static PlayerConfigurationManager Me {get; private set;}
	public int PlayerCount { get;set;}

	//Id of the local player
	//Don't get synced!
	//Will be set once after establishing the connection
	public int LocalPlayerId { get; set;}
    
	void Awake()
    {
        if (Me == null)
		{
			Me = this;
			DontDestroyOnLoad (this);
		}
		else
		{
			Destroy (this);
		}
    }

	protected override void SetPackageData()
    {
        networkPackage.AddValue(new NetworkPackageValue(PlayerCount));
    }

    // Update is called once per frame
    void Update()
    {
		//The Host is owner of the PlayerConfigManager
        Owner = NetworkManager.Me.Host;

		//Update player count und clients
		if (!Owner && networkPackage.Count > 0)
			PlayerCount = networkPackage.Value(0).GetInt32();
    }
}
