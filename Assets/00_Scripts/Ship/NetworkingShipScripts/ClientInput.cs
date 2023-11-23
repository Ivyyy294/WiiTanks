using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClientInput : NetworkObject
{
    private PlayerInputHandler inputHandler;
    private PlayerInput playerInput;
    [SerializeField] private GameObject aimIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerInput= GetComponent<PlayerInput>();   
        inputHandler = GetComponent<PlayerInputHandler>();

        if (Owner)
        {
            playerInput.enabled = true;
            inputHandler.enabled = true;
            gameObject.tag = "Player";
            gameObject.layer = 3;
        }

        if (!Owner)
        {
            aimIndicator.SetActive(false);
            gameObject.tag = "EnemyPlayer";
        }
        
    }

    protected override void SetPackageData()
    {
       
    }
}
