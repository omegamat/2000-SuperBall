using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private PlayerController m_playerController;
    public GameObject m_Wings;
    public GameObject m_BodyAnchor;
    public GameObject m_Body;

    public Vector3 m_rotation;
    public float m_rotationSpeed;
    private Vector3 _eulerAngles;
    public Vector3 _lookOffset;

    void Start()
    {
        m_playerController = gameObject.GetComponent<PlayerController>();
        if(m_playerController == null)
            Debug.LogWarning("PlayerController in" + gameObject.name + " Missing");      
    }
    void Update()
    {
        Wings();
        Body();

        if (m_playerController.isGliding)
        {
            m_Wings.SetActive(true);
        }      
        if (!m_playerController.isGliding)
        {
            m_Wings.SetActive(false);
        }
        
    }
    void Body()
    {
        //m_playerController.myRigid.velocity.normalized + m_Body.transform.position;
        Vector3 _VelocityDirection;
        //Vector3 _eulerAngles = Vector3.one;
        _VelocityDirection = m_playerController.myRigid.velocity.normalized + m_BodyAnchor.transform.position;
        //_eulerAngles += (m_rotation) * Time.deltaTime * (m_rotationSpeed * m_playerController.myRigid.velocity.magnitude);
        _eulerAngles += (new Vector3(0,0,m_rotation.z * Time.deltaTime * (m_rotationSpeed * m_playerController.myRigid.velocity.magnitude))) ;

        m_BodyAnchor.transform.LookAt(_VelocityDirection);
        //m_Body.transform.eulerAngles = new Vector3(0,0,90);

        m_Body.transform.localEulerAngles = _eulerAngles + new Vector3(0,90,0);       
    }
    void Wings()
    {

        float _moveHorizontal = Input.GetAxis("Horizontal");
        float _moveVertical = Input.GetAxis("Vertical");

        Vector3 _xz = new Vector3(_moveHorizontal,0,_moveVertical);
        Vector3 _dir = _xz + this.transform.position;


        m_Wings.transform.DOLookAt(_dir ,1.5f);;

        if(!m_playerController.isGrounded())
        {
            m_Wings.SetActive(true);
            //m_WingsMat.DOFade(255,1);
        }
    }
}
