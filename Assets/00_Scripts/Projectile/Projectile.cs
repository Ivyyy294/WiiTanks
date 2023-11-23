using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float velocity = 5f;
    [SerializeField] private float damage;
    [SerializeField] private float lifetime = 4;

    private float lifetimeTimer;
    private Rigidbody _rigidbody;
    private Collider _collider;
    // public Collider colliderParent;

    public float Damage
    {
        get { return damage; }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame

    void Update()
    {
        lifetimeTimer += Time.deltaTime;
        if (lifetime < lifetimeTimer)
        {
            Destroy(this.gameObject);
        }
    }
    void FixedUpdate()
    {
        //  Physics.IgnoreCollision(this.GetComponent<Collider>(), colliderParent, true);
        _rigidbody.MovePosition(gameObject.transform.position +
                                gameObject.transform.forward * velocity * Time.fixedDeltaTime);
        //  transform.Translate(Vector3.forward * velocity * Time.deltaTime);;

    }

    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(_collider, collision.collider, true);
            Debug.Log("player");
            
        }
        
        if (collision.gameObject.CompareTag("EnemyPlayer"))
        {   
            collision.gameObject.GetComponent<PlayerHP>().ReduceHealth(damage);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 reflectionDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            transform.forward = reflectionDirection;
            Debug.Log("hitwall");
        }
        
    }
    /*
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("EnemyPlayer"))
        {
            collider.GetComponent<PlayerHP>().ReduceHealth(damage);
           Destroy(this.gameObject);
        }
        if (collider.gameObject.CompareTag("Wall"))
        {
            Vector3 reflectionDirection = Vector3.Reflect(transform.forward, collider.transform.up);
            transform.forward = reflectionDirection;
            transform.forward = new Vector3(0, 0, -1);
            Debug.Log("hitwall");
        }
        
    }
*/
}
