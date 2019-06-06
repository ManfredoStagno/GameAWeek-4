using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;

    public float radius = 15f;
    public int spawnTimer = 25;

    private int counter;

    void Start()
    {
        objectPooler = ObjectPooler.instance;
        counter = spawnTimer;
    }

    void FixedUpdate()
    {
         SpawnAsteroids(RandomPointOnSpawner());               
    }

    void SpawnAsteroids(Vector3 point)
    {
        counter--;
        if (counter == 0)
        {
            if (point.magnitude < radius)
            {
                objectPooler.SpawnFromPool("Asteroid", transform.position + point, Random.rotation);
            }

            counter = spawnTimer;
        }
    }

    Vector3 RandomPointOnSpawner()
    {
        return new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
