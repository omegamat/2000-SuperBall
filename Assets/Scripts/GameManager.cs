using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}

    //public GameObject PlayerUI;
    public float m_time = 0;
    public int m_cyberCoins = 0;
    public int m_medals = 0;
    private void Awake() 
    {
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this;
            DontDestroyOnLoad(this.gameObject); 
        }

        Time.timeScale = 1f;
    }
    public void Test()
    {
        Debug.Log("Test");
    }
    public void ReciveCoins()
    {
        m_cyberCoins += 1;
        Debug.Log("coin");
    }
    public int GetCoins()
    {
        return m_cyberCoins;
    }
    
    
    public ScoreScreenRanks[] scoreScreenRanks;
}
[System.Serializable]
public class ScoreScreenRanks
{
    public int m_levelIndex;

    public float m_rankA = 10;
    public float m_rankB = 20;
    public float m_rankC = 30;

}

