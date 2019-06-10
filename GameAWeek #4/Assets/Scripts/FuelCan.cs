using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCan : MonoBehaviour, IPooledObject
{
    AudioSource audioSource;

    public AudioClip collectSound;

    public float myDamage = -10;

    public float velocity = 10;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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

            foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
                mr.enabled = false;
            GetComponent<Collider>().enabled = false;
            
            StartCoroutine(PlaySound());

            //gameObject.SetActive(false);
        }
    }

    public void OnObjectSpawned()
    {
        foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
            mr.enabled = true;
        GetComponent<Collider>().enabled = true;
    }

    IEnumerator PlaySound()
    {
        audioSource.PlayOneShot(collectSound);
        yield return new WaitWhile(() => audioSource.isPlaying);
        gameObject.SetActive(false);
    }
}
