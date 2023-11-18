using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNetworkManager : MonoBehaviour
{
	[SerializeField] bool startHost = false;
    // Start is called before the first frame update
   	private void Start()
	{
		var networkObjects = NetworkManager.Me.NetworkObjects;

		if (startHost)
		{
			networkObjects[1].Owner = true;
			networkObjects[2].Owner = false;
			NetworkManager.Me.StartHost(23000);
		}
		else
		{
			networkObjects[1].Owner = false;
			networkObjects[2].Owner = true;
			NetworkManager.Me.StartClient("127.0.0.1", 23000);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
