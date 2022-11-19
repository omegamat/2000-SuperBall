using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider col) 
    {
         if (col.gameObject.tag == "Player")
        {
            Debug.Log("reset");
            StartCoroutine(Death());

        }  
    }
    private void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("reset");
            StartCoroutine(Death());

        }  
    }
    
    IEnumerator Death()
    {
        gameObject.GetComponent<AudioSource>().Play();
        
        yield return new WaitForSeconds(2.0f);
        UIManager.ResetScene();
    }
}
