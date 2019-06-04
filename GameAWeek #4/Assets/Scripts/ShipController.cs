using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    Rigidbody rb;

    public float XSpeed;
    public float YSpeed;
    public float smoothSpeed = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"), 0); //non normalized - don't want axis raw
        moveVector.x *= XSpeed;
        moveVector.y *= YSpeed;

        Vector3 desiredPos = transform.position + moveVector * Time.deltaTime;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);

        rb.MovePosition(smoothedPos);

        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveVector), 0.15F);
            //transform.Translate(moveVector * Time.deltaTime, Space.World);
        }
    }
}
