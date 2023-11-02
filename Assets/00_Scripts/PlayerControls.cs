using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.InputSystem.Processors;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;
    public GameObject aimArrow;

    /*
    private Vector2 movementInput;
    private Vector2 lookInput;
    private bool shoot;
    private bool special;
    private bool melee;
    private bool dash;
    */

    [SerializeField] private float playerSpeed = 1.0f;
    private PlayerInputHandler inputHandler;

    private enum State
    {
        MoveState,
        ShootState,
        SpecialState,        
        MeleeState,
        DashState,
    }

    //animations
    private bool animRun;
    private bool animShoot;

    private float stateCountdown;
    private int currentState;

    /*

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
    */
    // Start is called before the first frame update
    void Start()
    {
       inputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {

        if (inputHandler.dash) { Dash(); }
     
        if (inputHandler.melee) { }

        Movement();
        Look();

        int LockState(int s, float t)
        {
            stateCountdown = Time.time + t;
            return s;
        }

        Debug.Log(inputHandler.movementInput);
        Debug.Log(inputHandler.shoot);
    }

    void GetState()
    {
       
    }

    void Dash()
    {

    }

    void Look()
    {
        Vector3 lookDirection = new Vector3(inputHandler.lookInput.x, 0, inputHandler.lookInput.y);
        aimArrow.transform.forward = lookDirection;  
    }

    void Movement()
    {
        Vector3 move = new Vector3(inputHandler.movementInput.x, 0, inputHandler.movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {

            gameObject.transform.forward = move;
            animRun = true;
            animator.SetBool("animRun", true);

        }

        else
        {
            animRun = false;
            animator.SetBool("animRun", false);
        }

        
    }

    void Shoot()
    {

    }

}
