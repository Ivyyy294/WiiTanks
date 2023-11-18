using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNetworkManager : MonoBehaviour
{
	[SerializeField] bool startHost = false;

    // Start is called before the first frame update
   	private void Start()
	{
		if (startHost)
			NetworkManager.Me.StartHost(23000);
		else
			NetworkManager.Me.StartClient("127.0.0.1", 23000);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
