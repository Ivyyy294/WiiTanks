using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICollideEvent
{
    void OnCollideUpdate(GameObject collidedObject);
}
