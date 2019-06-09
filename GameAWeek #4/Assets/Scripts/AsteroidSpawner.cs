using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;

    public string objectTag;

    public float radius = 15f;
    public float minRadius = 0f;

    public bool isAsteroid = true;
    public int astSpawnTimer = 25;
    private int astCounter;

    public bool isFuel = true;
    public int fuelSpawnTimer = 50;
    private int fuelCounter;

    void Start()
    {
        objectPooler = ObjectPooler.instance;

        astCounter = astSpawnTimer;
        fuelCounter = fuelSpawnTimer;
    }

    void FixedUpdate()
    {
        if(isAsteroid)
            SpawnAsteroids(RandomPointOnSpawner());
        if(isFuel)
            SpawnFuel(RandomPointOnSpawner());
    }

    void SpawnAsteroids(Vector3 point)
    {
        astCounter--;
        if (astCounter == 0)
        {
            if (point.magnitude < radius)
            {
                objectPooler.SpawnFromPool(objectTag, transform.position + point, Random.rotation);
            }

            astCounter = astSpawnTimer;
        }
    }

    void SpawnFuel(Vector3 point)
    {
        fuelCounter--;
        if (fuelCounter == 0)
        {
            if (point.magnitude < radius)
            {
                objectPooler.SpawnFromPool("Fuel", transform.position + point, Quaternion.identity);
            }

            fuelCounter = fuelSpawnTimer;
        }
    }

    Vector3 RandomPointOnSpawner()
    {
        Vector3 point = Random.insideUnitCircle;

        point.Normalize();
        point *= Random.Range(minRadius, radius);

        return point;        
    }

    private void OnDrawGizmos()
    {
        if (isFuel)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);

           
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minRadius);       
    }
}
