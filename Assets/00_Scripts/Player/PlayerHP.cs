using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour, ICollideEvent
{
    public float health;
    public Healthbar healthbarUI, healthbarWorldspace;
    [SerializeField] private float maxHealth;

    public float Health
    {
        get { return health; }
    }

    void ICollideEvent.OnCollideUpdate(GameObject collidedObject)
    {
        if (collidedObject.GetComponent<Projectile>() == null)
            return;
        else
            ReduceHealth(collidedObject.GetComponent<Projectile>().Damage);
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthbarUI.SetMaxValue(health);
        healthbarWorldspace.SetMaxValue(health);
        gameObject.GetComponent<OnCollide>()?.Subscribe(gameObject);
    }

    private void Update()
    {
        if (health > 0 && health < maxHealth)
            return;
        else if (health <= 0)
        {
            health = 0;
            Die();
        }
        else if (health > maxHealth)
            health = maxHealth;
        
    }

    public void ReduceHealth(float amount)
    {
        health -= amount;
        SetAllHealthbarsValue(health);
    }

    public void IncreaseHealth(float amount)
    {
        health += amount;
        SetAllHealthbarsValue(health);
    }

    public void Die()
    {
        print("you are DEAD");
    }

    void SetAllHealthbarsValue(float amount)
    {
        healthbarUI.SetHealth(health);
        healthbarWorldspace.SetHealth(health);
    }

}
