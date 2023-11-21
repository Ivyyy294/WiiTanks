using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float velocity = 5f;
    [SerializeField] private float damage;
    private Rigidbody _rigidbody;   

    public float Damage
    {
        get { return damage; }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.MovePosition(gameObject.transform.position + gameObject.transform.forward * velocity * Time.fixedDeltaTime);
    }
}
