using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseInterface;


    private void Start()
    {
         Debug.Log(this.gameObject.transform.name);
         Pause();
    }
    private void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            gameIsPaused = !gameIsPaused;
            Pause();         
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("GAME QUIT!");
    }
    public void PauseSwitch()
    {
        gameIsPaused = !gameIsPaused;
        Pause();
    }

    //pause game using timescale
    void Pause()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            pauseInterface.SetActive(true);

        }
        if (!gameIsPaused)
        {
            Time.timeScale = 1;
            pauseInterface.SetActive(false);
        }
    }
}
