using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNetworkManager : MonoBehaviour
{
    // Start is called before the first frame update
   	private void Start()
	{
		var networkObjects = NetworkManager.Me.NetworkObjects;

		if (NetworkManager.Me.Host)
		{
			networkObjects[0].Owner = true;
			networkObjects[1].Owner = false;
			NetworkManager.Me.StartHost(23000);
		}
		else
		{
			networkObjects[0].Owner = false;
			networkObjects[1].Owner = true;
			NetworkManager.Me.StartClient("127.0.0.1", 23000);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
