using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public GameObject TransformToFollow;

    public float Xoffset = 0;
    public float Yoffset = 0;
    public float Zoffset = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _follow = TransformToFollow.transform.position;
        Vector3 _pos = new Vector3(_follow.x + Xoffset,_follow.y + Yoffset,_follow.z + Zoffset);
        this.gameObject.transform.position = _pos;
    }
}
