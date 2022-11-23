using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController i{get; private set;}

    public float velocimetro = 0;
    public float m_SpeedForce = 500f;
    public float m_MaxSpeed = 40f;
    public float m_JumpForce = 300f;
    
    public float m_MaxDrag = 1f;
    public float m_MinDrag = 0f;

    Rigidbody myRigid;

    private bool jumpInput = true;

    public ParticleSystem jumpEffect;
    //public ParticleSystem SpeedLineEffect;
    public ParticleSystem SpeedParticuleEffect;

    public float speedLimitForEffects = 30f;
    
    void Start()
    {
        myRigid = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //save jump input to be used in fixed update.
        if (Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
        }
        SpeedEffect(speedLimitForEffects);

        velocimetro = myRigid.velocity.magnitude;
    }
    void FixedUpdate()
    {
        Movement();
        if (myRigid.velocity.magnitude > m_MaxSpeed)
        {
            myRigid.velocity = myRigid.velocity.normalized * m_MaxSpeed;
        }
        if (jumpInput)
        {
            Jump();
            

            jumpInput = false;
        }
        
    }

    //check if player is on ground.
    public bool isGrounded()
    {
        Ray _ray = new Ray(transform.position, new Vector3(0,transform.position.z * -1f,0));

        RaycastHit _hit;

        if (Physics.SphereCast(_ray,0.4f,out _hit,0.9f) )
        {
            //Debug.Log("Hit!..." + _hit.transform.name);
            return true;
        }
        else
        {
            return false;
        }
        
    }
    //Check if plyer is pressing movement button.
    public bool isOnMove()
    {
        float _moveHorizontal = Input.GetAxis("Horizontal");
        float _moveVertical = Input.GetAxis("Vertical");

        Vector3 _movement = new Vector3(_moveHorizontal, 0, _moveVertical);
        _movement = _movement.normalized;
        if (_movement.magnitude == 0f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void Movement()
    {
        float _moveHorizontal = Input.GetAxis("Horizontal");
        float _moveVertical = Input.GetAxis("Vertical");

        Vector3 _xz = new Vector3(_moveHorizontal,0,_moveVertical);
        Vector3 movement = Camera.main.transform.TransformDirection(_xz);
        movement.y = 0;
        movement = movement.normalized;

        if (isGrounded())
        {
            myRigid.AddForce(movement * m_SpeedForce);          

            if (isOnMove())
            {
                //decrease drag whent player is pressing the movemente input.
                myRigid.angularDrag = 1f;
                //myRigid.angularDrag = Mathf.Clamp(myRigid.angularDrag, 1f, 2f);

                myRigid.drag = 1f;
                //myRigid.drag = Mathf.Clamp(myRigid.drag, m_MinDrag, m_MaxDrag);
            }           
            if (!isOnMove())
            {
                //increse drag whent player is NOT pressing the movemente input.
                myRigid.angularDrag = 1.5f;
                //myRigid.angularDrag = Mathf.Clamp(myRigid.angularDrag, 0.05f, 2f);

                myRigid.drag = 1.5f;
                //myRigid.drag = Mathf.Clamp(myRigid.drag, m_MinDrag, m_MaxDrag);
            }

        }
        
        if (!isGrounded())
        {
            //decrease drag whent player is on air.
            myRigid.drag = 0;
            myRigid.AddForce(movement * m_SpeedForce);
        }
      
    }

    void Jump()
    {
        if (isGrounded())
        {
            myRigid.AddForce(m_JumpForce * Vector3.up);
            jumpEffect.Play();
            Debug.Log("jump!");
        }
        if (!isGrounded())
        {
            Debug.Log("CAN't jump");
        }

    }

    void SpeedEffect(float _speed)
    {
        if (myRigid.velocity.magnitude >= _speed)
        {
            //SpeedLineEffect.Play();
            SpeedParticuleEffect.Play();
        }
        if (myRigid.velocity.magnitude <= _speed)
        {
            //SpeedLineEffect.Stop();
            SpeedParticuleEffect.Stop();
        }
    }

    public void Respawn()
    {
        gameObject.transform.position = Vector3.zero;
    }
    public void Reset()
    {
        UIManager.ResetScene();
    }



}
