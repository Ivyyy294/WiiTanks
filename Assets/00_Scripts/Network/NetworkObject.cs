using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NetworkObject : MonoBehaviour
{
	public byte[] GetSerializedData()
	{
		//Clear Package
		networkPackage.Clear();

		//Call abstract SetPackageData
		SetPackageData();

		//Return Serialized Data
		return networkPackage.GetSerializedData();
	}
	public bool DeserializeData (byte[] rawData) {return networkPackage.ReadBytes (rawData);}

	protected abstract void SetPackageData();
	protected NetworkPackage networkPackage = new NetworkPackage();
}
