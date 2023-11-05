using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNetworkObject : NetworkObject
{
	[SerializeField] float speed = 5f;
	Rigidbody rigidbody;

	private void Start()
	{
		rigidbody = GetComponent <Rigidbody>();
		rigidbody.isKinematic = !Owner;
	}

	protected override void SetPackageData()
	{
		networkPackage.AddValue (new NetworkPackageValue (rigidbody.position.x));
		networkPackage.AddValue (new NetworkPackageValue (rigidbody.position.y));
		networkPackage.AddValue (new NetworkPackageValue (rigidbody.position.z));
	}

	private void Update()
	{
		if (Owner)
		{
			Vector3 input = Vector3.zero;
			input += Vector3.up * Input.GetAxis ("Vertical");
			input += Vector3.right * Input.GetAxis ("Horizontal");
			rigidbody.MovePosition (rigidbody.position + input.normalized * speed * Time.deltaTime);
		}
		else if (networkPackage.Count >= 3)
		{
			Vector3 newPos = Vector3.zero;
			newPos.x = networkPackage.Value(0).GetFloat();
			newPos.y = networkPackage.Value(1).GetFloat();
			newPos.z = networkPackage.Value(2).GetFloat();
			rigidbody.position = newPos;
		}
	}
}
