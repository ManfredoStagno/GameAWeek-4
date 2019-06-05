using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    Rigidbody rb;

    public float XSpeed;
    public float YSpeed;

    public float pitch;
    public float roll;
    public float yaw;

    public float smoothSpeed = 0.1f;
    public float rotSpeed = 0.15f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveShip();
    }

    void MoveShip()
    {
        //Movement
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"), 0);
        moveVector.x *= XSpeed;
        moveVector.y *= YSpeed;
        Vector3 desiredPos = transform.position + moveVector * Time.deltaTime;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        rb.MovePosition(smoothedPos);

        //Rotation
        Vector3 moveAngles = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), -Input.GetAxis("Horizontal"));
        moveAngles.x *= pitch;
        moveAngles.y *= yaw;
        moveAngles.z *= roll;
        Quaternion moveRotation = Quaternion.Euler(moveAngles);
        Quaternion smoothedRot = Quaternion.Lerp(transform.rotation, moveRotation, rotSpeed);
        rb.MoveRotation(smoothedRot);
    }
}
