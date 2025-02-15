﻿using UnityEngine;
using EZCameraShake;

public class ShipController : MonoBehaviour, IDamageable
{
    Rigidbody rb;
    GameManager gm;
    AudioSource audioSource;
    public BulletSpawner bulletSpawner;

    public AudioClip hitClip;
    [Space]
    [HideInInspector]
    public float speed;
    public float minSpeed;
    public float maxSpeed;
    [Space]
    public float startingFuel;
    public float totalFuelTime;
    [HideInInspector]
    public float remainingFuel;
    [Space]
    public float XSpeed;
    public float YSpeed;
    [Space]
    public float pitch;
    public float roll;
    public float yaw;
    [Space]
    public float smoothFactor = 0.1f;
    public float smoothSpeed = 0.1f;
    public float rotSpeed = 0.15f;
    [Space]
    private float myDamage = 10000;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = GameManager.instance;
        audioSource = GetComponent<AudioSource>();

        remainingFuel = startingFuel;
    }

    void FixedUpdate()
    {
        if (!gm.GAMEISOVER)
        {
            CalculateSpeed();
            MoveShip();
                  
        }
        RotateShip();
        if (!gm.GAMEISOVER)
            Shoot(); 

    }

    private void CalculateSpeed()
    {

        float desiredSpeed = minSpeed + maxSpeed * CalculateFuel();

        float smoothedSpeed = desiredSpeed;
        //float smoothedSpeed = Mathf.Lerp(speed, desiredSpeed, smoothFactor);

        speed = smoothedSpeed;
    }

    private float CalculateFuel()
    {
        remainingFuel -= Time.deltaTime;


        if (remainingFuel > totalFuelTime)
            remainingFuel = totalFuelTime;
        if (remainingFuel < 0)
        {
            remainingFuel = 0;
            minSpeed = 0;
            Die();
            return 0f;
        }
        return (remainingFuel / totalFuelTime);
    }

    void MoveShip()
    {
        //Movement
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"), speed);
        moveVector.x *= XSpeed;
        moveVector.y *= YSpeed;
        Vector3 desiredPos = transform.position + moveVector * Time.deltaTime;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);

        smoothedPos = CheckBorders(smoothedPos);

        rb.MovePosition(smoothedPos);
    }


    void RotateShip()
    {
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

    Vector3 CheckBorders(Vector3 smoothedPos)
    {        
        if (smoothedPos.x <= gm.left.position.x)
            smoothedPos.x = gm.left.position.x;
        else if (smoothedPos.x >= gm.right.position.x)
            smoothedPos.x = gm.right.position.x;

        if (smoothedPos.y <= gm.bottom.position.y)
            smoothedPos.y = gm.bottom.position.y;
        else if (smoothedPos.y >= gm.top.position.y)
            smoothedPos.y = gm.top.position.y;

        return smoothedPos;
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            bulletSpawner.ShootBullets();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.collider.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.OnDamaged(myDamage);
        }
    }

    public void OnDamaged(float damage)
    {
        if (!gm.GAMEISOVER)
        {
        remainingFuel -= damage;
        }


        if (Mathf.Sign(damage) > 0) //If it's not fuel
        {
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            audioSource.PlayOneShot(hitClip);
        }

        Debug.Log(remainingFuel);
    }

    private void Die()
    {
        Debug.Log("You Dead!");
        gm.GameOver();
    }
}
