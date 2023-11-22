using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float velocity = 5f;
    [SerializeField] private float damage;
    private Rigidbody _rigidbody;
    public Collider colliderParent;

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
        Physics.IgnoreCollision(this.GetComponent<Collider>(), colliderParent, true);
        _rigidbody.MovePosition(gameObject.transform.position + gameObject.transform.forward * velocity * Time.fixedDeltaTime);
    }

    public void SetOwner(Collider collider)
    {
        colliderParent = collider;
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
         //   Destroy(this.gameObject);
        }
        if (collider.gameObject.CompareTag("Wall"))
        {
            
        }
    }

}
