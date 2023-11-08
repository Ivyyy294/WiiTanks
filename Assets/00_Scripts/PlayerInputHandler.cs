using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.InputSystem.Processors;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 movementInput;
    public Vector2 lookInput;
    public bool shoot;
    public bool special;
    public bool melee;
    public bool dash;

    //menu buttons
    public bool menuOpened;
    public bool cancel;
    PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {   
        lookInput = context.ReadValue<Vector2>();
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        shoot = context.action.triggered;
    }

    public void OnSpecial(InputAction.CallbackContext context)
    {
        special = context.action.triggered;
    }

    public void OnMelee(InputAction.CallbackContext context)
    {
        melee = context.action.triggered;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        dash = context.action.triggered;
    }

    //switching action maps
    public void OnActionMapChange(InputAction.CallbackContext context)
    {
        if (playerInput.currentActionMap.name == "Player")
        {
            SetActionMapActive("UI");
            menuOpened = true;
            return;
        }
        else if (playerInput.currentActionMap.name == "UI")
        {
            SetActionMapActive("Player");
            menuOpened = false;
            return;
        }
    }

    private void SetActionMapActive(string mapName)
    {
        string currActionMap = playerInput.currentActionMap.name;

        playerInput.actions.FindActionMap(currActionMap).Disable();
        playerInput.actions.FindActionMap(mapName).Enable();

        playerInput.SwitchCurrentActionMap(mapName);
    }
}
