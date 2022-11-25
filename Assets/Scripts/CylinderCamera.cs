using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CylinderCamera : MonoBehaviour
{
    public Transform m_player;
    public Transform m_CameraPosition;
    public CinemachineVirtualCamera m_cm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraPosition();
        transform.position = new Vector3(transform.position.x,m_player.position.y,transform.position.z) ;
    }
    void CameraPosition()
    {
        Vector3 _myPosition = new Vector3(transform.position.x,m_player.position.y,transform.position.z);
        Vector3 _lookPosition = m_player.position + new Vector3(0,0,-1) * 5;
        Vector3 _heading  = m_player.position - transform.position;
        float _distance = _heading.magnitude;
        Vector3 _dir = _heading / _distance;
        Vector3 _cameraPos = new Vector3(_dir.x * 70,m_player.position.y + 10,_dir.z * 70);

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
        Vector3 _myPosition = new Vector3(transform.position.x,m_player.position.y,transform.position.z);
        Vector3 _lookPosition = m_player.position + new Vector3(0,0,-1) * 5;
        Vector3 _heading  = m_player.position - transform.position;
        float _distance = _heading.magnitude;
        Vector3 _dir = _heading / _distance;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, _dir * 70);
        //Gizmos.DrawSphere(_lookPosition)
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawLine(_myPosition, _lookPosition);
        Gizmos.DrawSphere((_dir * 70) + transform.position , 10);
    }
}
