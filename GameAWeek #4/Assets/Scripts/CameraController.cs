using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    Camera cam;
    ShipController playerController;

    public Vector3 cameraOffset;
    public float smoothSpeed = .125f;
    public float speedFactor = 1;

    public Vector3 lookOffset;
    public float lookAtSpeed = .125f;
    Vector3 currentLookAt;

    public float minFov;
    public float maxFov;
    public float fovSpeed = .1f;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        playerController = player.GetComponent<ShipController>();
    }

    private void FixedUpdate()
    {
        Vector3 actualOffset;
        actualOffset = cameraOffset; // - Vector3.forward * playerController.speed * speedFactor;

        Vector3 desiredPosition = player.position + actualOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;


        Quaternion desiredRotation = Quaternion.LookRotation(player.position - cam.transform.position + lookOffset);
        transform.rotation = Quaternion.Slerp(cam.transform.rotation, desiredRotation, lookAtSpeed);      

        //cam.transform.LookAt(player);

    }

    void Fov()
    {
        float desiredFOV = minFov + (maxFov - minFov) * playerController.speed/playerController.maxSpeed;
        float smoothedFov = Mathf.Lerp(cam.fieldOfView, desiredFOV, fovSpeed);

        cam.fieldOfView = smoothedFov;        
    }
    
}
