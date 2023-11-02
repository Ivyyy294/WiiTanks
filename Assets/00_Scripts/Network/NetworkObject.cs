using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NetworkObject : MonoBehaviour
{
	//Public
	public NetworkObject()
	{
		Previous = Head;
		Head = this;
	}

	~NetworkObject()
	{
		if (Head == this)
			Head = Previous;
		else
		{
			//Find first Object after this
			NetworkObject tmp = Head;

			while (tmp.Previous != this)
				tmp = tmp.Previous;

			tmp.Previous = Previous;
		}
	}

	public abstract byte[] GetSerializedData();
	public abstract bool DeserializeData (byte[] rawData);

	//Reference to the last NetworkObject
	public static NetworkObject Head {protected set; get;}

	//Reference to the previous NetworkObject
	public NetworkObject Previous {get; protected set;}

	//public bool SyncToServer {get; protected set;}
}
