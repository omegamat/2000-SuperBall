using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    //public static PlayerController i{get; private set;}

    public float velocimetro = 0;

    public float m_SpeedForce = 80;
    public float m_MaxSpeed = 35f;
    private float m_ActualMaxSpeed = 0;
    public float m_MaxFallSpeed = 35f;
    public float m_JumpForce = 2000f;
    
    public float m_MaxDrag = 1f;
    public float m_MinDrag = 0f;

    public float m_acceleration = 2;
    private float m_accel = 0;

    public bool isGliding{ get; private set;}

    Rigidbody myRigid;

    private bool jumpInput = true;

    public ParticleSystem jumpEffect;
    public ParticleSystem FallImpactEffect;
    private bool canFallImpactEffect = false;
    //public ParticleSystem SpeedLineEffect;
    public ParticleSystem SpeedParticuleEffect;

    public float speedLimitForEffects = 30f;   
    
    protected virtual void Start()
    {
        myRigid = gameObject.GetComponent<Rigidbody>();
        m_ActualMaxSpeed = m_MaxSpeed;
    }

    protected virtual void Update()
    {
        //save jump input to be used in fixed update.
        if (Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
            
        }
        SpeedEffect(speedLimitForEffects);

        Acceleration(m_acceleration);
        if(isGrounded())
        {
            isGliding = false;
        }
        if(!isGrounded())
        {
            if(Input.GetButtonDown("Jump"))
            {
                GlideSwitch();
            }               
        }

         

        if (isGliding)
        {
            m_MaxFallSpeed = 10;
        }
        if (!isGliding)
        {
            m_MaxFallSpeed = 30000;
        }
        

        velocimetro = myRigid.velocity.magnitude;
        //velocimetro = myRigid.velocity.magnitude;
    }
    void FixedUpdate()
    {
        Movement();
        if (jumpInput)
        {
            Jump();           
            jumpInput = false;
        }

        if(myRigid.velocity.magnitude > m_MaxSpeed)
        {
            //myRigid.velocity = myRigid.velocity.normalized * m_ActualMaxSpeed;

        }

        myRigid.velocity = new Vector3 
        (
            Mathf.Clamp(myRigid.velocity.x,-m_ActualMaxSpeed - m_accel, m_ActualMaxSpeed + m_accel),
            Mathf.Clamp(myRigid.velocity.y,-m_MaxFallSpeed, 9000),
            Mathf.Clamp(myRigid.velocity.z,-m_ActualMaxSpeed - m_accel, m_ActualMaxSpeed + m_accel)
        );
        
        if(myRigid.velocity.y < 0)
        {
            myRigid.velocity += Vector3.up * Physics.gravity.y * (3.5f -1) * Time.deltaTime;
        }
        else if(myRigid.velocity.y > 0)
        {
            myRigid.velocity += Vector3.up * Physics.gravity.y * (1.5f -1) * Time.deltaTime;
        }
        else if(myRigid.velocity.y < 0 && isGliding)
        {
            myRigid.velocity += Vector3.up * Physics.gravity.y * (0.3f -1) * Time.deltaTime;
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
            myRigid.AddForce(movement * m_SpeedForce ,ForceMode.Acceleration);          

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
            myRigid.drag = 0.5f;
            myRigid.AddForce(movement * m_SpeedForce);
        }
      
    }

    void Acceleration(float _accel)
    {
        float _timer =+ Time.deltaTime;

        if (myRigid.velocity.magnitude >= m_MaxSpeed - 1)
        {
            m_accel = m_accel + _timer * _accel;
        }
        if (myRigid.velocity.magnitude < m_MaxSpeed - 3)
        {
            m_accel = 0;
        }
        m_ActualMaxSpeed = m_MaxSpeed + m_accel;

    }

    void Jump()
    {
        if (isGrounded())
        {
            myRigid.AddForce(m_JumpForce * Vector3.up);
            jumpEffect.Play();
            Debug.Log("jump!");

            canFallImpactEffect = true;
        }
        if (!isGrounded())
        {
            Debug.Log("CAN't jump");
        }

    }
    private void OnCollisionEnter(Collision col) 
    {
        if(canFallImpactEffect)
        {
            FallImpactEffect.Play();
            canFallImpactEffect = false;
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

    public void GlideSwitch()
    {
        isGliding = !isGliding;

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
