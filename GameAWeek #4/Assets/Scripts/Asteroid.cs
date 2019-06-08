﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IPooledObject, IDamageable
{
    Rigidbody rb;

    public float myDamage = 10;

    public float maxTorque = 1;

    public float minVelocity = 10f;
    public float maxVelocity = 20f;

    public void OnObjectSpawned()
    {
        //Randomize Torque
        Vector3 torque = new Vector3
            (Random.Range(-1, 1) * Random.Range(0f, maxTorque), 
            Random.Range(-1, 1) * Random.Range(0f, maxTorque), 
            Random.Range(-1, 1) * Random.Range(0f, maxTorque));
        GetComponent<Rigidbody>().AddTorque(torque, ForceMode.Force);

        //Randomize Velocity
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -1) * Random.Range(minVelocity, maxVelocity);
    }

    public void OnDamaged(float damage)
    {
        Explode();
    }

    void Explode()
    {
        GameObject explosion = Instantiate(Resources.Load("Explosion") as GameObject, transform.position, Quaternion.LookRotation(Vector3.up, Vector3.forward));
        explosion.transform.localScale *= (transform.localScale.x + transform.localScale.y + transform.localScale.z)/3;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Asteroid"))
        {
            IDamageable damageable = collision.collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.OnDamaged(myDamage);
            }
        }
    }
}
