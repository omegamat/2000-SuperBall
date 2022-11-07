using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScenePortal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.DORotate(new Vector3(0,10,0),500,RotateMode.WorldAxisAdd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
