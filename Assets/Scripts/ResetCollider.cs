using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCollider : MonoBehaviour
{
    //if true respaw, if false reset
    public bool respawnOrReset = true;
    private void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.tag == "Player")
        {
            if(respawnOrReset)
                col.gameObject.SendMessage("Respawn");
            if(!respawnOrReset)
                col.gameObject.SendMessage("Reset");

        }   
    }
}
