using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Asteroid : MonoBehaviour, IPooledObject, IDamageable
{
    Rigidbody rb;
    AudioSource audioSource;

    public AudioClip explosionClip;
    public float volume;

    public float myHealth = 10;
    private float startingHealth;

    public float myDamage = 10;

    public float maxTorque = 1;

    public float minVelocity = 10f;
    public float maxVelocity = 20f;

    public float maxScale = 3;

    private void Start()
    {
        startingHealth = myHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void OnObjectSpawned()
    {

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;

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

        //FixHealth
        myHealth = startingHealth * (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;
    }

    public void OnDamaged(float damage)
    {
        Explode();
    }

    void Explode()
    {
        GameObject explosion = Instantiate(Resources.Load("Explosion") as GameObject, transform.position, Quaternion.LookRotation(Vector3.up, Vector3.forward));
        explosion.transform.localScale *= (transform.localScale.x + transform.localScale.y + transform.localScale.z)/3;

       // audioSource.PlayOneShot(explosionClip);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;        
        CameraShaker.Instance.ShakeOnce(.5f, 4f, .1f, 1f);
        //gameObject.SetActive(false);
        StartCoroutine(PlaySound());
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
            }
        }
    }

    IEnumerator PlaySound()
    {
        audioSource.PlayOneShot(explosionClip);
        yield return new WaitWhile(() => audioSource.isPlaying);
        gameObject.SetActive(false);
    }
}
