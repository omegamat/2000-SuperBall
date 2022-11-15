using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tranpolin : MonoBehaviour
{
    public float tranpolinForce = 600;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision col) 
    {
        if (col.collider.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * tranpolinForce);
            gameObject.transform.DOPunchScale(new Vector3(0.25f,2f,0.25f),0.25f);
        }
    }
}
