using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Asteroid : MonoBehaviour, IPooledObject, IDamageable
{
    Rigidbody rb;

    public float myDamage = 10;

    public float maxTorque = 1;

    public float minVelocity = 10f;
    public float maxVelocity = 20f;

    public float maxScale = 3;

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

        //Random Scale
        transform.localScale = new Vector3(Random.Range(1f, maxScale), Random.Range(1f, maxScale), Random.Range(1f, maxScale));
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
                float scaleMultiplier = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;

                damageable.OnDamaged(myDamage * scaleMultiplier);

                CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            }
        }
    }
}
