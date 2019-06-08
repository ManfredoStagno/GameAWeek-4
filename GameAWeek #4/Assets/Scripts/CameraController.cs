using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;

    public Vector3 cameraOffset;
    public float smoothSpeed = .125f;

    public Vector3 lookOffset;
    public float lookAtSpeed = .125f;

    Vector3 currentLookAt;
    private void FixedUpdate()
    {
        Vector3 desiredPosition = player.position+ cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
        
        Quaternion desiredRotation = Quaternion.LookRotation(player.position - transform.position + lookOffset);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, lookAtSpeed);

        //Vector3 desiredLookAt = player.position + lookOffset;
        //transform.LookAt(desiredLookAt);
        

    }

    
}
