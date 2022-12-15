using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputForButton : MonoBehaviour
{
    public Input m_input;
    public Button jump;
    private void Awake() 
    {
        jump.onClick.AddListener(JumpInput); 
    }
    public void JumpInput()
    {      
        
    }
    private void OnDisable() 
    {
        jump.onClick.RemoveListener(JumpInput);
    }
}
