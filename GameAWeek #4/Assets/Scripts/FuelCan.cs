using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCan : MonoBehaviour
{
    public float myDamage = -10;

    public float velocity = 10;

    private void FixedUpdate()
    {
        transform.position += new Vector3(0, 0, -velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Asteroid"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.OnDamaged(myDamage);
            }

            Instantiate(Resources.Load("Collect") as GameObject, transform.position, Quaternion.LookRotation(Vector3.up, Vector3.forward));
            gameObject.SetActive(false);
        }
    }
}
