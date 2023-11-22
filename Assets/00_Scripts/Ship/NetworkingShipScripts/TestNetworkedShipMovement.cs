using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNetworkedShipMovement : NetworkObject
{

	[SerializeField] private CharacterController controller;
	[SerializeField] private float playerSpeed = 1.0f;
	private PlayerInputHandler inputHandler;


	private void Start()
	{
		controller = GetComponent<CharacterController>();
		inputHandler = GetComponent<PlayerInputHandler>();
	}

	protected override void SetPackageData()
	{
		networkPackage.AddValue(new NetworkPackageValue(controller.transform.position.x));
		networkPackage.AddValue(new NetworkPackageValue(controller.transform.position.y));
		networkPackage.AddValue(new NetworkPackageValue(controller.transform.position.z));
		networkPackage.AddValue(new NetworkPackageValue(controller.transform.eulerAngles.y));
	}

	private void Update()
	{
		if (Owner)
		{
			Vector3 move = new Vector3(inputHandler.movementInput.x, 0, inputHandler.movementInput.y);
			controller.Move(move * Time.deltaTime * playerSpeed);

			if (move != Vector3.zero)
			{

				gameObject.transform.forward = move;
			}
		}
		else if (networkPackage.Count >= 4)
		{
			Vector3 newPos = Vector3.zero;
			newPos.x = networkPackage.Value(0).GetFloat();
			newPos.y = networkPackage.Value(1).GetFloat();
			newPos.z = networkPackage.Value(2).GetFloat();

			Vector3 newRotation = Vector3.zero;
			newRotation.y = networkPackage.Value(3).GetFloat();

			controller.transform.position = newPos;
			controller.transform.eulerAngles = newRotation;
		}
	}
}
