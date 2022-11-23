using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

using DG.Tweening;

public class UIManager : MonoBehaviour
{
    
    public static UIManager instance{get; private set;} //Singleton

    //PlayerUI
    public Text TimeText;
    public Text VelocityText;
    public Image VelocityImage;
    public Color[] VelocityColor = new Color[3];
    public Text CoinText;
    public PlayerController player; 
    private float timer;
    private float velocity;

    //ScoreScreen
    public Transform scoreScreen;
    private Text sc_cointext;
    private Text sc_Timetext;
    private TMP_Text sc_Ranktext;
    private Image playerUI_velocimetro;

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

        sc_Timetext = scoreScreen.GetChild(0).gameObject.GetComponent<Text>();
        sc_cointext = scoreScreen.GetChild(1).gameObject.GetComponent<Text>();
        sc_Ranktext = scoreScreen.GetChild(2).GetChild(0).gameObject.GetComponent<TMP_Text>(); 
         
    }
    private void Update() 
    {
        timer += Time.smoothDeltaTime;

        SettingUI();
        //SettingScoreScreen();

        if(Input.GetButton("Return"))
        {
            ResetScene();
        }
    }

    private void SettingUI()
    {
        //secondsCount += Time.deltaTime;
        //float milliSeconds = 0;
        //milliSeconds += (secondsCount * 100)% 100;

        velocity = player.velocimetro;
        //TimeText.text = "" + (int)secondsCount + "." + milliSeconds;
        TimeText.text = CountUpTimer();
        VelocityText.text = "" + (int)velocity;  
        CoinText.text = "" + coins;
        SettingVelocimetro();
    }

    string CountUpTimer()
    {       
        float _minutes = 0;
        float _seconds = 0;
        float _milliSeconds = 0;
        string output;

        

        _minutes = timer / 60;
        _seconds = timer % 60;
        _milliSeconds += (timer * 100) % 100;

        output = string.Format("{0}:{1}:{2}", (int)_minutes, (int)_seconds, (int)_milliSeconds);
        //output = ""+ timer;
        //Debug.Log ("timer" + output);

        return output;

        
    }
    private void SettingVelocimetro()
    {
        float _maxVel = 30;
        VelocityImage.fillAmount = Mathf.Clamp((player.velocimetro / _maxVel)*0.3f,0,0.3f);

        if (player.velocimetro > _maxVel*0.3f)
        VelocityImage.color = VelocityColor[0];
        if (player.velocimetro > _maxVel*0.6f)
            VelocityImage.color = VelocityColor[1];
        if (player.velocimetro > _maxVel*0.85f)
            VelocityImage.color = VelocityColor[2];
    }
    public void ScoreScreen()
    {
        scoreScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
        sc_Timetext.text = CountUpTimer();
        sc_cointext.text = "CYBERBITS: " + coins;

        int levelIndex = SceneManager.GetActiveScene().buildIndex;

        if(timer >= GameManager.instance.scoreScreenRanks[levelIndex].m_rankC)
        {
            sc_Ranktext.text = "C";
        }
        if(timer <= GameManager.instance.scoreScreenRanks[levelIndex].m_rankC)
        {
            sc_Ranktext.text = "C";
        }
        if(timer <= GameManager.instance.scoreScreenRanks[levelIndex].m_rankB)
        {
            sc_Ranktext.text = "B";
        }
        if(timer <= GameManager.instance.scoreScreenRanks[levelIndex].m_rankA)
        {
            sc_Ranktext.text = "A";
        }
        
        
        
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
        int totalSceneInBuild = SceneManager.sceneCountInBuildSettings - 1;

        //Debug.Log(SceneManager.GetSceneByBuildIndex(netxSceneID));
        Debug.Log(SceneManager.sceneCountInBuildSettings -1);
        if (netxSceneID <= totalSceneInBuild)
            SceneManager.LoadScene(netxSceneID);
        if (netxSceneID > totalSceneInBuild)
            SceneManager.LoadScene(0); 

        
    }
    public static void ResetScene()
    {
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneID);
    }

}
