using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothFactor;
    private void Start()
    {
     
    }

    void FixedUpdate()
    {

        following();
        
    }

    void following()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothCamera = Vector3.Lerp(transform.position, targetPosition, smoothFactor*Time.fixedDeltaTime);

        transform.position = smoothCamera;
    }
}
