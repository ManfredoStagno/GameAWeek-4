using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;
    AudioSource audioSource;

    public AudioClip shoot;

    public int spawnTimer = 25;

    private int counter;

    void Start()
    {
        objectPooler = ObjectPooler.instance;
        audioSource = GetComponent<AudioSource>();

        counter = spawnTimer;
    }

    private void FixedUpdate()
    {

    }

    public void ShootBullets()
    {
        counter--;
        if (counter == 0)
        {
            objectPooler.SpawnFromPool("Bullet", transform.position, Quaternion.Euler(transform.eulerAngles));
            audioSource.PlayOneShot(shoot);
            
            counter = spawnTimer;
        }
    }
}
