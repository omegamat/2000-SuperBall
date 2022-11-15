using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public Transform objToRotate;

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
    public void RotateMenus(int _p)
    {
        //Vector3 _rotation = new Vector3(_x,_y,_z);
        switch (_p)
        {
            case 0://front
                objToRotate.DORotate(Vector3.zero,0.25f);
            break;
            case 1://rigth
                objToRotate.DORotate(new Vector3(0,90f,0),0.25f);
            break;
            case 2://back
                objToRotate.DORotate(new Vector3(0,180f,0),0.25f);
            break;
            case 3://left
                objToRotate.DORotate(new Vector3(0,-90f,0),0.25f);
            break;
            case 4://up
                objToRotate.DORotate(new Vector3(-90,0,0),0.25f);
            break;
            case 5://down
                objToRotate.DORotate(new Vector3(90,0,0),0.25f);
            break;
            

        }
        //SphereMainMenu.DORotate(Vector3.zero,0.25f);
    }
}
