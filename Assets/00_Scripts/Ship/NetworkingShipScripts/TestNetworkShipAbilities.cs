using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNetworkShipAbilities : NetworkObject
{
    // public GameObject projectilePrefab;

    private PlayerInputHandler inputHandler;
    private Collider _collider;
    [SerializeField] private Projectile projectile;
    [SerializeField] private GameObject aimArrow;
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private float cooldown = 1;
    private float timerCooldown;
        

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
        _collider = GetComponent<Collider>();

    }


    // Update is called once per frame
    void Update()
    {
        if (Owner)
        {
            Vector3 lookDirection = new Vector3(inputHandler.lookInput.x, 0, inputHandler.lookInput.y);
            aimArrow.transform.forward = lookDirection;

            timerCooldown += Time.deltaTime;
            if (timerCooldown > cooldown)
            {
                if (inputHandler.shoot == true)
                {
                  
                    Shoot(lookDirection);
                    timerCooldown = 0;
                }
            }
        }
        else if (networkPackage.Count >= 3)
        {
            bool isShooting = networkPackage.Value(2).GetBool();

            Vector3 lookDirection = Vector3.zero;
            lookDirection.x = networkPackage.Value(0).GetFloat();
            lookDirection.y = 0;
            lookDirection.z = networkPackage.Value(1).GetFloat();

            aimArrow.transform.forward = lookDirection;

            timerCooldown += Time.deltaTime;
            if (isShooting && timerCooldown > cooldown)
            {
                Shoot(lookDirection);
                timerCooldown = 0;
            }
        }
    }

    private void Shoot(Vector3 lookDirection)
    {
        lookDirection.Normalize();
        Quaternion projectileAngle = Quaternion.LookRotation(lookDirection);
        Projectile projectileCopy = Instantiate(projectile, projectileSpawnPosition.position, projectileAngle);
            //  projectileCopy.SetOwner(_collider);
            //   projectileCopy.colliderParent = _collider;
       
    }

  
}