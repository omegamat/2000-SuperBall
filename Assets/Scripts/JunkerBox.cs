using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JunkerBox : MonoBehaviour
{
    public static JunkerBox instance{get; private set;}
    // Start is called before the first frame update
    void Awake() 
    {  
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);   
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


}
