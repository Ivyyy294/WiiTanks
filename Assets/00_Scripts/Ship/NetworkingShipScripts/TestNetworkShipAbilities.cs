using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNetworkShipAbilities : NetworkObject
{
    public GameObject projectilePrefab;

    private PlayerInputHandler inputHandler;

    private float timerCooldown, cooldown;

    protected override void SetPackageData()
    {
        networkPackage.AddValue(new NetworkPackageValue(inputHandler.lookInput.x));
        networkPackage.AddValue(new NetworkPackageValue(inputHandler.lookInput.y));
        networkPackage.AddValue(new NetworkPackageValue(inputHandler.shoot));
    }

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();

    }


    // Update is called once per frame
    void Update()
    {
        if (Owner)
        {
            timerCooldown += Time.deltaTime;
            if (timerCooldown > cooldown)
            {
                if (inputHandler.shoot == true)
                {
                    Vector3 projectileDirection = new Vector3(inputHandler.lookInput.x, 0, inputHandler.lookInput.y);
                    Shoot(projectileDirection);
                    timerCooldown = 0;
                }
            }
        }
        else if (networkPackage.Count >= 3)
        {
            bool isShooting = networkPackage.Value(2).GetBool();

            Vector3 projectileDirection = Vector3.zero;
            projectileDirection.x = networkPackage.Value(0).GetFloat();
            projectileDirection.y = 0;
            projectileDirection.z = networkPackage.Value(1).GetFloat();

            timerCooldown += Time.deltaTime;
            if (isShooting && timerCooldown > cooldown)
            {
                Shoot(projectileDirection);
                timerCooldown = 0;
            }
        }
    }

    private void Shoot(Vector3 projectileDirection)
    {
        projectileDirection.Normalize();
        Quaternion projectileAngle = Quaternion.LookRotation(projectileDirection);
        GameObject projectile = Instantiate(projectilePrefab, transform.position, projectileAngle);

    }
}