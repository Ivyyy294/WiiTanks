using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float velocity = 5f; 


    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().MovePosition(gameObject.transform.position + gameObject.transform.forward * velocity * Time.fixedDeltaTime);
    }
}
