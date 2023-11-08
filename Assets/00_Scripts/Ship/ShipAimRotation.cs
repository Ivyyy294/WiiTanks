using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAimRotation : MonoBehaviour
{
    [SerializeField] private GameObject aimArrow;
    private PlayerInputHandler inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateAimArrow();
    }

    void RotateAimArrow()
    {

        Vector3 lookDirection = new Vector3(inputHandler.lookInput.x, 0, inputHandler.lookInput.y);
        //aimArrow.transform.forward = lookDirection;
    }
}
