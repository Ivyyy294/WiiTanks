using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNetworkObject : NetworkObject
{
	public override byte[] GetSerializedData()
	{
		NetworkPackage networkPackage = new NetworkPackage();
		networkPackage.AddValue (new NetworkPackageValue ("Hello World"));
		networkPackage.AddValue (new NetworkPackageValue ("255"));
		return networkPackage.GetSerializedData();
	}
	public override bool DeserializeData (byte[] rawData)
	{
		NetworkPackage networkPackage = new NetworkPackage();
		networkPackage.ReadBytes (rawData);
		return true;
	}
}
