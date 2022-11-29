using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CylinderCamera : MonoBehaviour
{
    public Transform m_player;
    public Transform m_CameraPosition;
    
    public CinemachineVirtualCamera m_cm;

    public float m_cameraDistance = 100f;
    public float m_CameraYoffset = 10;
    private void Start()
    {
        if(m_player == null)
            m_player = GameObject.Find("Ball").gameObject.transform; 
    }
       

    // Update is called once per frame
    void Update()
    {
        CameraPosition();
        transform.position = new Vector3(transform.position.x,m_player.position.y,transform.position.z) ;
    }
    void CameraPosition()
    {
        Vector3 _heading  = m_player.position - transform.position;
        float _distance = _heading.magnitude;
        Vector3 _dir = _heading / _distance;
        Vector3 _cameraPos = new Vector3(_dir.x * m_cameraDistance, m_CameraYoffset, _dir.z * m_cameraDistance);

        m_CameraPosition.transform.position = _cameraPos + transform.position;
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
            m_cm.Priority = 11;
    }
    private void OnTriggerExit(Collider other) 
    {   
        if(other.gameObject.tag == "Player")
            m_cm.Priority = 9;
    }
    private void OnDrawGizmos() 
    {
        Vector3 _heading  = m_player.position - transform.position;
        float _distance = _heading.magnitude;
        Vector3 _dir = _heading / _distance;
        Vector3 _cameraPos = new Vector3(_dir.x * m_cameraDistance,m_player.position.y + m_CameraYoffset,_dir.z * m_cameraDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, _dir * m_cameraDistance);
        //Gizmos.DrawSphere(_lookPosition)
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawLine(_myPosition, _lookPosition);
        Gizmos.DrawSphere(_cameraPos + transform.position , 10);
    }
}
