using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAbilities : MonoBehaviour
{

    private PlayerInputHandler inputHandler;
   // public testProjectile temporaryProjectile;

    public float cooldown;
    private float timerCooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        // add cooldown via manager
        timerCooldown += Time.deltaTime;
        if(timerCooldown > cooldown)
        {
            if (inputHandler.shoot == true)
            {
                /*
                Vector3 projectileDirection = new Vector3(inputHandler.lookInput.x, 0, inputHandler.lookInput.y);
                projectileDirection.Normalize();
                Quaternion projectileAngle = Quaternion.LookRotation(projectileDirection);

               testProjectile projectile = Instantiate(temporaryProjectile, transform.position, projectileAngle);
               projectile.projectileDirection = projectileDirection;

                timerCooldown = 0;
                */
            }
        }
       
    }
}
