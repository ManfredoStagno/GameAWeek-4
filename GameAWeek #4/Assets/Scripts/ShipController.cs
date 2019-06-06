using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour, IDamageable
{
    Rigidbody rb;
    public BulletSpawner bulletSpawner;

    public float myHealth = 100;
    private float myDamage = 10000;

    public float XSpeed;
    public float YSpeed;
    public float speed;

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
        Shoot();
    }

    void MoveShip()
    {
        //Movement
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"), speed);
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
        //fix import
        moveAngles.x *= -1; moveAngles.y += 180f; moveAngles.z *= -1;        

        Quaternion moveRotation = Quaternion.Euler(moveAngles);
        Quaternion smoothedRot = Quaternion.Lerp(transform.rotation, moveRotation, rotSpeed);
        rb.MoveRotation(smoothedRot);
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            bulletSpawner.ShootBullets();
        }
    }

    public void OnDamaged(float damage)
    {
        myHealth -= damage;
        if (myHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("You Dead!");
    }
}
