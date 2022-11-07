using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnColliderDoEvent : MonoBehaviour
{
    public UnityEvent onColliderEvent;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject.tag == "Player")
        {
            onColliderEvent.Invoke();
        }
        
    }
}
