using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameManager gm;
    ShipController playerController;

    public float speed = 25;
    public float myDamage = 10;
    public float lifeSpan;

    private float timer;

    private void Start()
    {
        gm = GameManager.instance;
        playerController = gm.player.GetComponent<ShipController>();
    }

    private void FixedUpdate()
    {
        transform.position += transform.up.normalized * (speed + playerController.speed) * Time.deltaTime;

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

        if (damageable != null && !other.CompareTag("Player"))
        {
            damageable.OnDamaged(myDamage);
        }

        timer = 0;
        Instantiate(Resources.Load("BulletExp") as GameObject, transform.position, Quaternion.LookRotation(Vector3.up, Vector3.forward));
        gameObject.SetActive(false);        
    }
}
