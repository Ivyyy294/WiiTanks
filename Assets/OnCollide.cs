using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollide : MonoBehaviour
{
    private List<GameObject> listeners = new List<GameObject>();
    //public static Dictionary<GameObject, System.Action> listeners; //thought about making Dict<GameObject, Action> to assign which collision a certain listener needs
    /* public enum CollidedTypeOfObject
     {Enemy, DefaultProjectile, Other} [SerializeField] public Dictionary<GameObject, string> listeners;     */

    public void Subscribe(GameObject newListener)
    {
        listeners.Add(newListener);
    }

    public void Unsubscribe(GameObject listener)
    {
        listeners.Remove(listener);
    }

    private void NotifySubscribers(GameObject collidedObject)
    {
        foreach (GameObject subscriber in listeners)
        {
            subscriber.GetComponent<ICollideEvent>().OnCollideUpdate(collidedObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NotifySubscribers(other.gameObject);
    }

    /*private void OnEnemyEnter() { }*/

}
