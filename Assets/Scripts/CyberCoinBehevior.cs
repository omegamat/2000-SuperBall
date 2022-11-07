using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CyberCoinBehevior : MonoBehaviour
{
    bool isActive = true;
    public GameObject obj;
    public ParticleSystem coinEffect;

    private void Start() 
    {
        this.transform.DORotate(Vector3.up * 180, 2f).SetLoops(-1);
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(isActive)
            {
                coinEffect.Play();
                   
                obj.SetActive(false);
                isActive = false; 

                UIManager.instance.ReciveCoins();
                //GameManager.instance.ReciveCoins();
            }
                      
        }
    }
}
