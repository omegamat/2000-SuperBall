using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private PlayerController m_playerController;
    public GameObject m_Wings;

    void Start()
    {
        m_playerController = gameObject.GetComponent<PlayerController>();
        if(m_playerController == null)
            Debug.LogWarning("PlayerController in" + gameObject.name + " Missing");      
    }
    void Update()
    {
        Wings();

        if (m_playerController.isGliding)
        {
            m_Wings.SetActive(true);
        }
        if (!m_playerController.isGliding)
        {
            m_Wings.SetActive(false);
        }
        
    }
    void Wings()
    {

        float _moveHorizontal = Input.GetAxis("Horizontal");
        float _moveVertical = Input.GetAxis("Vertical");

        Vector3 _xz = new Vector3(_moveHorizontal,0,_moveVertical);
        Vector3 _dir = _xz + this.transform.localPosition;


        m_Wings.transform.DOLookAt(_dir ,1.5f);

        if(!m_playerController.isGrounded())
        {
            m_Wings.SetActive(true);
            //m_WingsMat.DOFade(255,1);
        }
    }
}
