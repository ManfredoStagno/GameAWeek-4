using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 25;
    public float myDamage = 10;
    public float lifeSpan;

    private float timer;

    private void FixedUpdate()
    {
        transform.position += transform.up.normalized * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= lifeSpan)
        {
            timer = 0;
            gameObject.SetActive(false); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.OnDamaged(myDamage);
        }

        timer = 0;
        gameObject.SetActive(false);        
    }
}
