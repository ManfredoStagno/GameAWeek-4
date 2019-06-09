using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    Camera cam;

    public Vector3 cameraOffset;
    public float smoothSpeed = .125f;
    public float speedFactor = 1;

    public Vector3 lookOffset;
    public float lookAtSpeed = .125f;
    Vector3 currentLookAt;

    public float minFov;
    public float maxFov;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate()
    {
        Vector3 actualOffset;
        actualOffset = cameraOffset * speedFactor * GameManager.instance.playerSpeed;

        Vector3 desiredPosition = player.position + cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;


        Quaternion desiredRotation = Quaternion.LookRotation(player.position - cam.transform.position + lookOffset);
        transform.rotation = Quaternion.Slerp(cam.transform.rotation, desiredRotation, lookAtSpeed);      

        //cam.transform.LookAt(player);

    }

    void FovController(float speed)
    {

    }
    
}
