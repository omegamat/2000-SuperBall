using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using DG.Tweening;

public class UIManager : MonoBehaviour
{
    
    public static UIManager instance{get; private set;} //Singleton

    public Transform SphereMainMenu;
    public Text TimeText;
    public Text VelocityText;
    public Text CoinText;
    public PlayerController player; 
    private float secondsCount;
    private float velocity;

    private int coins = 0;

        private void Awake() 
    {
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }
    private void Start() 
    {
        
    }
    private void Update() 
    {
        secondsCount += Time.deltaTime;
        velocity = player.velocimetro;
        TimeText.text = "Time: " + (int)secondsCount;
        VelocityText.text = "Speed: " + (int)velocity;  
        CoinText.text = "CyberBits: " + coins;  
    }
    public void ReciveCoins()
    {
        coins += 1;
        Debug.Log("coin");
    }
    public void OnQuit()
    {
        Application.Quit();
    }
    public void OnLoadScene(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }
    public void OnLoadNextScene()
    {
        int netxSceneID = SceneManager.GetActiveScene().buildIndex + 1;
        if(SceneManager.GetSceneByBuildIndex(netxSceneID) != null)
        {
            SceneManager.LoadScene(netxSceneID);
        }
        if(SceneManager.GetSceneByBuildIndex(netxSceneID) == null)
        {
            SceneManager.LoadScene(0);
        }
    }
    public static void ResetScene()
    {
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneID);
    }

    public void RotateSphereMainMenu(int _p)
    {
        //Vector3 _rotation = new Vector3(_x,_y,_z);
        switch (_p)
        {
            case 0:
                SphereMainMenu.DORotate(Vector3.zero,0.25f);
            break;
            case 1:
                SphereMainMenu.DORotate(new Vector3(0,-90f,0),0.25f);
            break;
            case 2:
                SphereMainMenu.DORotate(new Vector3(0,90f,0),0.25f);
            break;

        }
        //SphereMainMenu.DORotate(Vector3.zero,0.25f);
    }
}
