using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FallingPlataform : MonoBehaviour
{
    public float fallTime = 5f; 
    public float waitForFall = 1f; 
    private void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(Fall());
            Destroy(gameObject,fallTime + waitForFall);
        }
    }

    IEnumerator Fall()
    {
        gameObject.transform.DOPunchPosition(new Vector3(0.25f,0,0.25f),0.25f,5);
        yield return new WaitForSeconds(waitForFall);
        gameObject.transform.DOScale(new Vector3(0,transform.localScale.y,0), fallTime);
        yield return null;
    }
}
