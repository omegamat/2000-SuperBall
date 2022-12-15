using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//joy stick for mobile
public class JoyStick : MonoBehaviour
{
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    public Vector3 Mousedebug;

    public Transform circle;
    public float circlePositionMult = 10;
    public Transform outerCircle;


    void Update() 
    {
        Mousedebug = Input.mousePosition;
        if(Input.mousePosition.x < Screen.width/2 || touchStart)
        {       
            if(Input.GetMouseButtonDown(0))
            {

                //pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Camera.main.transform.position.z));
                pointA = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);
                circle.transform.position = Input.mousePosition;
                outerCircle.transform.position = Input.mousePosition;

                circle.gameObject.SetActive(true);
                outerCircle.gameObject.SetActive(true);

            }
            if(Input.GetMouseButton(0))
            {
                touchStart = true;
                //pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Camera.main.transform.position.z));
                pointB = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);
            }
            else
            {
                touchStart = false;
                circle.gameObject.SetActive(false);
                outerCircle.gameObject.SetActive(false);
            }
        }
       
        
    }
    public Vector3 TouchJoyStickAxis()
    {
        Vector2 _offset = pointA - pointB;
        Vector2 _direction = Vector2.ClampMagnitude(_offset,circlePositionMult);
        Vector3 _circlePos = Vector2.ClampMagnitude(_offset,circlePositionMult);

        _direction = _direction.normalized;

        Vector3 _axis = new Vector3(_direction.x,0,_direction.y) * -1;

        circle.transform.position = new Vector3(
            pointA.x + (_circlePos.x * -1 ),
            pointA.y + (_circlePos.y * -1),
            0);

        if(touchStart)
            return _axis;
        else
            return Vector3.zero;
    }

    
}
