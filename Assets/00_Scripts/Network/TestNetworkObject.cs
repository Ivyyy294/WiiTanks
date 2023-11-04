using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNetworkObject : NetworkObject
{
	protected override void SetPackageData()
	{
		networkPackage.AddValue (new NetworkPackageValue (transform.position.x));
		networkPackage.AddValue (new NetworkPackageValue (transform.position.y));
		networkPackage.AddValue (new NetworkPackageValue (transform.position.z));
	}

	private void Update()
	{
		if (networkPackage.Count >= 3)
		{
			Vector3 newPos = Vector3.zero;
			newPos.x = networkPackage.Value(0).GetFloat();
			newPos.y = networkPackage.Value(1).GetFloat();
			newPos.z = networkPackage.Value(2).GetFloat();
			transform.position = newPos;
		}
	}
}
